namespace RoteSeek
{
    partial class FormFindAndReplace
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
            this.gbxRep = new System.Windows.Forms.GroupBox();
            this.tbxReplace = new System.Windows.Forms.TextBox();
            this.lblReplace = new System.Windows.Forms.Label();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.tbxFind = new System.Windows.Forms.TextBox();
            this.lblFind = new System.Windows.Forms.Label();
            this.cbxIgnore = new System.Windows.Forms.CheckBox();
            this.gbxRep.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxRep
            // 
            this.gbxRep.Controls.Add(this.cbxIgnore);
            this.gbxRep.Controls.Add(this.tbxReplace);
            this.gbxRep.Controls.Add(this.lblReplace);
            this.gbxRep.Controls.Add(this.btnReplaceAll);
            this.gbxRep.Controls.Add(this.btnCancel);
            this.gbxRep.Controls.Add(this.btnSearch);
            this.gbxRep.Controls.Add(this.btnReplace);
            this.gbxRep.Controls.Add(this.tbxFind);
            this.gbxRep.Controls.Add(this.lblFind);
            this.gbxRep.ForeColor = System.Drawing.Color.White;
            this.gbxRep.Location = new System.Drawing.Point(12, 12);
            this.gbxRep.Name = "gbxRep";
            this.gbxRep.Size = new System.Drawing.Size(515, 176);
            this.gbxRep.TabIndex = 0;
            this.gbxRep.TabStop = false;
            this.gbxRep.Text = "替换文本内容";
            // 
            // tbxReplace
            // 
            this.tbxReplace.Location = new System.Drawing.Point(97, 81);
            this.tbxReplace.Name = "tbxReplace";
            this.tbxReplace.Size = new System.Drawing.Size(280, 21);
            this.tbxReplace.TabIndex = 4;
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(26, 85);
            this.lblReplace.Name = "lblReplace";
            this.lblReplace.Size = new System.Drawing.Size(53, 12);
            this.lblReplace.TabIndex = 3;
            this.lblReplace.Text = "替换为：";
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.ForeColor = System.Drawing.Color.Black;
            this.btnReplaceAll.Location = new System.Drawing.Point(411, 129);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(75, 23);
            this.btnReplaceAll.TabIndex = 2;
            this.btnReplaceAll.Text = "全部替换";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(334, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(43, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "退出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Location = new System.Drawing.Point(411, 34);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查找下一个";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.ForeColor = System.Drawing.Color.Black;
            this.btnReplace.Location = new System.Drawing.Point(411, 81);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 2;
            this.btnReplace.Text = "替换";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // tbxFind
            // 
            this.tbxFind.Location = new System.Drawing.Point(97, 35);
            this.tbxFind.Name = "tbxFind";
            this.tbxFind.Size = new System.Drawing.Size(280, 21);
            this.tbxFind.TabIndex = 1;
            // 
            // lblFind
            // 
            this.lblFind.AutoSize = true;
            this.lblFind.Location = new System.Drawing.Point(26, 40);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(65, 12);
            this.lblFind.TabIndex = 0;
            this.lblFind.Text = "查找内容：";
            // 
            // cbxIgnore
            // 
            this.cbxIgnore.AutoSize = true;
            this.cbxIgnore.Location = new System.Drawing.Point(97, 132);
            this.cbxIgnore.Name = "cbxIgnore";
            this.cbxIgnore.Size = new System.Drawing.Size(96, 16);
            this.cbxIgnore.TabIndex = 5;
            this.cbxIgnore.Text = "不区分大小写";
            this.cbxIgnore.UseVisualStyleBackColor = true;
            // 
            // FormFindAndReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(73)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(539, 208);
            this.Controls.Add(this.gbxRep);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormFindAndReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找和替换";
            this.gbxRep.ResumeLayout(false);
            this.gbxRep.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxRep;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.TextBox tbxFind;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.TextBox tbxReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox cbxIgnore;
    }
}