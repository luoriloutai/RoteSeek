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
    public partial class FormAddMark : Form
    {
        string text= "";
        int createState = 1;

        public FormAddMark()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            text = this.tbxMarkName.Text;
            if (text == "")
            {
                MessageBox.Show("书签内容不能为空,请重新输入书签名称!", "提示 - 书签名称为空");
                return;
            }
            this.Close();
        }


        //取消按钮事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            createState = 0;
            this.Close();
        }

        /// <summary>
        /// 获取创建的书签名称字符串
        /// </summary>
        /// <returns></returns>
        public string GetMarkString()
        {
            return text;
        }

        /// <summary>
        /// 获取创建书签的状态，为1时表示创建成功,为0时表示未创建成功
        /// </summary>
        /// <returns></returns>
        public int GetCreateState()
        {
            return createState;
        }
    }
}
