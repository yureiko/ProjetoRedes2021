
namespace Sockets
{
    partial class Server
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
            this.txtBxChat.Size = new System.Drawing.Size(466, 279);
            this.txtBxChat.TabIndex = 5;
            // 
            // txtBxSendMsg
            // 
            this.txtBxSendMsg.Location = new System.Drawing.Point(19, 385);
            this.txtBxSendMsg.Name = "txtBxSendMsg";
            this.txtBxSendMsg.Size = new System.Drawing.Size(385, 20);
            this.txtBxSendMsg.TabIndex = 6;
            this.txtBxSendMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBxSendMsg_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(410, 383);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 412);
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
            this.Name = "Form1";
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
    }
}

