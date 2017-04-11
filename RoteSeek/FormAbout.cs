using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RoteSeek
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            string path= Path.GetFullPath(@"..\..\Media\nature.mp3");
            this.wmp.URL = path;
            this.wmp.Visible = false;
        }

        private void FormAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.wmp.URL = "";
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            int hours = time.Hour;
            if (hours > 0 && hours <= 8)
            {
                this.lblTip.Text = "早上好！现在时间是\n " + time;
            }
            else if (hours>8&& hours <= 12)
            {
                this.lblTip.Text = "上午好！现在时间是\n "+ time;
            }
            else if (hours >12&&hours<=20)
            {
                this.lblTip.Text = "下午好！现在时间是\n " + time;
            }
            else if (hours > 20&&hours <23)
            {
                this.lblTip.Text = "晚上好！现在时间是\n " + time;
            }
            else
            {
                this.lblTip.Text = "夜深了，休息吧！现在时间是\n " + time;
            }
        }
    }
}
