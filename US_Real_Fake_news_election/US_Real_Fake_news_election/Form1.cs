using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace US_Real_Fake_news_election
{
    public partial class Form1 : Form
    {
        readonly List<News> NewsList = new List<News>();
        public Form1()
        {
            InitializeComponent();
            TimeBox USTimeBox = new TimeBox();
            Controls.Add(USTimeBox);
            USTimeBox.Start();
        }


        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XElement newsList = XElement.Load(openFileDialog1.FileName);
                IEnumerable<XElement> news =
                    from element in newsList.Elements()
                    select element;
                foreach (XElement x in news)
                {
                    News MyNews = new News(x);
                    NewsList.Add(MyNews);
                }

                dataGridView1.DataSource = NewsList.ToList();
            }
            else
                MessageBox.Show("Fájl kiválasztása szükséges!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
