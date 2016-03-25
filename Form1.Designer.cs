namespace ChatForLan
{
    partial class frmMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvUsers = new System.Windows.Forms.ListView();
            this.rtReceiveMessage = new System.Windows.Forms.RichTextBox();
            this.bnSendMessage = new System.Windows.Forms.Button();
            this.chUsers = new System.Windows.Forms.ColumnHeader();
            this.rtSendMessage = new System.Windows.Forms.RichTextBox();
            this.panel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 32);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtSendMessage);
            this.panel2.Controls.Add(this.bnSendMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 448);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(577, 66);
            this.panel2.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvUsers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtReceiveMessage);
            this.splitContainer1.Size = new System.Drawing.Size(577, 416);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // lvUsers
            // 
            this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUsers});
            this.lvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUsers.FullRowSelect = true;
            this.lvUsers.GridLines = true;
            this.lvUsers.Location = new System.Drawing.Point(0, 0);
            this.lvUsers.MultiSelect = false;
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.Size = new System.Drawing.Size(192, 416);
            this.lvUsers.TabIndex = 0;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            // 
            // rtReceiveMessage
            // 
            this.rtReceiveMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtReceiveMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtReceiveMessage.Location = new System.Drawing.Point(0, 0);
            this.rtReceiveMessage.Name = "rtReceiveMessage";
            this.rtReceiveMessage.Size = new System.Drawing.Size(381, 416);
            this.rtReceiveMessage.TabIndex = 0;
            this.rtReceiveMessage.Text = "";
            // 
            // bnSendMessage
            // 
            this.bnSendMessage.Location = new System.Drawing.Point(3, 3);
            this.bnSendMessage.Name = "bnSendMessage";
            this.bnSendMessage.Size = new System.Drawing.Size(84, 60);
            this.bnSendMessage.TabIndex = 0;
            this.bnSendMessage.Text = "Отправить сообщение";
            this.bnSendMessage.UseVisualStyleBackColor = true;
            this.bnSendMessage.Click += new System.EventHandler(this.bnSendMessage_Click);
            // 
            // chUsers
            // 
            this.chUsers.Text = "Пользователи";
            this.chUsers.Width = 187;
            // 
            // rtSendMessage
            // 
            this.rtSendMessage.Location = new System.Drawing.Point(93, 3);
            this.rtSendMessage.Name = "rtSendMessage";
            this.rtSendMessage.Size = new System.Drawing.Size(481, 60);
            this.rtSendMessage.TabIndex = 2;
            this.rtSendMessage.Text = "";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 514);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "LanChat";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvUsers;
        private System.Windows.Forms.RichTextBox rtReceiveMessage;
        private System.Windows.Forms.Button bnSendMessage;
        private System.Windows.Forms.ColumnHeader chUsers;
        private System.Windows.Forms.RichTextBox rtSendMessage;
    }
}

