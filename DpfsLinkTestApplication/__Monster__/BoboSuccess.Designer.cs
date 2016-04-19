namespace DpfsLinkTestApplication.__Monster__ {
    partial class BoboSuccess {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReadMessage = new System.Windows.Forms.Button();
            this.picBobo0002 = new System.Windows.Forms.PictureBox();
            this.picBobo0001 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBobo0002)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBobo0001)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReadMessage);
            this.panel1.Controls.Add(this.picBobo0002);
            this.panel1.Controls.Add(this.picBobo0001);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 625);
            this.panel1.TabIndex = 0;
            // 
            // btnReadMessage
            // 
            this.btnReadMessage.Location = new System.Drawing.Point(456, 489);
            this.btnReadMessage.Name = "btnReadMessage";
            this.btnReadMessage.Size = new System.Drawing.Size(211, 23);
            this.btnReadMessage.TabIndex = 2;
            this.btnReadMessage.Text = "Read Message";
            this.btnReadMessage.UseVisualStyleBackColor = true;
            this.btnReadMessage.Visible = false;
            this.btnReadMessage.Click += new System.EventHandler(this.button1_Click);
            // 
            // picBobo0002
            // 
            this.picBobo0002.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBobo0002.Image = global::DpfsLinkTestApplication.Properties.Resources.bobo_next0002;
            this.picBobo0002.Location = new System.Drawing.Point(0, 0);
            this.picBobo0002.Name = "picBobo0002";
            this.picBobo0002.Size = new System.Drawing.Size(760, 625);
            this.picBobo0002.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBobo0002.TabIndex = 1;
            this.picBobo0002.TabStop = false;
            this.picBobo0002.Visible = false;
            this.picBobo0002.Click += new System.EventHandler(this.picBobo0002_Click);
            // 
            // picBobo0001
            // 
            this.picBobo0001.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBobo0001.Image = global::DpfsLinkTestApplication.Properties.Resources.bobo_next0001;
            this.picBobo0001.Location = new System.Drawing.Point(0, 0);
            this.picBobo0001.Name = "picBobo0001";
            this.picBobo0001.Size = new System.Drawing.Size(760, 625);
            this.picBobo0001.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBobo0001.TabIndex = 0;
            this.picBobo0001.TabStop = false;
            // 
            // BoboSuccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 625);
            this.Controls.Add(this.panel1);
            this.Name = "BoboSuccess";
            this.Text = "Level {0001} : Bobo saves the Houses!";
            this.Load += new System.EventHandler(this.BoboSuccess_Load);
            this.Shown += new System.EventHandler(this.BoboSuccess_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBobo0002)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBobo0001)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBobo0001;
        private System.Windows.Forms.PictureBox picBobo0002;
        private System.Windows.Forms.Button btnReadMessage;
    }
}