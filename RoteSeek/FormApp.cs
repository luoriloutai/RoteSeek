using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Media;
using System.Runtime.InteropServices;


namespace RoteSeek
{ /*namespace-----------------------------------*/
    public partial class FormApp : Form
    { /*class-----------------------------------*/

        //当前文档状态，表示是否进行了编辑。为1时表示进行了编辑。
        private int fileState = 0;

        //所有书签的文本容器
        private List<string> markAll = new List<string>(5);

        //所有的定位信息容器
        private List<int> locAll = new List<int>(5);

        //所有的书签的句柄容器
        private List<IntPtr> markAllHandles = new List<IntPtr>(5);

        //已保存过的文档
        private string saved = "未保存";

        //已打开的文档
        private string opend = "未打开";


        //当前选择书签的句柄
        private IntPtr curMarkHandle = IntPtr.Zero;

        //上一个选择的书签
        private IntPtr preMarkHandle = IntPtr.Zero;

        //记录书签个数
        private int markCount = 0;

        //记录上一次操作的书签句柄
        private List<IntPtr> prevHandles = new List<IntPtr>(3);

        //执行查询的次数
        private int excCount = 0;

        //记录查询窗口
        private FormFindAndReplace ffr = null;

        public FormApp()
        {
            InitializeComponent();
            InitializePrograme();
        }


        //加载配置
        private void InitializePrograme()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\Info\cfg.xml");

            //背景色
            XmlNode node = doc.SelectSingleNode("File/User/BackColor");
            XmlAttributeCollection xac = node.Attributes;
            string red = xac["R"].Value;
            string green = xac["G"].Value;
            string blue = xac["B"].Value;
            this.txtContainer.BackColor = Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));

            //前景色
            node = doc.SelectSingleNode("File/User/ForeColor");
            xac = node.Attributes;
            red = xac["R"].Value;
            green = xac["G"].Value;
            blue = xac["B"].Value;
            this.txtContainer.ForeColor = Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));


            //字体信息
            node = doc.SelectSingleNode("File/User/Font");
            xac = node.Attributes;
            string family = xac["family"].Value;
            string se = xac["size"].Value;
            double size = Convert.ToDouble(se);
            float s = (float)size;
            Font f = new Font(family, s);
            this.txtContainer.Font = f;

            //编辑器文本距边框的距离
            node = doc.SelectSingleNode("File/User/SelectionIndent");
            xac = node.Attributes;
            string left = xac["left"].Value;
            int inLeft = Convert.ToInt32(left);
            string right = xac["right"].Value;
            int inRight = Convert.ToInt32(right);
            this.txtContainer.SelectionIndent = inLeft;
            this.txtContainer.SelectionRightIndent = inRight;
        }


        private void FormApp_Load(object sender, EventArgs e)
        {
            
            txtContainer.Select();
            fileState = 0;
            this.Text = "未保存文档 - RoteSeek";
        }

        //
        //“关闭目录”按钮样式设置
        //
        private void pbxContentsClose_MouseEnter(object sender, EventArgs e)
        {
            pbxContentsClose.Image = Properties.Resources.btnCloseEnter;
        }

        private void pbxContentsClose_MouseLeave(object sender, EventArgs e)
        {
            pbxContentsClose.Image = Properties.Resources.btnClose;
        }
        
        
        //
        //关闭目录导航
        //
        private void pbxContentsClose_Click(object sender, EventArgs e)
        {
            spcContent.Panel1Collapsed = true;
            tShowNav.Enabled = true;
            mShowNav.Enabled = true;
            tHideNav.Enabled = false;
            mHideNav.Enabled = false;
        }

        //
        //工具栏显示目录按钮事件,菜单事件也使用该方法
        //
        private void tShowNav_Click(object sender, EventArgs e)
        {
            spcContent.Panel1Collapsed = false;
            tHideNav.Enabled = true;
            mHideNav.Enabled = true;
            tShowNav.Enabled = false;
            mShowNav.Enabled = false;
        }

        //
        //工具栏隐藏目录按钮事件，菜单事件也使用该方法
        //
        private void tHideNav_Click(object sender, EventArgs e)
        {
            spcContent.Panel1Collapsed = true;
            tHideNav.Enabled = false;
            mHideNav.Enabled = false;
            tShowNav.Enabled = true;
            mShowNav.Enabled = true;
        }

        //
        //打开文件
        //
        private void tOpFile_Click(object sender, EventArgs e)
        {
            DialogResult r = opd.ShowDialog();
           
            if (r == DialogResult.OK)
            {
                if (fileState == 1)
                {
                    DialogResult result= MessageBox.Show("您已对当前文档进行了编辑操作，但未保存，是否保存？", "提示 - 保存文档",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        int state= SaveFile();
                        if (state == 1)
                        {
                            ExOpenFile();
                        }
                    }
                    if (result == DialogResult.No)
                    {
                        ExOpenFile();
                    }
                    
                }
                else
                {
                    ExOpenFile();
                }
                

                
            }
           
        }

        /// <summary>
        /// 打开文件的方法
        /// </summary>
        private void ExOpenFile()
        {
            string filePath = opd.FileName;
            if (File.Exists(filePath))
            {
                if (opend != filePath)
                {
                    saved = filePath;    //记录保存文档
                    opend = filePath;    //记录打开文档
                    txtContainer.Select();    //将控件激活
                    int id = filePath.LastIndexOf('.');
                    string fType = filePath.Substring(id + 1);    //文件后缀名
                    id = filePath.LastIndexOf(@"\");
                    string name = filePath.Substring(id + 1);    //文件名称，不包括全路径
                    lblContentTitle.Text = "正文 - " + name + "(已打开)";
                    this.Text = filePath + " - RoteSeek";    //设置窗体标题

                    //
                    //Words格式文件读取方法
                    //
                    if (fType.ToLower() == "words")
                    {
                        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                        BinaryReader br = new BinaryReader(fs, Encoding.UTF8);
                        //读取内容
                        try
                        {
                            if (lblNoMark != null)
                            {
                                pnlMark.Controls.Remove(lblNoMark);    //移除第一个标志性书签
                            }
                            if (prevHandles.Count > 0)
                            {
                                Control c = null;

                                //若前次执行的书签句柄存在，则清空List
                                int count = prevHandles.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    c = FromHandle(prevHandles[i]);
                                    pnlMark.Controls.Remove(c);

                                }
                                for (int m = 0; m < count; m++)
                                {
                                    prevHandles.RemoveAt(0);
                                }
                            }
                            if (markAllHandles.Count > 0)
                            {
                                Control c = null;
                                int count = markAllHandles.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    c = FromHandle(markAllHandles[i]);
                                    pnlMark.Controls.Remove(c);    //控件
                                }
                                for (int m = 0; m < count; m++)
                                {
                                    markAllHandles.RemoveAt(0);    //句柄
                                    markAll.RemoveAt(0);    //文本
                                    locAll.RemoveAt(0);    //定位信息
                                }
                            }

                            markCount = 0;    //标签数量清零

                            string s = br.ReadString();
                            string format = s.Substring(0, 5);
                            if (format != "words")
                            {
                                DialogResult dr = MessageBox.Show("该文件不是标准的Words文件，可能无法正常显示！是否继续打开？", "提示 - 文件格式不正确", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == DialogResult.No)
                                {
                                    return;    //若选择了不打开则直接返回
                                }
                            }
                            int start = s.IndexOf(":");
                            int end = s.IndexOf("#");
                            int locEnd = s.IndexOf("$");
                            if (end - start > 1)
                            {
                                //书签文本字符串
                                string info = s.Substring(start + 1, end - 1 - start - 1);

                                string[] markInfo = info.Split('|');    //所有的书签信息

                                //将书签信息加入Treeview中
                                for (int j = 0; j < markInfo.Length; j++)
                                {
                                    Label lblMark = new Label();
                                    lblMark.Width = pnlMark.Width - 8;
                                    lblMark.Location = new Point(4, j * lblMark.Height + 4);
                                    lblMark.Padding = new Padding(5);
                                    lblMark.TextAlign = ContentAlignment.MiddleLeft;
                                    lblMark.ContextMenuStrip = cmsMark;
                                    string marText = markInfo[j].Replace("/", "|");
                                    marText = marText.Replace("*", "#");
                                    marText = marText.Replace("&", "$");
                                    lblMark.Text = marText;
                                    lblMark.AutoEllipsis = true;
                                    lblMark.Click += new EventHandler(lblMark_Click);
                                    lblMark.MouseEnter += new EventHandler(lblMark_MouseEnter);
                                    lblMark.MouseLeave += new EventHandler(lblMark_MouseLeave);
                                    pnlMark.Controls.Add(lblMark);    //添加控件
                                    markAllHandles.Add(lblMark.Handle);    //记录句柄
                                    markAll.Add(markInfo[j]);    //记录书签文本
                                    markCount++;    //记录书签个数
                                }

                                //定位信息字符串
                                info = s.Substring(end + 1, locEnd - 1 - end - 1);
                                markInfo = info.Split('|');    //所有的定位信息

                                for (int u = 0; u < markInfo.Length; u++)
                                {
                                    int pos = Convert.ToInt32(markInfo[u]);
                                    locAll.Add(pos);    //记录定位信息
                                }
                            }
                            else
                            {
                                Label lblMark = new Label();
                                lblMark.Width = pnlMark.Width - 8;
                                lblMark.Location = new Point(4, 4);
                                lblMark.Padding = new Padding(5);
                                lblMark.TextAlign = ContentAlignment.MiddleLeft;
                                lblMark.Text = "无书签";
                                pnlMark.Controls.Add(lblMark);
                                prevHandles.Add(lblMark.Handle);
                            }

                            int st = s.IndexOf("$");
                            txtContainer.Text = s.Substring(st + 1);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("读取文件时发生异常，文件可能无法正常显示！" + ex.Message, "读取错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        finally
                        {
                            br.Close();
                            fs.Close();
                        }
                    }
                    //
                    //普通文本文件读取
                    //
                    else
                    {
                        if (lblNoMark != null)
                        {
                            pnlMark.Controls.Remove(lblNoMark);
                        }
                        if (prevHandles.Count > 0)
                        {
                            Control c = null;
                            int count = prevHandles.Count;
                            for (int i = 0; i < count; i++)
                            {
                                c = FromHandle(prevHandles[i]);
                                pnlMark.Controls.Remove(c);
                            }
                            for (int m = 0; m < count; m++)
                            {
                                prevHandles.RemoveAt(0);
                            }
                        }
                        if (markAllHandles.Count > 0)
                        {
                            Control c = null;
                            int count = markAllHandles.Count;
                            for (int i = 0; i < count; i++)
                            {
                                c = FromHandle(markAllHandles[i]);
                                pnlMark.Controls.Remove(c);    //控件
                            }
                            for (int m = 0; m < count; m++)
                            {
                                markAllHandles.RemoveAt(0);    //句柄
                                markAll.RemoveAt(0);    //文本
                                locAll.RemoveAt(0);    //定位信息
                            }
                        }

                        markCount = 0;

                        Label lblMark = new Label();
                        lblMark.Width = pnlMark.Width - 8;
                        lblMark.Location = new Point(4, 4);
                        lblMark.Padding = new Padding(5);
                        lblMark.TextAlign = ContentAlignment.MiddleLeft;
                        lblMark.Text = "无书签";
                        pnlMark.Controls.Add(lblMark);
                        prevHandles.Add(lblMark.Handle);
                        StreamReader sr = new StreamReader(filePath, Encoding.Default);
                        try
                        {
                            txtContainer.Text = sr.ReadToEnd();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("读取文件时发生异常，文件可能无法正常显示！" + ex.Message, "读取错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        finally
                        {
                            sr.Close();
                        }

                    }

                    fileState = 0;     //设置状态为未变化
                }

            }
        }



        /// <summary>
        /// 查找当前选择的书签的索引,返回-1表示未找到。
        /// </summary>
        /// <returns></returns>
        private int FindSelectMarkIndex()
        {
            int curIndex = -1;
            for (int i = 0; i < markAllHandles.Count; i++)
            {
                if (curMarkHandle == markAllHandles[i])
                {
                    curIndex = i;
                }
            }
            return curIndex;

        }


        /// <summary>
        /// 鼠标离开书签时的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblMark_MouseLeave(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            if (curMarkHandle != c.Handle)
            {
                c.BackColor = SystemColors.Window;
            }
        }



        /// <summary>
        /// 鼠标滑过书签时的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblMark_MouseEnter(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            if (curMarkHandle != c.Handle)
            {
                c.BackColor = Color.FromArgb(95, 255, 221, 111);
            }
        }


        /// <summary>
        /// 书签单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void lblMark_Click(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            curMarkHandle = c.Handle;    //记录当前句柄

            if (preMarkHandle != IntPtr.Zero)
            {
                if (curMarkHandle != preMarkHandle)
                {
                    c.BackColor = Color.FromArgb(255, 221, 111);
                    Control co = FromHandle(preMarkHandle);
                    co.BackColor = SystemColors.Window;

                    //记录前一个句柄
                    preMarkHandle = curMarkHandle;
                }
            }
            else
            {
                c.BackColor = Color.FromArgb(255, 221, 111);
                //记录前一个句柄
                preMarkHandle = curMarkHandle;
            }

            //定位
            int indx = FindSelectMarkIndex();
            if (indx != -1)
            {
                int lastLoc = locAll[indx];
                //int startPos = Convert.ToInt32(lastLoc);
                this.txtContainer.Select();
                this.txtContainer.Select(lastLoc, 15);
            }

        }

        //
        //新建文件
        //
        private void mNewFile_Click(object sender, EventArgs e)
        {
            if (fileState == 1)
            {
                DialogResult r = MessageBox.Show("当前文档进行了编辑，但未保存，是否进行保存？", "提示 - 保存文档", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    int state = SaveFile();    //先保存
                    if (state == 1)
                    {
                        txtContainer.Clear();    //清除文本
                        fileState = 0;    //文本状态恢复
                        opend = "未打开";    //恢复打开记录
                        saved = "未保存";    //恢复保存记录
                        this.Text = "未保存 - RoteSeek";
                        this.lblContentTitle.Text = "正文 - 文档未保存";

                        int count = prevHandles.Count;
                        Control ctl = null;
                        for (int i = 0; i < count; i++)
                        {
                            ctl = FromHandle(prevHandles[i]);
                            pnlMark.Controls.Remove(ctl);
                        }
                        for (int i = 0; i < count; i++)
                        {
                            prevHandles.RemoveAt(0);
                        }

                        int mCou = markAllHandles.Count;
                        for (int i = 0; i < mCou; i++)
                        {
                            ctl = FromHandle(markAllHandles[i]);
                            pnlMark.Controls.Remove(ctl);
                        }
                        for (int i = 0; i < mCou; i++)
                        {
                            markAllHandles.RemoveAt(0);
                            markAll.RemoveAt(0);
                            locAll.RemoveAt(0);
                        }
                        
                        Label lblMark = new Label();
                        lblMark.Width = pnlMark.Width - 8;
                        lblMark.Location = new Point(4, 4);
                        lblMark.Padding = new Padding(5);
                        lblMark.TextAlign = ContentAlignment.MiddleLeft;
                        lblMark.Text = "无书签";
                        pnlMark.Controls.Add(lblMark);
                        prevHandles.Add(lblMark.Handle);
                    }

                }
                if (r == DialogResult.Cancel)
                {
                    return;
                }
                if (r == DialogResult.No)
                {
                    txtContainer.Clear();    //清除文本
                    fileState = 0;    //文本状态恢复
                    opend = "未打开";    //恢复打开记录
                    saved = "未保存";    //恢复保存记录
                    this.Text = "未保存 - RoteSeek";
                    this.lblContentTitle.Text = "正文 - 文档未保存";

                    int count = prevHandles.Count;
                    Control ctl = null;
                    for (int i = 0; i < count; i++)
                    {
                        ctl = FromHandle(prevHandles[i]);
                        pnlMark.Controls.Remove(ctl);
                    }
                    for (int i = 0; i < count; i++)
                    {
                        prevHandles.RemoveAt(0);
                    }

                    int mCount = markAllHandles.Count;
                    for (int i = 0; i < mCount; i++)
                    {
                        ctl = FromHandle(markAllHandles[i]);
                        pnlMark.Controls.Remove(ctl);
                    }
                    for (int i = 0; i < mCount; i++)
                    {
                        markAllHandles.RemoveAt(0);
                        markAll.RemoveAt(0);
                        locAll.RemoveAt(0);
                    }

                    Label lblMark = new Label();
                    lblMark.Width = pnlMark.Width - 8;
                    lblMark.Location = new Point(4, 4);
                    lblMark.Padding = new Padding(5);
                    lblMark.TextAlign = ContentAlignment.MiddleLeft;
                    lblMark.Text = "无书签";
                    pnlMark.Controls.Add(lblMark);
                    prevHandles.Add(lblMark.Handle);
                }
                markCount=0;
            }
            else
            {
                txtContainer.Clear();    //清除文本
                fileState = 0;    //文本状态恢复
                opend = "未打开";    //恢复打开记录
                saved = "未保存";    //恢复保存记录
                this.Text = "未保存 - RoteSeek";
                this.lblContentTitle.Text = "正文 - 文档未保存";

                int count = prevHandles.Count;
                Control ctl = null;
                for (int i = 0; i < count; i++)
                {
                    ctl = FromHandle(prevHandles[i]);
                    pnlMark.Controls.Remove(ctl);
                }
                for (int i = 0; i < count; i++)
                {
                    prevHandles.RemoveAt(0);
                }

                int mcout=markAllHandles.Count;
                for (int i = 0; i < mcout; i++)
                {
                    ctl = FromHandle(markAllHandles[i]);
                    pnlMark.Controls.Remove(ctl);
                }
                for (int i = 0; i < mcout; i++)
                {
                    markAllHandles.RemoveAt(0);
                    markAll.RemoveAt(0);
                    locAll.RemoveAt(0);
                }

                Label lblMark = new Label();
                lblMark.Width = pnlMark.Width - 8;
                lblMark.Location = new Point(4, 4);
                lblMark.Padding = new Padding(5);
                lblMark.TextAlign = ContentAlignment.MiddleLeft;
                lblMark.Text = "无书签";
                pnlMark.Controls.Add(lblMark);
                prevHandles.Add(lblMark.Handle);
            }
            markCount = 0;    //恢复书签数量
        }

        /// <summary>
        /// 保存文档的方法
        /// </summary>
        private int SaveFile()
        {
            int saveState = 0;    //保存状态，若成功则返回1.
            if (saved != opend)    //要保存的文档未打开，即未执行过打开操作
            {
                DialogResult r = sfd.ShowDialog();
                if (r == DialogResult.OK)
                {
                    saved = sfd.FileName;    //记录保存的文档
                    opend = sfd.FileName;    //记录打开的文档,在执行保存操作后，文档既是打开的又是保存的
                    if (saved != "")
                    {
                        int id = saved.LastIndexOf('.');
                        string fType = saved.Substring(id + 1);
                        if (fType.ToLower() == "words")    //words文件写入
                        {
                            FileStream fs = new FileStream(saved, FileMode.Create, FileAccess.Write);
                            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);
                            try
                            {
                                string all = "words:" + GetMarkInfo() + "#" + GetLocInfo() + "$" + txtContainer.Text;
                                bw.Write(all);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("写入文件时发生异常，文件可能无法正常保存！" + ex.Message, "写入错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                bw.Close();
                                fs.Close();
                            }

                            this.Text = saved + " - RoteSeek";
                            int stPos = saved.LastIndexOf(@"\") + 1;
                            string name = saved.Substring(stPos);
                            this.lblContentTitle.Text = "正文 - " + name + "(已保存)";
                        }
                        else    //普通文件写入
                        {
                            StreamWriter sw = new StreamWriter(saved, false, Encoding.Default);
                            try
                            {
                                sw.Write(txtContainer.Text);
                            }
                            catch
                            {
                                MessageBox.Show("写入文件时发生异常，文件可能无法正常保存！", "写入错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                sw.Close();
                            }

                            this.Text = saved + " - RoteSeek";
                            int stPos = saved.LastIndexOf(@"\") + 1;
                            string name = saved.Substring(stPos);
                            this.lblContentTitle.Text = "正文 - " + name + "(已保存)";
                        }

                        saveState = 1;    //设置保存状态，此时为成功状态
                        fileState = 0;    //重设文本状态
                    }
                    
                }
            }
            else    //要保存的文档已打开
            {
                int id = saved.LastIndexOf('.');
                string fType = saved.Substring(id + 1);
                if (fType.ToLower() == "words")    //words文件保存方法
                {
                    FileStream fs = new FileStream(saved, FileMode.Create, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);
                    try
                    {
                        string all = "words:" + GetMarkInfo() + "#" + GetLocInfo() + "$" + txtContainer.Text;
                        bw.Write(all);
                        int stPos = saved.LastIndexOf(@"\") + 1;
                        string name = saved.Substring(stPos);
                        this.lblContentTitle.Text = "正文 - " + name + "(已保存)";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("写入文件时发生异常，文件可能无法正常保存！" + ex.Message, "写入错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        bw.Close();
                        fs.Close();
                    }
                }
                else    //普通文本文件保存方法
                {
                    StreamWriter sw = new StreamWriter(saved, false, Encoding.Default);
                    try
                    {
                        sw.Write(txtContainer.Text);
                        int stPos = saved.LastIndexOf(@"\") + 1;
                        string name = saved.Substring(stPos);
                        this.lblContentTitle.Text = "正文 - " + name + "(已保存)";
                    }
                    catch
                    {
                        MessageBox.Show("写入文件时发生异常，文件可能无法正常保存！", "写入错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sw.Close();
                    }
                }

                saveState = 1;    //保存成功
                fileState = 0;    //文本状态
            }
            return saveState;
        }

        /// <summary>
        /// 获取书签信息
        /// </summary>
        /// <returns></returns>
        private string GetMarkInfo()
        {
            string markInfo = "";
            for (int i = 0; i < markAll.Count; i++)
            {
                markInfo += markAll[i]+"|";
            }
            return markInfo;
        }

        /// <summary>
        /// 获取定位信息
        /// </summary>
        /// <returns></returns>
        private string GetLocInfo()
        {
            string locInfo = "";
            for (int i = 0; i < locAll.Count; i++)
            {
                locInfo += locAll[i]+"|";
            }
            return locInfo;
        }



        //
        //退出程序前提醒保存未保存的文档
        //
        private void FormApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ffr != null)
            {
                ffr.Close();
            }
            if (fileState == 1)
            {
                if(opend!="")
                {
                    int pos = opend.LastIndexOf(".");
                    string fType = saved.Substring(pos + 1);
                    if (fType == "words")
                    {
                        DialogResult r = MessageBox.Show("文档已经过编辑，但是未保存，是否保存？", "提示 - 保存文档", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (r == DialogResult.Yes)
                        {
                            SaveFile();
                        }
                        if (r == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        DialogResult r = MessageBox.Show("当前文档已进行了编辑或者添加了书签，是否保存？ -  如果您想保留书签信息，请将该文档保存或另存为words格式文件！", "提示 - 保存文档", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (r == DialogResult.Yes)
                        {
                            fileState = 0;
                            SaveFile();
                        }
                        if (r == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                        }
                    }
                }
                
            }
        }

        //
        //保存当前文档
        //
        private void tSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        //
        //退出程序
        //
        private void mExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //
        //另存为
        //
        private void mSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult r = sfd.ShowDialog();
            if (r == DialogResult.OK)
            {
                string path = sfd.FileName;
                if (path != "")
                {
                    int id = path.LastIndexOf('.');
                    string fType = path.Substring(id + 1);
                    if (fType.ToLower() == "words")    //words文件写入
                    {
                        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                        BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);
                        try
                        {
                            string all = "words:" + GetMarkInfo() + "#" + GetLocInfo() + "$" + txtContainer.Text;
                            bw.Write(all);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("写入文件时发生异常，文件可能无法正常保存！" + ex.Message, "写入错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            bw.Close();
                            fs.Close();
                        }

                        this.Text = path + " - RoteSeek";
                        int stPos = path.LastIndexOf(@"\") + 1;
                        string name = path.Substring(stPos);
                        this.lblContentTitle.Text = "正文 - " + name + "(已保存)";
                    }
                    else    //普通文件写入
                    {
                        StreamWriter sw = new StreamWriter(path, false, Encoding.Default);
                        try
                        {
                            sw.Write(txtContainer.Text);
                        }
                        catch
                        {
                            MessageBox.Show("写入文件时发生异常，文件可能无法正常保存！", "写入错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            sw.Close();
                        }

                        this.Text = path + " - RoteSeek";
                        int stPos = path.LastIndexOf(@"\") + 1;
                        string name = path.Substring(stPos);
                        this.lblContentTitle.Text = "正文 - " + name + "(已保存)";
                    }
                    opend = path;
                    saved = path;
                    fileState = 0;
                }
            }
        }

       
        //
        //撤消上一步编辑操作
        //
        private void mCancel_Click(object sender, EventArgs e)
        {
            this.txtContainer.Undo();
        }

        //
        //剪切操作
        //
        private void mCut_Click(object sender, EventArgs e)
        {
            this.txtContainer.Cut();
        }

        
        //
        //复制
        //
        private void mCopy_Click(object sender, EventArgs e)
        {
            this.txtContainer.Copy();
        }

        //
        //粘贴
        //
        private void mPaste_Click(object sender, EventArgs e)
        {
            this.txtContainer.Paste();
        }

        //
        //全选
        //
        private void mSelectAll_Click(object sender, EventArgs e)
        {
            this.txtContainer.SelectAll();
        }

        //
        //删除
        //
        private void mDel_Click(object sender, EventArgs e)
        {
            this.txtContainer.SelectedText = "";
        }

        //
        //文本变化后引发
        //
        private void txtContainer_TextChanged(object sender, EventArgs e)
        {
            this.fileState = 1; 
        }
        /// <summary>
        /// 删除全部内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mDelAll_Click(object sender, EventArgs e)
        {
            this.txtContainer.Clear();
        }


        /// <summary>
        /// 手动添加书签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mAddMark_Click(object sender, EventArgs e)
        {
            if (txtContainer.Text != "")
            {

                FormAddMark fam = new FormAddMark();
                fam.ShowIcon = false;
                fam.ShowDialog();
                string markString = fam.GetMarkString();
                int crtState = fam.GetCreateState();
                if (crtState == 0)
                {
                    return;
                }

                if (lblNoMark != null)
                {
                    pnlMark.Controls.Remove(lblNoMark);
                }
                //
                //对字符进行替换,当读取后对其进行相应解码，以恢复文本内容
                //
                if (markString != "")
                {
                    string mark = markString.Replace("|", "/");
                    mark = mark.Replace("#", "*");
                    mark = mark.Replace("$", "&");

                    if (prevHandles.Count > 0)
                    {
                        Control c = null;
                        int count = prevHandles.Count;
                        for (int i = 0; i < count; i++)
                        {
                            c = FromHandle(prevHandles[i]);
                            pnlMark.Controls.Remove(c);
                        }
                        for (int m = 0; m < count; m++)
                        {
                            prevHandles.RemoveAt(0);
                        }
                    }

                    Label lblMark = new Label();
                    lblMark.Width = pnlMark.Width - 8;
                    lblMark.Location = new Point(4, markCount * lblMark.Height + 4);
                    lblMark.Padding = new Padding(5);
                    lblMark.TextAlign = ContentAlignment.MiddleLeft;
                    lblMark.ContextMenuStrip = cmsMark;
                    lblMark.Text = markString;
                    lblMark.AutoEllipsis = true;
                    lblMark.Click += new EventHandler(lblMark_Click);
                    lblMark.MouseEnter += new EventHandler(lblMark_MouseEnter);
                    lblMark.MouseLeave += new EventHandler(lblMark_MouseLeave);
                    pnlMark.Controls.Add(lblMark);    //添加控件
                    markAllHandles.Add(lblMark.Handle);    //记录句柄
                    markAll.Add(mark);    //记录书签文本
                    markCount++;    //记录书签个数
                    locAll.Add(this.txtContainer.SelectionStart);    //记录当前定位信息

                    fileState = 1;
                }
            }
            else
            {
                MessageBox.Show("当前文档没有内容，不能添加书签！", "提示 - 不能添加书签", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 自动添加书签，该方法将光标当前位置作为定位点，所以应先将光标定位好
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mAutoCreateMark_Click(object sender, EventArgs e)
        {
            if (txtContainer.Text != "")
            {

                int pos = this.txtContainer.SelectionStart;
                string markString = "";
                if (txtContainer.Text.Length >= 15+pos)
                {
                    markString = this.txtContainer.Text.Substring(pos, 15);
                }
                else
                {
                    markString = txtContainer.Text;
                }

                pnlMark.Controls.Remove(lblNoMark);
                //
                //对字符进行替换,当读取后对其进行相应解码，以恢复文本内容
                //
                string mark = markString.Replace("|", "/");
                mark = mark.Replace("#", "*");
                mark = mark.Replace("$", "&");

                if (prevHandles.Count > 0)
                {
                    Control c = null;
                    int count = prevHandles.Count;
                    for (int i = 0; i < count; i++)
                    {
                        c = FromHandle(prevHandles[i]);
                        pnlMark.Controls.Remove(c);
                    }
                    for (int m = 0; m < count; m++)
                    {
                        prevHandles.RemoveAt(0);
                    }
                }

                Label lblMark = new Label();
                lblMark.Width = pnlMark.Width - 8;
                lblMark.Location = new Point(4, markCount * lblMark.Height + 4);
                lblMark.Padding = new Padding(5);
                lblMark.TextAlign = ContentAlignment.MiddleLeft;
                lblMark.ContextMenuStrip = cmsMark;
                lblMark.Text = markString;
                lblMark.AutoEllipsis = true;
                lblMark.Click += new EventHandler(lblMark_Click);
                lblMark.MouseEnter += new EventHandler(lblMark_MouseEnter);
                lblMark.MouseLeave += new EventHandler(lblMark_MouseLeave);
                pnlMark.Controls.Add(lblMark);    //添加控件
                markAllHandles.Add(lblMark.Handle);    //记录句柄
                markAll.Add(mark);    //记录书签文本
                markCount++;    //记录书签个数
                locAll.Add(this.txtContainer.SelectionStart);    //记录当前定位信息

                fileState = 1;
            }
            else
            {
                MessageBox.Show("当前文档没有内容，不能添加书签！", "提示 - 不能添加书签", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 删除当前书签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mDelMark_Click(object sender, EventArgs e)
        {
            if (curMarkHandle != IntPtr.Zero)
            {
                int cuIndex = FindSelectMarkIndex();
                if (cuIndex != -1)
                {
                    Control ct = null;
                    int count = prevHandles.Count;
                    for (int k = 0; k < count; k++)
                    {
                        ct = FromHandle(prevHandles[k]);
                        pnlMark.Controls.Remove(ct);
                    }

                    for (int j = 0; j < count; j++)
                    {
                        prevHandles.RemoveAt(0);
                    }

                    Control c = FromHandle(curMarkHandle);
                    pnlMark.Controls.Remove(c);
                    markAllHandles.RemoveAt(cuIndex);
                    markAll.RemoveAt(cuIndex);
                    locAll.RemoveAt(cuIndex);
                    markCount--;
                    for (int i = cuIndex; i < markAllHandles.Count; i++)
                    {
                        c = FromHandle(markAllHandles[i]);
                        c.Location = new Point(4, c.Location.Y - c.Height);
                    }

                    Label lblMark = new Label();
                    lblMark.Width = pnlMark.Width - 8;
                    lblMark.Location = new Point(4, 4);
                    lblMark.Padding = new Padding(5);
                    lblMark.TextAlign = ContentAlignment.MiddleLeft;
                    lblMark.Text = "无书签";
                    pnlMark.Controls.Add(lblMark);
                    prevHandles.Add(lblMark.Handle);

                    fileState = 1;

                    curMarkHandle = IntPtr.Zero;    //无书签
                }
            }
            else
            {
                MessageBox.Show("当前未选择任何书签，不能进行删除！", "提示 - 没有选择书签", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 删除全部书签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mDelAllMark_Click(object sender, EventArgs e)
        {
            if (markAllHandles.Count > 0)
            {
                Control c = null;
                int mrkCount = markAllHandles.Count;
                for (int i = 0; i < mrkCount; i++)
                {
                    c = FromHandle(markAllHandles[i]);
                    pnlMark.Controls.Remove(c);
                }

                for (int k = 0; k < mrkCount; k++)
                {
                    markAllHandles.RemoveAt(0);
                    markAll.RemoveAt(0);
                    locAll.RemoveAt(0);
                }
                int prvCount = prevHandles.Count;
                for (int j = 0; j < prvCount; j++)
                {
                    c = FromHandle(prevHandles[j]);
                    pnlMark.Controls.Remove(c);
                }

                for (int f = 0; f < prvCount; f++)
                {
                    prevHandles.RemoveAt(0);
                }

                Label lblMark = new Label();
                lblMark.Width = pnlMark.Width - 8;
                lblMark.Location = new Point(4, 4);
                lblMark.Padding = new Padding(5);
                lblMark.TextAlign = ContentAlignment.MiddleLeft;
                lblMark.Text = "无书签";
                pnlMark.Controls.Add(lblMark);
                prevHandles.Add(lblMark.Handle);

                fileState = 1;

                markCount = 0;    //书签0个

                curMarkHandle = IntPtr.Zero;    //当前无书签
            }
            else
            {
                MessageBox.Show("本文档没有书签，不能进行删除！", "提示 - 没有书签", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        /// <summary>
        /// 上一个书签按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mPrevMark_Click(object sender, EventArgs e)
        {
            int indx = FindSelectMarkIndex();
            if (indx != -1)
            {
                if (indx > 0)
                {
                    Control c = FromHandle(markAllHandles[indx - 1]);
                    curMarkHandle = markAllHandles[indx - 1];
                    //颜色设置
                    if (curMarkHandle != preMarkHandle)
                    {
                        c = FromHandle(curMarkHandle);
                        c.BackColor=Color.FromArgb(255, 221, 111);
                        c = FromHandle(preMarkHandle);
                        c.BackColor = SystemColors.Window;
                    }

                    this.txtContainer.Select();
                    this.txtContainer.Select(locAll[indx-1], 15);

                    preMarkHandle = curMarkHandle;
                }
                else
                {
                    MessageBox.Show("已经到了第一个书签！", "提示 - 无上一个书签",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("当前没有书签被激活，请选择书签！", "提示 - 无激活书签",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        /// <summary>
        /// 下一个书签按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mNextMark_Click(object sender, EventArgs e)
        {
            int indx = FindSelectMarkIndex();
            if (indx != -1)
            {
                if (indx < markAllHandles.Count-1)
                {
                    Control c = FromHandle(markAllHandles[indx + 1]);
                    curMarkHandle = markAllHandles[indx +1];
                    //颜色设置
                    if (curMarkHandle != preMarkHandle)
                    {
                        c = FromHandle(curMarkHandle);
                        c.BackColor = Color.FromArgb(255, 221, 111);
                        c = FromHandle(preMarkHandle);
                        c.BackColor = SystemColors.Window;
                    }

                    this.txtContainer.Select();
                    this.txtContainer.Select(locAll[indx + 1], 15);

                    preMarkHandle = curMarkHandle;
                }
                else
                {
                    MessageBox.Show("已经到了最后一个书签！", "提示 - 无下一个书签", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("当前没有书签被激活，请选择书签！", "提示 - 无激活书签", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //查找和替换
        private void mFind_Click(object sender, EventArgs e)
        {
            FormFindAndReplace fr = new FormFindAndReplace();
            ffr = fr;
            fr.FindEventHandler += new FormFindAndReplace.FindDelegate(fr_FindEventHandler);
            fr.ReplaceEventHandler += new FormFindAndReplace.ReplaceDelegate(fr_ReplaceEventHandler);
            fr.ReplaceAllEventHandler += new FormFindAndReplace.ReplaceAllDelegate(fr_ReplaceAllEventHandler);
            fr.TopMost = true;
            fr.Show();

        }


        /// <summary>
        /// 全部替换
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        void fr_ReplaceAllEventHandler(string oldValue, string newValue)
        {
            this.Select();
            txtContainer.Select();
            if (ffr.IsIgnoreCapital() == true)
            {
                string content = this.txtContainer.Text.ToLower();
                content = content.Replace(oldValue, newValue);
                this.txtContainer.Text = content;
            }
            else
            {
                string content = this.txtContainer.Text;
                content = content.Replace(oldValue, newValue);
                this.txtContainer.Text = content;
            }
        }


        /// <summary>
        /// 替换事件处理
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        void fr_ReplaceEventHandler(string oldValue, string newValue)
        {
            if (txtContainer.SelectedText != "")
            {
                this.Select();
                txtContainer.Select();
                if (ffr.IsIgnoreCapital() == true)
                {
                    string content = this.txtContainer.SelectedText.ToLower();
                    content = content.Replace(oldValue, newValue);
                    this.txtContainer.SelectedText = content;
                }
                else
                {
                    string content = this.txtContainer.SelectedText;
                    content = content.Replace(oldValue, newValue);
                    this.txtContainer.SelectedText = content;
                }
                
            }
            else
            {
                MessageBox.Show("您还没有进行查找操作，请先执行查找操作，然后再执行替换操作！", "提示 - 替换失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        /// <summary>
        /// 查询事件处理程序
        /// </summary>
        /// <param name="find"></param>
        void fr_FindEventHandler(string find)
        {
            bool isIgnor = ffr.IsIgnoreCapital();
            if (isIgnor == true)
            {
                char[] findString = find.ToCharArray();
                string textSource = txtContainer.Text.ToLower();
                List<int> findIndexes = new List<int>(5);
                for (int i = 0; i < textSource.Length; i++)
                {
                    if (findString.Length > 0)
                    {
                        if (findString[0].ToString().ToLower() == textSource[i].ToString().ToLower())
                        {
                            int charCount = 1;
                            for (int j = 1; j < findString.Length; j++)
                            {

                                if (j + i < textSource.Length)
                                {
                                    if (findString[j].ToString().ToLower() == textSource[i + j].ToString().ToLower())
                                    {
                                        charCount++;
                                    }
                                }
                            }

                            if (charCount == findString.Length)
                            {
                                findIndexes.Add(i);
                            }
                        }
                    }

                }


                if (findIndexes.Count > 0)
                {
                    if (excCount < findIndexes.Count)
                    {
                        this.Select();
                        txtContainer.Select();
                        txtContainer.Select(findIndexes[excCount], findString.Length);
                        excCount++;

                    }
                    else
                    {
                        excCount = 0;
                        MessageBox.Show("已经查询到文档末尾，再次查询将从文档起始位置开始！", "提示 - 已经查询到文档末尾", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("没有找到查询内容！", "提示 - 没有匹配内容", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                char[] findString = find.ToCharArray();
                string textSource = txtContainer.Text;
                List<int> findIndexes = new List<int>(5);
                for (int i = 0; i < textSource.Length; i++)
                {
                    if (findString.Length > 0)
                    {
                        if (findString[0] == textSource[i])
                        {
                            int charCount = 1;
                            for (int j = 1; j < findString.Length; j++)
                            {

                                if (j + i < textSource.Length)
                                {
                                    if (findString[j] == textSource[i + j])
                                    {
                                        charCount++;
                                    }
                                }
                            }

                            if (charCount == findString.Length)
                            {
                                findIndexes.Add(i);
                            }
                        }
                    }

                }


                if (findIndexes.Count > 0)
                {
                    if (excCount < findIndexes.Count)
                    {
                        this.Select();
                        txtContainer.Select();
                        txtContainer.Select(findIndexes[excCount], findString.Length);
                        excCount++;

                    }
                    else
                    {
                        excCount = 0;
                        MessageBox.Show("已经查询到文档末尾，再次查询将从文档起始位置开始！", "提示 - 已经查询到文档末尾", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("没有找到查询内容！", "提示 - 没有匹配内容", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }


        /// <summary>
        /// 修改书签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mAlterMark_Click(object sender, EventArgs e)
        {
            FormAlterMark fam = new FormAlterMark();
            fam.AlterMarkHandler += new FormAlterMark.AlterMarkDelegate(fam_AlterMarkHandler);
            fam.ShowDialog();
            
        }


        //修改书签处理程序
        void fam_AlterMarkHandler(string markName)
        {
            string  newMarkName = markName;
            //fAlter = fam;
            int i = FindSelectMarkIndex();
            if (i != -1)
            {
                if (newMarkName != "")
                {
                    markAll[i] = newMarkName;    //将新书签名称变更到容器中
                    Control cl = FromHandle(markAllHandles[i]);
                    cl.Text = newMarkName;    //将新内容显示到书签上
                }
                fileState = 1;
            }
            else
            {
                MessageBox.Show("当前没有书签被激活，不能进行修改,请先选择书签！", "提示 - 无激活书签", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void tsmi20_Click(object sender, EventArgs e)
        {
            this.txtContainer.ZoomFactor = 0.2f;
        }

        private void tsmi70_Click(object sender, EventArgs e)
        {
            this.txtContainer.ZoomFactor = 0.7f;
        }

        private void tsmi100_Click(object sender, EventArgs e)
        {
            this.txtContainer.ZoomFactor = 1.0f;
        }

        private void tsmi150_Click(object sender, EventArgs e)
        {
            this.txtContainer.ZoomFactor = 1.5f;
        }

        private void tsmi200_Click(object sender, EventArgs e)
        {
            this.txtContainer.ZoomFactor = 2.0f;
        }

        private void tsmi400_Click(object sender, EventArgs e)
        {
            this.txtContainer.ZoomFactor = 4.0f;
        }


        //设置
        private void menuSet_Click(object sender, EventArgs e)
        {
            FormSet fst = new FormSet();
            fst.SetEventHandler += new FormSet.SetDelegate(fst_SetEventHandler);
            fst.ShowDialog();
        }


        //重新加载
        private void Reload()
        {
            if(File.Exists(opend))
            {
                int id = opend.LastIndexOf('.');
                string fType = opend.Substring(id + 1);    //文件后缀名
                id = opend.LastIndexOf(@"\");
                string name = opend.Substring(id + 1);    //文件名称，不包括全路径
                //lblContentTitle.Text = "正文 - " + name + "(已打开)";
                //this.Text = opend + " - RoteSeek";    //设置窗体标题


                //
                //Words格式文件读取方法
                //
                if (fType.ToLower() == "words")
                {
                    FileStream fs = new FileStream(opend, FileMode.Open, FileAccess.ReadWrite);
                    BinaryReader br = new BinaryReader(fs, Encoding.UTF8);
                    //读取内容
                    try
                    {
                        if (lblNoMark != null)
                        {
                            pnlMark.Controls.Remove(lblNoMark);    //移除第一个标志性书签
                        }
                        if (prevHandles.Count > 0)
                        {
                            Control c = null;

                            //若前次执行的书签句柄存在，则清空List
                            int count = prevHandles.Count;
                            for (int i = 0; i < count; i++)
                            {
                                c = FromHandle(prevHandles[i]);
                                pnlMark.Controls.Remove(c);

                            }
                            for (int m = 0; m < count; m++)
                            {
                                prevHandles.RemoveAt(0);
                            }
                        }
                        if (markAllHandles.Count > 0)
                        {
                            Control c = null;
                            int count = markAllHandles.Count;
                            for (int i = 0; i < count; i++)
                            {
                                c = FromHandle(markAllHandles[i]);
                                pnlMark.Controls.Remove(c);    //控件
                            }
                            for (int m = 0; m < count; m++)
                            {
                                markAllHandles.RemoveAt(0);    //句柄
                                markAll.RemoveAt(0);    //文本
                                locAll.RemoveAt(0);    //定位信息
                            }
                        }

                        markCount = 0;    //标签数量清零

                        string s = br.ReadString();
                        string format = s.Substring(0, 5);
                        if (format != "words")
                        {
                            DialogResult dr = MessageBox.Show("该文件不是标准的Words文件，可能无法正常显示！是否继续打开？", "提示 - 文件格式不正确", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dr == DialogResult.No)
                            {
                                return;    //若选择了不打开则直接返回
                            }
                        }
                        int start = s.IndexOf(":");
                        int end = s.IndexOf("#");
                        int locEnd = s.IndexOf("$");
                        if (end - start > 1)
                        {
                            //书签文本字符串
                            string info = s.Substring(start + 1, end - 1 - start - 1);

                            string[] markInfo = info.Split('|');    //所有的书签信息

                            //将书签信息加入Treeview中
                            for (int j = 0; j < markInfo.Length; j++)
                            {
                                Label lblMark = new Label();
                                lblMark.Width = pnlMark.Width - 8;
                                lblMark.Location = new Point(4, j * lblMark.Height + 4);
                                lblMark.Padding = new Padding(5);
                                lblMark.TextAlign = ContentAlignment.MiddleLeft;
                                lblMark.ContextMenuStrip = cmsMark;
                                string marText = markInfo[j].Replace("/", "|");
                                marText = marText.Replace("*", "#");
                                marText = marText.Replace("&", "$");
                                lblMark.Text = marText;
                                lblMark.AutoEllipsis = true;
                                lblMark.Click += new EventHandler(lblMark_Click);
                                lblMark.MouseEnter += new EventHandler(lblMark_MouseEnter);
                                lblMark.MouseLeave += new EventHandler(lblMark_MouseLeave);
                                pnlMark.Controls.Add(lblMark);    //添加控件
                                markAllHandles.Add(lblMark.Handle);    //记录句柄
                                markAll.Add(markInfo[j]);    //记录书签文本
                                markCount++;    //记录书签个数
                            }

                            //定位信息字符串
                            info = s.Substring(end + 1, locEnd - 1 - end - 1);
                            markInfo = info.Split('|');    //所有的定位信息

                            for (int u = 0; u < markInfo.Length; u++)
                            {
                                int pos = Convert.ToInt32(markInfo[u]);
                                locAll.Add(pos);    //记录定位信息
                            }
                        }
                        else
                        {
                            Label lblMark = new Label();
                            lblMark.Width = pnlMark.Width - 8;
                            lblMark.Location = new Point(4, 4);
                            lblMark.Padding = new Padding(5);
                            lblMark.TextAlign = ContentAlignment.MiddleLeft;
                            lblMark.Text = "无书签";
                            pnlMark.Controls.Add(lblMark);
                            prevHandles.Add(lblMark.Handle);
                        }

                        int st = s.IndexOf("$");
                        txtContainer.Text = s.Substring(st + 1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("读取文件时发生异常，文件可能无法正常显示！" + ex.Message, "读取错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        br.Close();
                        fs.Close();
                    }
                }
                //
                //普通文本文件读取
                //
                else
                {
                    if (lblNoMark != null)
                    {
                        pnlMark.Controls.Remove(lblNoMark);
                    }
                    if (prevHandles.Count > 0)
                    {
                        Control c = null;
                        int count = prevHandles.Count;
                        for (int i = 0; i < count; i++)
                        {
                            c = FromHandle(prevHandles[i]);
                            pnlMark.Controls.Remove(c);
                        }
                        for (int m = 0; m < count; m++)
                        {
                            prevHandles.RemoveAt(0);
                        }
                    }
                    if (markAllHandles.Count > 0)
                    {
                        Control c = null;
                        int count = markAllHandles.Count;
                        for (int i = 0; i < count; i++)
                        {
                            c = FromHandle(markAllHandles[i]);
                            pnlMark.Controls.Remove(c);    //控件
                        }
                        for (int m = 0; m < count; m++)
                        {
                            markAllHandles.RemoveAt(0);    //句柄
                            markAll.RemoveAt(0);    //文本
                            locAll.RemoveAt(0);    //定位信息
                        }
                    }

                    markCount = 0;

                    Label lblMark = new Label();
                    lblMark.Width = pnlMark.Width - 8;
                    lblMark.Location = new Point(4, 4);
                    lblMark.Padding = new Padding(5);
                    lblMark.TextAlign = ContentAlignment.MiddleLeft;
                    lblMark.Text = "无书签";
                    pnlMark.Controls.Add(lblMark);
                    prevHandles.Add(lblMark.Handle);
                    StreamReader sr = new StreamReader(opend, Encoding.Default);
                    try
                    {
                        txtContainer.Text = sr.ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("读取文件时发生异常，文件可能无法正常显示！" + ex.Message, "读取错误报告", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        sr.Close();
                    }

                }

                fileState = 0;     //设置状态为未变化
            }
            
                
        }

        void fst_SetEventHandler()
        {
            InitializePrograme();
            Reload();
            fileState = 0;
            MessageBox.Show("设置成功，部分设置下次启动生效!", "提示 - 设置成功");
        }

        private void mAbout_Click(object sender, EventArgs e)
        {
            FormAbout fab = new FormAbout();
            fab.Show();
        }



        
   
        

    }/*class-------------------------------------------*/
}/*namespae--------------------------------------------*/
