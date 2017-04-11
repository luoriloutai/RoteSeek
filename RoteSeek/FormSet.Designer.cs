namespace RoteSeek
{
    partial class FormSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbxBack = new System.Windows.Forms.GroupBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.lblBack = new System.Windows.Forms.Label();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.lblFore = new System.Windows.Forms.Label();
            this.lblForeColor = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbxFont = new System.Windows.Forms.GroupBox();
            this.lblFont = new System.Windows.Forms.Label();
            this.lblFontSet = new System.Windows.Forms.Label();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxLeft = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxRight = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.btnRestore = new System.Windows.Forms.Button();
            this.gbxBack.SuspendLayout();
            this.gbxFont.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxBack
            // 
            this.gbxBack.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gbxBack.Controls.Add(this.lblForeColor);
            this.gbxBack.Controls.Add(this.lblFore);
            this.gbxBack.Controls.Add(this.lblBackColor);
            this.gbxBack.Controls.Add(this.lblBack);
            this.gbxBack.ForeColor = System.Drawing.Color.Black;
            this.gbxBack.Location = new System.Drawing.Point(12, 12);
            this.gbxBack.Name = "gbxBack";
            this.gbxBack.Size = new System.Drawing.Size(333, 86);
            this.gbxBack.TabIndex = 0;
            this.gbxBack.TabStop = false;
            this.gbxBack.Text = "颜色设置";
            // 
            // lblBack
            // 
            this.lblBack.AutoSize = true;
            this.lblBack.ForeColor = System.Drawing.Color.Black;
            this.lblBack.Location = new System.Drawing.Point(30, 42);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(77, 12);
            this.lblBack.TabIndex = 0;
            this.lblBack.Text = "选择背景色：";
            // 
            // lblBackColor
            // 
            this.lblBackColor.BackColor = System.Drawing.SystemColors.Window;
            this.lblBackColor.Location = new System.Drawing.Point(113, 33);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(23, 23);
            this.lblBackColor.TabIndex = 1;
            this.toolTip.SetToolTip(this.lblBackColor, "页面背景色");
            this.lblBackColor.Click += new System.EventHandler(this.lblBackColor_Click);
            // 
            // lblFore
            // 
            this.lblFore.AutoSize = true;
            this.lblFore.ForeColor = System.Drawing.Color.Black;
            this.lblFore.Location = new System.Drawing.Point(177, 42);
            this.lblFore.Name = "lblFore";
            this.lblFore.Size = new System.Drawing.Size(77, 12);
            this.lblFore.TabIndex = 0;
            this.lblFore.Text = "选择前景色：";
            // 
            // lblForeColor
            // 
            this.lblForeColor.BackColor = System.Drawing.SystemColors.WindowText;
            this.lblForeColor.Location = new System.Drawing.Point(260, 33);
            this.lblForeColor.Name = "lblForeColor";
            this.lblForeColor.Size = new System.Drawing.Size(23, 23);
            this.lblForeColor.TabIndex = 1;
            this.toolTip.SetToolTip(this.lblForeColor, "页面前景色");
            this.lblForeColor.Click += new System.EventHandler(this.lblForeColor_Click);
            // 
            // gbxFont
            // 
            this.gbxFont.Controls.Add(this.btnSelectFont);
            this.gbxFont.Controls.Add(this.lblFontSet);
            this.gbxFont.Controls.Add(this.lblFont);
            this.gbxFont.Location = new System.Drawing.Point(12, 127);
            this.gbxFont.Name = "gbxFont";
            this.gbxFont.Size = new System.Drawing.Size(333, 85);
            this.gbxFont.TabIndex = 1;
            this.gbxFont.TabStop = false;
            this.gbxFont.Text = "字体设置";
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(12, 42);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(53, 12);
            this.lblFont.TabIndex = 0;
            this.lblFont.Text = "当前字体";
            // 
            // lblFontSet
            // 
            this.lblFontSet.AutoEllipsis = true;
            this.lblFontSet.Location = new System.Drawing.Point(70, 42);
            this.lblFontSet.Name = "lblFontSet";
            this.lblFontSet.Size = new System.Drawing.Size(158, 23);
            this.lblFontSet.TabIndex = 1;
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.Location = new System.Drawing.Point(234, 37);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFont.TabIndex = 2;
            this.btnSelectFont.Text = "更改字体";
            this.btnSelectFont.UseVisualStyleBackColor = true;
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxRight);
            this.groupBox1.Controls.Add(this.tbxLeft);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 242);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 90);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "页面设置";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 379);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "文本左边距";
            // 
            // tbxLeft
            // 
            this.tbxLeft.Location = new System.Drawing.Point(93, 36);
            this.tbxLeft.Name = "tbxLeft";
            this.tbxLeft.Size = new System.Drawing.Size(58, 21);
            this.tbxLeft.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文本右边距";
            // 
            // tbxRight
            // 
            this.tbxRight.Location = new System.Drawing.Point(259, 35);
            this.tbxRight.Name = "tbxRight";
            this.tbxRight.Size = new System.Drawing.Size(50, 21);
            this.tbxRight.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(270, 379);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(139, 379);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "恢复默认";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // FormSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(357, 427);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxFont);
            this.Controls.Add(this.gbxBack);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.FormSet_Load);
            this.gbxBack.ResumeLayout(false);
            this.gbxBack.PerformLayout();
            this.gbxFont.ResumeLayout(false);
            this.gbxFont.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxBack;
        private System.Windows.Forms.Label lblForeColor;
        private System.Windows.Forms.Label lblFore;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox gbxFont;
        private System.Windows.Forms.Button btnSelectFont;
        private System.Windows.Forms.Label lblFontSet;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxRight;
        private System.Windows.Forms.TextBox tbxLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Button btnRestore;
    }
}