using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiereManager.Data;
using TiereManager.Model;

namespace TiereManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IRepository Repository { get; set; } = new DemoRepo();

        private void loadButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Repository.GetAll<Katze>();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Repository.SaveAll((IEnumerable<Katze>)dataGridView1.DataSource);
        }
    }
}
