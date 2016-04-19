namespace DpfsLinkTestApplication {
    partial class AddMessage {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnMessageExit = new System.Windows.Forms.Button();
            this.btnMessageClear = new System.Windows.Forms.Button();
            this.btnMessageSave = new System.Windows.Forms.Button();
            this.btnLoadMessage = new System.Windows.Forms.Button();
            this.clbxMsgTypeSelect = new System.Windows.Forms.CheckedListBox();
            this.rtbxMessage = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(9, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 308);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.MaximumSize = new System.Drawing.Size(872, 308);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(872, 308);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbxMessage);
            this.splitContainer1.Size = new System.Drawing.Size(872, 308);
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer2.Panel1.Controls.Add(this.btnMessageExit);
            this.splitContainer2.Panel1.Controls.Add(this.btnMessageClear);
            this.splitContainer2.Panel1.Controls.Add(this.btnMessageSave);
            this.splitContainer2.Panel1.Controls.Add(this.btnLoadMessage);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer2.Panel2.Controls.Add(this.clbxMsgTypeSelect);
            this.splitContainer2.Size = new System.Drawing.Size(872, 50);
            this.splitContainer2.SplitterDistance = 192;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnMessageExit
            // 
            this.btnMessageExit.Location = new System.Drawing.Point(99, 3);
            this.btnMessageExit.MaximumSize = new System.Drawing.Size(90, 22);
            this.btnMessageExit.MinimumSize = new System.Drawing.Size(90, 22);
            this.btnMessageExit.Name = "btnMessageExit";
            this.btnMessageExit.Size = new System.Drawing.Size(90, 22);
            this.btnMessageExit.TabIndex = 3;
            this.btnMessageExit.Text = "Exit";
            this.btnMessageExit.UseVisualStyleBackColor = true;
            this.btnMessageExit.Click += new System.EventHandler(this.btnMessageExit_Click);
            // 
            // btnMessageClear
            // 
            this.btnMessageClear.Location = new System.Drawing.Point(3, 3);
            this.btnMessageClear.MaximumSize = new System.Drawing.Size(90, 22);
            this.btnMessageClear.MinimumSize = new System.Drawing.Size(90, 22);
            this.btnMessageClear.Name = "btnMessageClear";
            this.btnMessageClear.Size = new System.Drawing.Size(90, 22);
            this.btnMessageClear.TabIndex = 2;
            this.btnMessageClear.Text = "Clear";
            this.btnMessageClear.UseVisualStyleBackColor = true;
            this.btnMessageClear.Click += new System.EventHandler(this.btnMessageClear_Click);
            // 
            // btnMessageSave
            // 
            this.btnMessageSave.Location = new System.Drawing.Point(99, 25);
            this.btnMessageSave.MaximumSize = new System.Drawing.Size(90, 22);
            this.btnMessageSave.MinimumSize = new System.Drawing.Size(90, 22);
            this.btnMessageSave.Name = "btnMessageSave";
            this.btnMessageSave.Size = new System.Drawing.Size(90, 22);
            this.btnMessageSave.TabIndex = 1;
            this.btnMessageSave.Text = "Save";
            this.btnMessageSave.UseVisualStyleBackColor = true;
            this.btnMessageSave.Click += new System.EventHandler(this.btnMessageSave_Click);
            // 
            // btnLoadMessage
            // 
            this.btnLoadMessage.Location = new System.Drawing.Point(3, 25);
            this.btnLoadMessage.MaximumSize = new System.Drawing.Size(90, 22);
            this.btnLoadMessage.MinimumSize = new System.Drawing.Size(90, 22);
            this.btnLoadMessage.Name = "btnLoadMessage";
            this.btnLoadMessage.Size = new System.Drawing.Size(90, 22);
            this.btnLoadMessage.TabIndex = 0;
            this.btnLoadMessage.Text = "Load";
            this.btnLoadMessage.UseVisualStyleBackColor = true;
            this.btnLoadMessage.Click += new System.EventHandler(this.btnLoadMessage_Click);
            // 
            // clbxMsgTypeSelect
            // 
            this.clbxMsgTypeSelect.CheckOnClick = true;
            this.clbxMsgTypeSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbxMsgTypeSelect.FormattingEnabled = true;
            this.clbxMsgTypeSelect.Items.AddRange(new object[] {
            "IP",
            "IPV4",
            "IPV6",
            "RAW",
            "IPX",
            "IPV6FRAG",
            "LCMP",
            "ICMP",
            "IGMP",
            "TCP",
            "UDP",
            "UNKNOWN"});
            this.clbxMsgTypeSelect.Location = new System.Drawing.Point(0, 0);
            this.clbxMsgTypeSelect.Margin = new System.Windows.Forms.Padding(10);
            this.clbxMsgTypeSelect.MaximumSize = new System.Drawing.Size(676, 50);
            this.clbxMsgTypeSelect.MinimumSize = new System.Drawing.Size(676, 50);
            this.clbxMsgTypeSelect.MultiColumn = true;
            this.clbxMsgTypeSelect.Name = "clbxMsgTypeSelect";
            this.clbxMsgTypeSelect.Size = new System.Drawing.Size(676, 50);
            this.clbxMsgTypeSelect.TabIndex = 1;
            this.clbxMsgTypeSelect.ThreeDCheckBoxes = true;
            this.clbxMsgTypeSelect.SelectedIndexChanged += new System.EventHandler(this.clbxMsgTypeSelect_SelectedIndexChanged);
            // 
            // rtbxMessage
            // 
            this.rtbxMessage.AutoWordSelection = true;
            this.rtbxMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbxMessage.DetectUrls = false;
            this.rtbxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbxMessage.Location = new System.Drawing.Point(0, 0);
            this.rtbxMessage.Margin = new System.Windows.Forms.Padding(10);
            this.rtbxMessage.MaximumSize = new System.Drawing.Size(872, 254);
            this.rtbxMessage.MinimumSize = new System.Drawing.Size(872, 254);
            this.rtbxMessage.Name = "rtbxMessage";
            this.rtbxMessage.ShortcutsEnabled = false;
            this.rtbxMessage.ShowSelectionMargin = true;
            this.rtbxMessage.Size = new System.Drawing.Size(872, 254);
            this.rtbxMessage.TabIndex = 0;
            this.rtbxMessage.Text = "";
            this.rtbxMessage.WordWrap = false;
            // 
            // AddMessage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(890, 326);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(906, 365);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(906, 365);
            this.Name = "AddMessage";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Message";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnMessageSave;
        private System.Windows.Forms.Button btnLoadMessage;
        private System.Windows.Forms.RichTextBox rtbxMessage;
        public System.Windows.Forms.CheckedListBox clbxMsgTypeSelect;
        private System.Windows.Forms.Button btnMessageExit;
        private System.Windows.Forms.Button btnMessageClear;

    }
}
