﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace US_Real_Fake_news_election
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TimeBox USTimeBox = new TimeBox();
            Controls.Add(USTimeBox);
            USTimeBox.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
