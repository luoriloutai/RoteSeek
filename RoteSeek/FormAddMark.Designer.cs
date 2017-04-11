namespace RoteSeek
{
    partial class FormAddMark
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
            this.gbxSay = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbxMarkName = new System.Windows.Forms.TextBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.gbxSay.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxSay
            // 
            this.gbxSay.Controls.Add(this.btnOk);
            this.gbxSay.Controls.Add(this.btnCancel);
            this.gbxSay.Controls.Add(this.tbxMarkName);
            this.gbxSay.Controls.Add(this.lblInput);
            this.gbxSay.ForeColor = System.Drawing.Color.White;
            this.gbxSay.Location = new System.Drawing.Point(12, 12);
            this.gbxSay.Name = "gbxSay";
            this.gbxSay.Size = new System.Drawing.Size(340, 147);
            this.gbxSay.TabIndex = 0;
            this.gbxSay.TabStop = false;
            this.gbxSay.Text = "添加书签";
            // 
            // btnOk
            // 
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(27, 102);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(182, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbxMarkName
            // 
            this.tbxMarkName.Location = new System.Drawing.Point(27, 54);
            this.tbxMarkName.Name = "tbxMarkName";
            this.tbxMarkName.Size = new System.Drawing.Size(287, 21);
            this.tbxMarkName.TabIndex = 1;
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(25, 31);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(89, 12);
            this.lblInput.TabIndex = 0;
            this.lblInput.Text = "输入书签名称：";
            // 
            // FormAddMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(73)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(364, 180);
            this.Controls.Add(this.gbxSay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FormAddMark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加书签";
            this.gbxSay.ResumeLayout(false);
            this.gbxSay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxSay;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbxMarkName;

    }
}