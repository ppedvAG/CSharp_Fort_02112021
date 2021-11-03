using Bogus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HalloLinq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DemoButton_Clicked(object sender, EventArgs e)
        {
            List<Person> list = new()
            {
                new Person()
                {
                    Name = "Fred",
                    GebDatum = DateTime.Now.AddYears(-30),
                    Schuhgröße = 12,
                    Stadt = "Heidelberg",
                    Auto = "Brumm Brumm"
                },

                new Person()
                {
                    Name = "Wilma",
                    GebDatum = DateTime.Now.AddYears(-29),
                    Schuhgröße = 7,
                    Stadt = "Heidelberg",
                    Auto = "Rotes Brumm Brumm"
                }
            };

            dataGridView1.DataSource = list;
        }

        private void BogusButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDemodaten();
        }

        IEnumerable<Person> GetDemodaten()
        {
            var faker = new Faker<Person>("de");
            faker.UseSeed(12);
            //faker.RuleFor(x => x.Name, (f, p) => $"{f.Name.FullName()} {p.Schuhgröße}" );
            faker.RuleFor(x => x.Name, (f, p) => f.Name.FullName());
            faker.RuleFor(x => x.GebDatum, (f, p) => f.Date.Past(30));
            faker.RuleFor(x => x.Schuhgröße, (f, p) => f.Random.Number(33, 55));
            faker.RuleFor(x => x.Stadt, (f, p) => f.Address.City());
            faker.RuleFor(x => x.Auto, (f, p) => $"{f.Vehicle.Manufacturer()} {f.Vehicle.Model()} {p.Schuhgröße}");
            //faker.RuleFor(x => x.Auto, x => x.Vehicle.Model());
            //faker.RuleFor(x => x.Auto,);

            return faker.Generate(1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDemodaten().Where(p => p.Schuhgröße > 40 && p.Name.Length < 30)
                                                     .OrderBy(p => p.GebDatum.Month)
                                                     .ThenByDescending(x => x.GebDatum.Year)
                                                     .ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var query = from p in GetDemodaten()
                        where p.Schuhgröße > 40 && p.Name.Length < 30
                        orderby p.GebDatum.Month, p.GebDatum.Year descending
                        select p;

            dataGridView1.DataSource = query.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control item in flowLayoutPanel1.Controls.OfType<Button>().Where(x => x.Text.StartsWith("Linq")))
            {
                item.BackColor = Color.Green;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double av = GetDemodaten().Average(x => x.Schuhgröße);
            int count = GetDemodaten().Count(x => x.Schuhgröße > 40);

            bool istDaEiner = GetDemodaten().Any(x => x.Schuhgröße > 60);
            bool sindAlle = GetDemodaten().All(x => x.Schuhgröße > 40);

            IEnumerable<string?> alleAutos = GetDemodaten().Select(x => x.Auto);

            var erster = GetDemodaten().FirstOrDefault(x => x.Schuhgröße > 53);
            if (erster != null)
                MessageBox.Show(erster.Name);

            var nachYear = GetDemodaten().GroupBy(x => x.GebDatum.Year);

            MessageBox.Show(av.ToString());
        }
    }

    class MYComp : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x.Name == y.Name)
                return 0; //sind gleich

            return 1;
            return -1;
        }
    }
}
