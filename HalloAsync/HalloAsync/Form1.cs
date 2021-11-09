using System;
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

            t.ContinueWith(t =>
                            {
                                button3.Enabled = true;
                                label1.Text = t.Result.ToString();
                            }, CancellationToken.None, TaskContinuationOptions.NotOnFaulted, ts);


            t.ContinueWith(t => MessageBox.Show($"Fertig: {t.Result}"), cts.Token, TaskContinuationOptions.None, ts);


        }

        CancellationTokenSource cts = new CancellationTokenSource();

        private void button4_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
