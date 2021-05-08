
namespace Sockets
{
    partial class chatSockets
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
            this.txtBxIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxPort = new System.Windows.Forms.TextBox();
            this.btnConect = new System.Windows.Forms.Button();
            this.txtBxChat = new System.Windows.Forms.TextBox();
            this.txtBxSendMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnServerStart = new System.Windows.Forms.Button();
            this.txtBxNick = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chBxUser1 = new System.Windows.Forms.CheckBox();
            this.chBxUser3 = new System.Windows.Forms.CheckBox();
            this.chBxUser4 = new System.Windows.Forms.CheckBox();
            this.chBxUser5 = new System.Windows.Forms.CheckBox();
            this.chBxUser6 = new System.Windows.Forms.CheckBox();
            this.chBxUser7 = new System.Windows.Forms.CheckBox();
            this.chBxUser8 = new System.Windows.Forms.CheckBox();
            this.txtBxUsers = new System.Windows.Forms.TextBox();
            this.chBxUser2 = new System.Windows.Forms.CheckBox();
            this.chBxUser9 = new System.Windows.Forms.CheckBox();
            this.chBxUser10 = new System.Windows.Forms.CheckBox();
            this.chBxUser11 = new System.Windows.Forms.CheckBox();
            this.chBxUser12 = new System.Windows.Forms.CheckBox();
            this.chBxUser13 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtBxIP
            // 
            this.txtBxIP.Location = new System.Drawing.Point(51, 19);
            this.txtBxIP.Name = "txtBxIP";
            this.txtBxIP.Size = new System.Drawing.Size(157, 20);
            this.txtBxIP.TabIndex = 0;
            this.txtBxIP.Text = "192.168.100.40";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Porta";
            // 
            // txtBxPort
            // 
            this.txtBxPort.Location = new System.Drawing.Point(252, 19);
            this.txtBxPort.Name = "txtBxPort";
            this.txtBxPort.Size = new System.Drawing.Size(71, 20);
            this.txtBxPort.TabIndex = 3;
            this.txtBxPort.Text = "9400";
            // 
            // btnConect
            // 
            this.btnConect.Location = new System.Drawing.Point(329, 17);
            this.btnConect.Name = "btnConect";
            this.btnConect.Size = new System.Drawing.Size(75, 23);
            this.btnConect.TabIndex = 4;
            this.btnConect.Text = "Connect";
            this.btnConect.UseVisualStyleBackColor = true;
            this.btnConect.Click += new System.EventHandler(this.btnConect_Click);
            // 
            // txtBxChat
            // 
            this.txtBxChat.Location = new System.Drawing.Point(19, 98);
            this.txtBxChat.Multiline = true;
            this.txtBxChat.Name = "txtBxChat";
            this.txtBxChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBxChat.Size = new System.Drawing.Size(413, 279);
            this.txtBxChat.TabIndex = 5;
            // 
            // txtBxSendMsg
            // 
            this.txtBxSendMsg.Location = new System.Drawing.Point(19, 385);
            this.txtBxSendMsg.Name = "txtBxSendMsg";
            this.txtBxSendMsg.Size = new System.Drawing.Size(503, 20);
            this.txtBxSendMsg.TabIndex = 6;
            this.txtBxSendMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBxSendMsg_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(528, 382);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(410, 17);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(75, 23);
            this.btnServerStart.TabIndex = 8;
            this.btnServerStart.Text = "Server Start";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // txtBxNick
            // 
            this.txtBxNick.Location = new System.Drawing.Point(51, 45);
            this.txtBxNick.Name = "txtBxNick";
            this.txtBxNick.Size = new System.Drawing.Size(157, 20);
            this.txtBxNick.TabIndex = 9;
            this.txtBxNick.Text = "User";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nick";
            // 
            // chBxUser1
            // 
            this.chBxUser1.AutoSize = true;
            this.chBxUser1.Location = new System.Drawing.Point(452, 103);
            this.chBxUser1.Name = "chBxUser1";
            this.chBxUser1.Size = new System.Drawing.Size(15, 14);
            this.chBxUser1.TabIndex = 12;
            this.chBxUser1.UseVisualStyleBackColor = true;
            // 
            // chBxUser3
            // 
            this.chBxUser3.AutoSize = true;
            this.chBxUser3.Location = new System.Drawing.Point(452, 143);
            this.chBxUser3.Name = "chBxUser3";
            this.chBxUser3.Size = new System.Drawing.Size(15, 14);
            this.chBxUser3.TabIndex = 14;
            this.chBxUser3.UseVisualStyleBackColor = true;
            // 
            // chBxUser4
            // 
            this.chBxUser4.AutoSize = true;
            this.chBxUser4.Location = new System.Drawing.Point(452, 163);
            this.chBxUser4.Name = "chBxUser4";
            this.chBxUser4.Size = new System.Drawing.Size(15, 14);
            this.chBxUser4.TabIndex = 15;
            this.chBxUser4.UseVisualStyleBackColor = true;
            // 
            // chBxUser5
            // 
            this.chBxUser5.AutoSize = true;
            this.chBxUser5.Location = new System.Drawing.Point(452, 183);
            this.chBxUser5.Name = "chBxUser5";
            this.chBxUser5.Size = new System.Drawing.Size(15, 14);
            this.chBxUser5.TabIndex = 16;
            this.chBxUser5.UseVisualStyleBackColor = true;
            // 
            // chBxUser6
            // 
            this.chBxUser6.AutoSize = true;
            this.chBxUser6.Location = new System.Drawing.Point(452, 203);
            this.chBxUser6.Name = "chBxUser6";
            this.chBxUser6.Size = new System.Drawing.Size(15, 14);
            this.chBxUser6.TabIndex = 17;
            this.chBxUser6.UseVisualStyleBackColor = true;
            // 
            // chBxUser7
            // 
            this.chBxUser7.AutoSize = true;
            this.chBxUser7.Location = new System.Drawing.Point(452, 223);
            this.chBxUser7.Name = "chBxUser7";
            this.chBxUser7.Size = new System.Drawing.Size(15, 14);
            this.chBxUser7.TabIndex = 18;
            this.chBxUser7.UseVisualStyleBackColor = true;
            // 
            // chBxUser8
            // 
            this.chBxUser8.AutoSize = true;
            this.chBxUser8.Location = new System.Drawing.Point(452, 243);
            this.chBxUser8.Name = "chBxUser8";
            this.chBxUser8.Size = new System.Drawing.Size(15, 14);
            this.chBxUser8.TabIndex = 19;
            this.chBxUser8.UseVisualStyleBackColor = true;
            // 
            // txtBxUsers
            // 
            this.txtBxUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxUsers.Location = new System.Drawing.Point(476, 98);
            this.txtBxUsers.Multiline = true;
            this.txtBxUsers.Name = "txtBxUsers";
            this.txtBxUsers.Size = new System.Drawing.Size(127, 273);
            this.txtBxUsers.TabIndex = 11;
            // 
            // chBxUser2
            // 
            this.chBxUser2.AutoSize = true;
            this.chBxUser2.Location = new System.Drawing.Point(452, 123);
            this.chBxUser2.Name = "chBxUser2";
            this.chBxUser2.Size = new System.Drawing.Size(15, 14);
            this.chBxUser2.TabIndex = 13;
            this.chBxUser2.UseVisualStyleBackColor = true;
            // 
            // chBxUser9
            // 
            this.chBxUser9.AutoSize = true;
            this.chBxUser9.Location = new System.Drawing.Point(452, 263);
            this.chBxUser9.Name = "chBxUser9";
            this.chBxUser9.Size = new System.Drawing.Size(15, 14);
            this.chBxUser9.TabIndex = 20;
            this.chBxUser9.UseVisualStyleBackColor = true;
            // 
            // chBxUser10
            // 
            this.chBxUser10.AutoSize = true;
            this.chBxUser10.Location = new System.Drawing.Point(452, 283);
            this.chBxUser10.Name = "chBxUser10";
            this.chBxUser10.Size = new System.Drawing.Size(15, 14);
            this.chBxUser10.TabIndex = 21;
            this.chBxUser10.UseVisualStyleBackColor = true;
            // 
            // chBxUser11
            // 
            this.chBxUser11.AutoSize = true;
            this.chBxUser11.Location = new System.Drawing.Point(452, 303);
            this.chBxUser11.Name = "chBxUser11";
            this.chBxUser11.Size = new System.Drawing.Size(15, 14);
            this.chBxUser11.TabIndex = 22;
            this.chBxUser11.UseVisualStyleBackColor = true;
            // 
            // chBxUser12
            // 
            this.chBxUser12.AutoSize = true;
            this.chBxUser12.Location = new System.Drawing.Point(452, 323);
            this.chBxUser12.Name = "chBxUser12";
            this.chBxUser12.Size = new System.Drawing.Size(15, 14);
            this.chBxUser12.TabIndex = 23;
            this.chBxUser12.UseVisualStyleBackColor = true;
            // 
            // chBxUser13
            // 
            this.chBxUser13.AutoSize = true;
            this.chBxUser13.Location = new System.Drawing.Point(452, 343);
            this.chBxUser13.Name = "chBxUser13";
            this.chBxUser13.Size = new System.Drawing.Size(15, 14);
            this.chBxUser13.TabIndex = 24;
            this.chBxUser13.UseVisualStyleBackColor = true;
            // 
            // chatSockets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 412);
            this.Controls.Add(this.chBxUser13);
            this.Controls.Add(this.chBxUser12);
            this.Controls.Add(this.chBxUser11);
            this.Controls.Add(this.chBxUser10);
            this.Controls.Add(this.chBxUser9);
            this.Controls.Add(this.chBxUser8);
            this.Controls.Add(this.chBxUser7);
            this.Controls.Add(this.chBxUser6);
            this.Controls.Add(this.chBxUser5);
            this.Controls.Add(this.chBxUser4);
            this.Controls.Add(this.chBxUser3);
            this.Controls.Add(this.chBxUser2);
            this.Controls.Add(this.chBxUser1);
            this.Controls.Add(this.txtBxUsers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxNick);
            this.Controls.Add(this.btnServerStart);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtBxSendMsg);
            this.Controls.Add(this.txtBxChat);
            this.Controls.Add(this.btnConect);
            this.Controls.Add(this.txtBxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxIP);
            this.Name = "chatSockets";
            this.Text = "ChatSockets";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxPort;
        private System.Windows.Forms.Button btnConect;
        private System.Windows.Forms.TextBox txtBxChat;
        private System.Windows.Forms.TextBox txtBxSendMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.TextBox txtBxNick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chBxUser1;
        private System.Windows.Forms.CheckBox chBxUser3;
        private System.Windows.Forms.CheckBox chBxUser4;
        private System.Windows.Forms.CheckBox chBxUser5;
        private System.Windows.Forms.CheckBox chBxUser6;
        private System.Windows.Forms.CheckBox chBxUser7;
        private System.Windows.Forms.CheckBox chBxUser8;
        private System.Windows.Forms.TextBox txtBxUsers;
        private System.Windows.Forms.CheckBox chBxUser2;
        private System.Windows.Forms.CheckBox chBxUser9;
        private System.Windows.Forms.CheckBox chBxUser10;
        private System.Windows.Forms.CheckBox chBxUser11;
        private System.Windows.Forms.CheckBox chBxUser12;
        private System.Windows.Forms.CheckBox chBxUser13;
    }
}

