using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class TimeBox
    {
        TimeBox : GroupBox
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
            Left = 5;
            Top = 5;
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
}

