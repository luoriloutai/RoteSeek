using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoteSeek
{
    public partial class FormFindAndReplace : Form
    {
        //查找
        public delegate void FindDelegate(string find);
        public event FindDelegate FindEventHandler;

        //替换 - 单个替换
        public delegate void ReplaceDelegate(string oldValue,string newValue);
        public event ReplaceDelegate ReplaceEventHandler;

        //全部替换
        public delegate void ReplaceAllDelegate(string oldValue,string newValue);
        public event ReplaceAllDelegate ReplaceAllEventHandler;
       
        public FormFindAndReplace()
        {
            InitializeComponent();
        }

        //查找事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbxFind.Text == "")
            {
                MessageBox.Show("查询内容不能为空，请输入要查询的内容！", "提示 - 查询内容为空", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; 
            }
            FindEventHandler(tbxFind.Text);
        }



        //替换事件
        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (tbxFind.Text == "")
            {
                MessageBox.Show("查询内容不能为空，请输入查询内容！", "提示 - 输入查询内容", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReplaceEventHandler(tbxFind.Text,tbxReplace.Text);
        }



        //全部替换
        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            if (tbxFind.Text == "")
            {
                MessageBox.Show("查询内容不能为空，请输入查询内容！", "提示 - 输入查询内容", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReplaceAllEventHandler(tbxFind.Text, tbxReplace.Text);
        }


        //取消替换
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //指示是否区分大小写
        public bool IsIgnoreCapital()
        {
            if (cbxIgnore.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
