using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace RoteSeek
{
    public partial class FormSet : Form
    {
        public delegate void SetDelegate();
        public event SetDelegate SetEventHandler;

        public FormSet()
        {
            InitializeComponent();
        }

        private void lblBackColor_Click(object sender, EventArgs e)
        {
            DialogResult r= this.colorDialog.ShowDialog();
            if (r == DialogResult.OK)
            {
                lblBackColor.BackColor = this.colorDialog.Color;
            }
        }

        private void lblForeColor_Click(object sender, EventArgs e)
        {
            DialogResult r = this.colorDialog.ShowDialog();
            if (r == DialogResult.OK)
            {
                lblForeColor.BackColor = this.colorDialog.Color;
            }
        }


        //颜色设置
        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            DialogResult r= this.fontDialog.ShowDialog();
            if (r == DialogResult.OK)
            {
                string font = fontDialog.Font.ToString();
                int p = font.IndexOf("=")+1;
                int ed = font.IndexOf(",");
                font = font.Substring(p, ed - p);
                lblFontSet.Text = font;
            }
        }


        private void FormSet_Load(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\Info\cfg.xml");

            //背景色
            XmlNode node = doc.SelectSingleNode("File/User/BackColor");
            XmlAttributeCollection xac= node.Attributes;
            string red = xac["R"].Value;
            string green = xac["G"].Value;
            string blue = xac["B"].Value;
            this.lblBackColor.BackColor = Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));
            
            //前景色
            node = doc.SelectSingleNode("File/User/ForeColor");
            xac=node.Attributes;
            red = xac["R"].Value;
            green = xac["G"].Value;
            blue = xac["B"].Value;
            this.lblForeColor.BackColor = Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));


            //字体信息
            node = doc.SelectSingleNode("File/User/Font");
            xac= node.Attributes;
            string family = xac["family"].Value;
            //string se = xac["size"].Value;
            //double size = Convert.ToDouble(se);
            //float s = (float)size;
            //Font f = new Font(family, s);
            lblFontSet.Text = family;

            //编辑器文本距边框的距离
            node = doc.SelectSingleNode("File/User/SelectionIndent");
            xac = node.Attributes;
            string left = xac["left"].Value;
            string right = xac["right"].Value;
            this.tbxLeft.Text = left;
            this.tbxRight.Text = right;
        }

        //恢复默认
        private void Restore()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\Info\cfg.xml");

            //背景色
            XmlNode node = doc.SelectSingleNode("File/Default/BackColor");
            XmlAttributeCollection xac = node.Attributes;
            string red = xac["R"].Value;
            string green = xac["G"].Value;
            string blue = xac["B"].Value;
            this.lblBackColor.BackColor = Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));

            //前景色
            node = doc.SelectSingleNode("File/Default/ForeColor");
            xac = node.Attributes;
            red = xac["R"].Value;
            green = xac["G"].Value;
            blue = xac["B"].Value;
            this.lblForeColor.BackColor = Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));


            //字体信息
            node = doc.SelectSingleNode("File/Default/Font");
            xac = node.Attributes;
            string family = xac["family"].Value;
            //string se = xac["size"].Value;
            //double size = Convert.ToDouble(se);
            //float s = (float)size;
            //Font f = new Font(family, s);
            lblFontSet.Text = family;

            //编辑器文本距边框的距离
            node = doc.SelectSingleNode("File/Default/SelectionIndent");
            xac = node.Attributes;
            string left = xac["left"].Value;
            string right = xac["right"].Value;
            this.tbxLeft.Text = left;
            this.tbxRight.Text = right;
        }

        //写入记录
        private void WriteProfile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\Info\cfg.xml");
            
            XmlNode node = doc.SelectSingleNode("File/User/BackColor");
            XmlAttributeCollection xac= node.Attributes;
            xac["R"].Value=this.lblBackColor.BackColor.R.ToString();
            xac["G"].Value=this.lblBackColor.BackColor.G.ToString() ;
            xac["B"].Value=this.lblBackColor.BackColor.B.ToString();
            node = doc.SelectSingleNode("File/User/ForeColor");
            xac = node.Attributes;
            xac["R"].Value = this.lblForeColor.BackColor.R.ToString();
            xac["G"].Value = this.lblForeColor.BackColor.G.ToString();
            xac["B"].Value = this.lblForeColor.BackColor.B.ToString();
            node = doc.SelectSingleNode("File/User/Font");
            xac = node.Attributes;
            string font = xac["family"].Value = this.lblFontSet.Text;
            node = doc.SelectSingleNode("File/User/SelectionIndent");
            xac = node.Attributes;
            xac["left"].Value = this.tbxLeft.Text;
            xac["right"].Value = this.tbxRight.Text;
            doc.Save(@"..\..\Info\cfg.xml");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string nums = "0123456789";
            char[] input = this.tbxLeft.Text.ToCharArray();
            int count = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j] == nums[i])
                    {
                        count++;
                    }
                }
            }
            if (count != input.Length)
            {
                MessageBox.Show("文本左边距不全是数字，请输入有效数字！","提示 - 输入格式错误");
                return;
            }

            input = this.tbxRight.Text.ToCharArray();
            count = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j] == nums[i])
                    {
                        count++;
                    }
                }
            }
            if (count != input.Length)
            {
                MessageBox.Show("文本右边距不全是数字，请输入有效数字！", "提示 - 输入格式错误");
                return;
            }
            
            WriteProfile();
            SetEventHandler();
            this.Close();
        }




        private void btnRestore_Click(object sender, EventArgs e)
        {
            Restore();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
