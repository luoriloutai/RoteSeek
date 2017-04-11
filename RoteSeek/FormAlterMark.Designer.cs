namespace RoteSeek
{
    partial class FormAlterMark
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
            this.gbxAlter = new System.Windows.Forms.GroupBox();
            this.lblMarkNew = new System.Windows.Forms.Label();
            this.tbxNewMark = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbxAlter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxAlter
            // 
            this.gbxAlter.Controls.Add(this.btnOk);
            this.gbxAlter.Controls.Add(this.btnCancel);
            this.gbxAlter.Controls.Add(this.tbxNewMark);
            this.gbxAlter.Controls.Add(this.lblMarkNew);
            this.gbxAlter.ForeColor = System.Drawing.Color.White;
            this.gbxAlter.Location = new System.Drawing.Point(12, 12);
            this.gbxAlter.Name = "gbxAlter";
            this.gbxAlter.Size = new System.Drawing.Size(370, 160);
            this.gbxAlter.TabIndex = 0;
            this.gbxAlter.TabStop = false;
            this.gbxAlter.Text = "修改书签";
            // 
            // lblMarkNew
            // 
            this.lblMarkNew.AutoSize = true;
            this.lblMarkNew.Location = new System.Drawing.Point(31, 36);
            this.lblMarkNew.Name = "lblMarkNew";
            this.lblMarkNew.Size = new System.Drawing.Size(101, 12);
            this.lblMarkNew.TabIndex = 0;
            this.lblMarkNew.Text = "输入新书签名称：";
            // 
            // tbxNewMark
            // 
            this.tbxNewMark.Location = new System.Drawing.Point(33, 62);
            this.tbxNewMark.Name = "tbxNewMark";
            this.tbxNewMark.Size = new System.Drawing.Size(307, 21);
            this.tbxNewMark.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(162, 109);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(33, 109);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FormAlterMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(73)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(394, 184);
            this.Controls.Add(this.gbxAlter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAlterMark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改书签";
            this.gbxAlter.ResumeLayout(false);
            this.gbxAlter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxAlter;
        private System.Windows.Forms.Label lblMarkNew;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbxNewMark;
    }
}