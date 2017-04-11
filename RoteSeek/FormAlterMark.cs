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
    public partial class FormAlterMark : Form
    {
        public delegate void AlterMarkDelegate(string markName);
        public event AlterMarkDelegate AlterMarkHandler;

        public FormAlterMark()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.tbxNewMark.Text == "")
            {
                MessageBox.Show("新书签名称不能为空！", "提示 - 新书签名称为空",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            AlterMarkHandler(this.tbxNewMark.Text);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
