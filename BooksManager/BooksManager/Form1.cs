using BooksManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
            var bm = new Data.BooksManager(null);

            var result = await bm.GetVolumeinfoFromGoogleAsync(textBox1.Text);
            dataGridView1.DataSource = result.ToList();

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog() { Filter = "Json Bücherdatei|*.json|Alle Dateien|*.*" };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var bm = new Data.BooksManager(new BooksJsonRepository(dlg.FileName));
                await bm.BooksRepository.Save((IEnumerable<Volumeinfo>)dataGridView1.DataSource);
                MessageBox.Show("Speichern erfolgreich");
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog() { Filter = "Json Bücherdatei|*.json|Alle Dateien|*.*" };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var bm = new Data.BooksManager(new BooksJsonRepository(dlg.FileName));
                dataGridView1.DataSource = new BindingList<Volumeinfo>((await bm.BooksRepository.Load()).ToList());
            }
        }
    }
}
