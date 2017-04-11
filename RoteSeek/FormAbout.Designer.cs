namespace RoteSeek
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.lblTil = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.wmp = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.wmp)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTil
            // 
            this.lblTil.AutoSize = true;
            this.lblTil.BackColor = System.Drawing.Color.Transparent;
            this.lblTil.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTil.ForeColor = System.Drawing.Color.Gray;
            this.lblTil.Location = new System.Drawing.Point(558, 48);
            this.lblTil.Name = "lblTil";
            this.lblTil.Size = new System.Drawing.Size(26, 252);
            this.lblTil.TabIndex = 1;
            this.lblTil.Text = "关\r\n于\r\n我\r\n，\r\n关\r\n于\r\n你\r\n，\r\n也\r\n关\r\n于\r\n它";
            // 
            // lblContent
            // 
            this.lblContent.BackColor = System.Drawing.Color.Transparent;
            this.lblContent.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContent.ForeColor = System.Drawing.Color.DarkGray;
            this.lblContent.Location = new System.Drawing.Point(12, 74);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(230, 442);
            this.lblContent.TabIndex = 2;
            this.lblContent.Text = resources.GetString("lblContent.Text");
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.BackColor = System.Drawing.Color.Transparent;
            this.lblTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.ForeColor = System.Drawing.Color.LightGray;
            this.lblTip.Location = new System.Drawing.Point(12, 18);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(33, 17);
            this.lblTip.TabIndex = 3;
            this.lblTip.Text = "time";
            // 
            // wmp
            // 
            this.wmp.Enabled = true;
            this.wmp.Location = new System.Drawing.Point(330, 450);
            this.wmp.Name = "wmp";
            this.wmp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp.OcxState")));
            this.wmp.Size = new System.Drawing.Size(215, 45);
            this.wmp.TabIndex = 4;
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(73)))), ((int)(((byte)(106)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(605, 526);
            this.Controls.Add(this.wmp);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.lblTil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormAbout";
            this.Text = "关于";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAbout_FormClosing);
            this.Load += new System.EventHandler(this.FormAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wmp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTil;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label lblTip;
        private AxWMPLib.AxWindowsMediaPlayer wmp;


    }
}