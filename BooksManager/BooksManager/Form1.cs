using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace BooksManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={textBox1.Text}";

            var http = new HttpClient();

            var json = await http.GetStringAsync(url);

            BooksResult br = JsonSerializer.Deserialize<BooksResult>(json);


            dataGridView1.DataSource = br.items.Select(x => x.volumeInfo).ToList();

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog() { Filter = "Json Bücherdatei|*.json|Alle Dateien|*.*" };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var json = JsonSerializer.Serialize((IEnumerable<Volumeinfo>)dataGridView1.DataSource,
                                                new JsonSerializerOptions() { WriteIndented = true });
                await File.WriteAllTextAsync(dlg.FileName, json);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog() { Filter = "Json Bücherdatei|*.json|Alle Dateien|*.*" };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var json = await File.ReadAllTextAsync(dlg.FileName);
                IEnumerable<Volumeinfo> vi = JsonSerializer.Deserialize<IEnumerable<Volumeinfo>>(json);
                dataGridView1.DataSource = new BindingList<Volumeinfo>(vi.ToList());
            }
        }
    }
}
