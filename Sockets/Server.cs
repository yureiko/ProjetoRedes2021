using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Sockets
{
    public partial class Server : Form
    {

        private Socket connection;

        private Thread ServerSide;

        private Thread ClientSide;

        private string ip;

        private string port;

        private string message = "";

        public Server()
        {
            InitializeComponent();

            ServerSide = new Thread(new ThreadStart(RunServer));

            ClientSide = new Thread(new ThreadStart(RunClient));

            btnSend.Enabled = false;

            txtBxSendMsg.Enabled = false;

            //ServerSide.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null)
                connection.Close();

            Application.Exit();
        }



        public void RunServer()

        {
            TcpListener escutando;

            byte[] messageSend;

            byte[] messageRcv = new byte[256];

            try

            {
                escutando = new TcpListener(System.Net.IPAddress.Any,Convert.ToInt32(port));

                escutando.Start();

                while (true)
                {
                    sendToChatBox("","Aguardando Conexoes" + Environment.NewLine);

                    connection = escutando.AcceptSocket();

                    messageSend = Encoding.UTF8.GetBytes("Conectado");

                    connection.Send(messageSend);

                    sendToChatBox("Append", "Conectado " + Environment.NewLine);


                    do
                    {
                        try
                        {
                            connection.Receive(messageRcv, 0, connection.Available, SocketFlags.None);

                            message = System.Text.Encoding.UTF8.GetString(messageRcv);

                            sendToChatBox("Append", System.Text.Encoding.UTF8.GetString(messageRcv));

                        }

                        catch (Exception)
                        {
                            break;
                        }

                    } while (connection.Connected && message != "FIM");

                    sendToChatBox("Append", "Conexão Finalizada!");

                    connection.Close();

                }

            }

            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }

        }

        public void RunClient()
        {

            TcpClient cliente;

            byte[] messageSend;
            byte[] messageRcv = new byte[256];

            try

            {

                cliente = new TcpClient();

                cliente.Connect(ip, Convert.ToInt32(port));

                connection = cliente.Client;

                do
                {
                    try

                    {

                        if (connection != null)
                        {
                            connection.Receive(messageRcv);

                            sendToChatBox("Append", System.Text.Encoding.UTF8.GetString(messageRcv));

                            message = System.Text.Encoding.UTF8.GetString(messageRcv);

                        }
                    }

                    catch (Exception)
                    {
                        System.Environment.Exit(System.Environment.ExitCode);

                    }

                } while (message != "FIM");

                cliente.Close();

                Application.Exit();

            }

            catch (Exception error)

            {

                MessageBox.Show(error.ToString());

            }
        }



        private void txtBxSendMsg_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && connection != null)
                {
                    byte[] msgSend = Encoding.UTF8.GetBytes(txtBxNick.Text + ": " + txtBxSendMsg.Text + Environment.NewLine);

                    if (connection.Connected)
                    {
                        connection.Send(msgSend);

                        txtBxChat.Text +=  txtBxNick.Text + ": " + txtBxSendMsg.Text + Environment.NewLine;

                        txtBxSendMsg.Clear();
                    }

                }

            }

            catch (SocketException)
            {
                txtBxChat.Text += "Atenção! Erro...";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection != null)
                {
                    byte[] msgSend = Encoding.UTF8.GetBytes(txtBxNick.Text + ": " + txtBxSendMsg.Text + Environment.NewLine);

                    if (connection.Connected)
                    {
                        connection.Send(msgSend);

                        txtBxChat.Text += txtBxNick.Text + ": " + txtBxSendMsg.Text + Environment.NewLine;

                        txtBxSendMsg.Clear();
                    }

                }

            }

            catch (SocketException)
            {
                txtBxChat.Text += "Atenção! Erro...";
            }
        }




        private void sendToChatBox(string cmd, string s)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                if(cmd == "Append")
                    txtBxChat.Text += s + Environment.NewLine;
                else
                    txtBxChat.Text = s;
            };
            if(this.InvokeRequired)
            {
                this.Invoke(methodInvokerDelegate);
            }
            else
            {
                methodInvokerDelegate();
            }
        }

        private void btnConect_Click(object sender, EventArgs e)
        {
            if (btnConect.Text == "Connect")
            {
                btnConect.Text = "Disconnect";
                btnServerStart.Enabled = false;
                txtBxIP.Enabled = false;
                txtBxPort.Enabled = false;
                btnSend.Enabled = true;
                txtBxSendMsg.Enabled = true;

                ip = txtBxIP.Text;
                port = txtBxPort.Text;

                ClientSide.Start();
            }
            else
            {
                if (connection != null)
                   connection.Close();
            }

        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            if(btnServerStart.Text == "Server Start")
            {
                btnServerStart.Text = "Server End";
                btnConect.Enabled = false;
                txtBxIP.Enabled = false;
                txtBxPort.Enabled = false;
                btnSend.Enabled = true;
                txtBxSendMsg.Enabled = true;

                port = txtBxPort.Text;

                ServerSide.Start();
            }
            else
            {
                if(connection != null)
                   connection.Close();
            }

        }

    }
}
