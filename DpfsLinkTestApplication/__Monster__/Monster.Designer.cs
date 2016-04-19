namespace DpfsLinkTestApplication.__Monster__ {
    public partial class Monster {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monster));
            this.panelMonster = new System.Windows.Forms.Panel();
            this.picWin = new System.Windows.Forms.PictureBox();
            this.picDead = new System.Windows.Forms.PictureBox();
            this.picMonster = new System.Windows.Forms.PictureBox();
            this.panelMonster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMonster)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMonster
            // 
            this.panelMonster.Controls.Add(this.picWin);
            this.panelMonster.Controls.Add(this.picDead);
            this.panelMonster.Controls.Add(this.picMonster);
            this.panelMonster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMonster.Location = new System.Drawing.Point(0, 0);
            this.panelMonster.Name = "panelMonster";
            this.panelMonster.Size = new System.Drawing.Size(760, 625);
            this.panelMonster.TabIndex = 0;
            // 
            // picWin
            // 
            this.picWin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWin.Image = global::DpfsLinkTestApplication.Properties.Resources.bobo_success;
            this.picWin.Location = new System.Drawing.Point(0, 0);
            this.picWin.Name = "picWin";
            this.picWin.Size = new System.Drawing.Size(760, 625);
            this.picWin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWin.TabIndex = 2;
            this.picWin.TabStop = false;
            this.picWin.Visible = false;
            this.picWin.Click += new System.EventHandler(this.picWin_Click);
            // 
            // picDead
            // 
            this.picDead.BackColor = System.Drawing.Color.Black;
            this.picDead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDead.Image = global::DpfsLinkTestApplication.Properties.Resources.dead;
            this.picDead.Location = new System.Drawing.Point(0, 0);
            this.picDead.Name = "picDead";
            this.picDead.Size = new System.Drawing.Size(760, 625);
            this.picDead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDead.TabIndex = 1;
            this.picDead.TabStop = false;
            this.picDead.Visible = false;
            // 
            // picMonster
            // 
            this.picMonster.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picMonster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMonster.Image = ((System.Drawing.Image)(resources.GetObject("picMonster.Image")));
            this.picMonster.Location = new System.Drawing.Point(0, 0);
            this.picMonster.Name = "picMonster";
            this.picMonster.Size = new System.Drawing.Size(760, 625);
            this.picMonster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMonster.TabIndex = 0;
            this.picMonster.TabStop = false;
            // 
            // Monster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 625);
            this.Controls.Add(this.panelMonster);
            this.Name = "Monster";
            this.Text = "Oh No! Monster!";
            this.Load += new System.EventHandler(this.Monster_Load);
            this.Shown += new System.EventHandler(this.Monster_Shown);
            this.panelMonster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMonster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMonster;
        private System.Windows.Forms.PictureBox picMonster;
        private System.Windows.Forms.PictureBox picDead;
        private System.Windows.Forms.PictureBox picWin;
    }
}