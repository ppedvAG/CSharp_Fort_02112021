using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(100);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    progressBar1.Invoke(new Action(() => progressBar1.Value = i));
                    Thread.Sleep(10);
                }

                button2.Invoke(new Action(() => button2.Enabled = true));
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ts = TaskScheduler.FromCurrentSynchronizationContext(); //UI Thread
            cts = new CancellationTokenSource();
            button3.Enabled = false;

            var t = Task.Factory.StartNew<long>(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Factory.StartNew(() => progressBar1.Value = i, CancellationToken.None, TaskCreationOptions.None, ts);
                    Thread.Sleep(10);

                    if (cts.IsCancellationRequested)
                        return 0; //cleanup

                    if (cts.IsCancellationRequested) //bei ()
                        break;
                }
                return 3290845;
            });

            t.ContinueWith(t => MessageBox.Show($"Fertig: {t.Result}"), cts.Token, TaskContinuationOptions.None, ts);


            t.ContinueWith(t => 
                            {
                                button3.Enabled = true;
                                label1.Text = t.Result.ToString();
                            }, CancellationToken.None, TaskContinuationOptions.NotOnFaulted, ts);




        }

        CancellationTokenSource cts = new CancellationTokenSource();

        private void button4_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }


        private async void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            cts = new CancellationTokenSource();

            try
            {

                for (int i = 0; i < 100; i++)
                {
                    progressBar1.Value = i;
                    await Task.Delay(100, cts.Token);
                    //await File.ReadAllLinesAsync(@"C:\großeDatei.txt");
                }
            }
            catch (TaskCanceledException ex)
            {
                //er wurde erfolgreich abgebrochen
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler: {ex.Message}");
            }

            button5.Enabled = true;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            cts = new CancellationTokenSource();

            var http = new HttpClient();
            //var url = $"http://www.placebacon.net/{pictureBox1.Width}/{pictureBox1.Height}";
            var url = $"http://www.placekitten.com/{pictureBox1.Width}/{pictureBox1.Height}";

            Stream stream = await http.GetStreamAsync(url, cts.Token);
            await Task.Delay(5000, cts.Token);

            var img = Image.FromStream(stream);

            pictureBox1.Image = img;

            progressBar1.Style = ProgressBarStyle.Continuous;
            button6.Enabled = true;
        }


        private long BerechneGanzLangsam(int wert)
        {
            Thread.Sleep(5000);
            return wert * wert / 2 - 5 + 8;
        }

        private Task<long> BerechneGanzLangsamAsync(int wert, CancellationToken cts = default)
        {
            return Task.Run(() => BerechneGanzLangsam(wert), cts);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(BerechneGanzLangsam(745).ToString());
            MessageBox.Show((await BerechneGanzLangsamAsync(745)).ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
