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
        private readonly Classes Filter;
        private XElement NewsDocument;

        public Form1()
        {
            InitializeComponent();
            TimeBox USTimeBox = new TimeBox();
            Filter = new Classes();
            Controls.Add(USTimeBox);
            Controls.Add(Filter);
            USTimeBox.Start();
        }


        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NewsDocument = XElement.Load(openFileDialog1.FileName);
                ButtonQuery_Click(sender, e);
            }
            else
                MessageBox.Show("Fájl kiválasztása szükséges!");
        }
        private void ButtonQuery_Click(object sender, EventArgs e)
        {
            NewsList.Clear();
            IEnumerable<XElement> news = Filter.FilteredData(NewsDocument);
            foreach (XElement x in news)
            {
                News MyNews = new News(x);
                NewsList.Add(MyNews);
            }
            dataGridView1.DataSource = NewsList.ToList();
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                try
                {
                    CSV_file output_file = new CSV_file(saveFileDialog1.FileName);
                    output_file.Add_row(News.CsvHeader());
                    foreach (News news in NewsList)
                        output_file.Add_row(news.ToList());
                    output_file.Save();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
