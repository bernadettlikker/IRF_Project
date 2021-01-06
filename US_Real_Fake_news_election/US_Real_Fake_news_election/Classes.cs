using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace US_Real_Fake_news_election
{
    class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Label { get; set; }
        public News(XElement row)
        {
            Id = (int)row.Element("id");
            Title = (string)row.Element("title");
            Text = (string)row.Element("text");
            Label = (string)row.Element("label");
        }
    }
    class TimeBox : GroupBox
    {
                
        private readonly USDateTime UsEastTime = new USDateTime(TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
        private readonly USDateTime UsPacificTime = new USDateTime(TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        public Timer UsTimer { set; get; }

        public TimeBox()
        {
            Timer timer = new Timer
            {
                Interval = 1000,
            };
            timer.Tick += OnTimedEvent;
            UsTimer = timer;

            Label label1 = new Label
            {
                Top = 20,
                Left = 5,
                Width = 70,
                Text = "East Coast:"
            };

            Label label_east_coast = new Label
            {
                Top = 20,
                Left = 85,
                Width = 120,
                Name = "label_east_coast",
                Text = Convert.ToString(UsEastTime.LocalTime)

            };

            Label label2 = new Label
            {
                Top = 45,
                Left = 5,
                Width = 70,
                Text = "Pacific:"
            };

            Label label_pacific = new Label
            {
                Top = 45,
                Left = 85,
                Width = 120,
                Name = "label_pacific",
                Text = Convert.ToString(UsPacificTime.LocalTime)
            };

            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(label_east_coast);
            Controls.Add(label_pacific);
            
            Left = 10;
            Top = 10;
            Width = 210;
            Height = 70;
            Text = "US Local Time";

        }
        public void Start()
        {
            UsTimer.Enabled = true;
            UsTimer.Start();
        }
        private void OnTimedEvent(object sender, EventArgs e)
        {
            ((Label)Controls.Find("label_east_coast", false)[0]).Text = Convert.ToString(UsEastTime.LocalTime);
            ((Label)Controls.Find("label_pacific", false)[0]).Text = Convert.ToString(UsPacificTime.LocalTime);
        }
        public DateTime LocalTime(TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }
    }
    public struct USDateTime
    {
        public TimeZoneInfo TimeZone { get; }
        public USDateTime(TimeZoneInfo timeZone)
        {
            TimeZone = timeZone;
        }
        public DateTime LocalTime
        {
            get
            {
                return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZone);
            }
        }
    }

    class FilterBox : GroupBox
    {
        private readonly RadioButton rbFakeNews;
        private readonly RadioButton rbRealNews;
        private readonly CheckBox cbAllNews;
        private readonly TextBox textBox;
        public string QueryString { get; set; }
        public string Query()
        {
            return "";
        }

        public FilterBox()
        {
            Text = "Filters";
            Width = 255;
            Height = 110;
            Left = 300;
            Top = 10;
            Label labelFilter = new Label
            {
                Left = 10,
                Top = 35,
                Text = "Filter",
                Width = 30
            };
            textBox = new TextBox
            {
                Left = 45,
                Top = 30,
                Width = 200
            };
            cbAllNews = new CheckBox
            {
                Left = 10,
                Top = 75,
                Text = "All News",
                Checked = true,
                Width = 70
            };
            rbFakeNews = new RadioButton
            {
                Left = 85,
                Top = 75,
                Text = "Fake News",
                Width = 80,
                Enabled = false
            };
            rbRealNews = new RadioButton
            {
                Left = 165,
                Top = 75,
                Text = "Real News",
                Width = 80,
                Enabled = false
            };
            cbAllNews.CheckedChanged += CbAllNew_CheckedChanged;
            Controls.Add(labelFilter);
            Controls.Add(textBox);
            Controls.Add(cbAllNews);
            Controls.Add(rbFakeNews);
            Controls.Add(rbRealNews);

            private void CbAllNew_CheckedChanged(object sender, EventArgs e)
            {
                rbFakeNews.Enabled = (sender as CheckBox).CheckState == CheckState.Unchecked;
                rbRealNews.Enabled = (sender as CheckBox).CheckState == CheckState.Unchecked;
            }

            public IEnumerable<XElement> FilteredData(XElement data)
            {
                IEnumerable<XElement> Data = null;
                if (textBox.Text == "" && cbAllNews.Checked)
                    Data = from element in data.Elements() select element;
                else
                {
                    if (cbAllNews.Checked)
                        Data = from element in data.Elements()
                               where ((string)element.Element("title")).Contains(textBox.Text)
                               select element;
                    else
                    {
                        Data = from element in data.Elements()
                               where ((string)element.Element("title")).Contains(textBox.Text) &&
                               (string)element.Element("label") == (rbFakeNews.Checked ? "FAKE" : "REAL")
                               select element;
                    }
                }
                return Data;
            }

        }
    }

