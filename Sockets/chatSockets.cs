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

    public partial class chatSockets : Form
    {
        private TcpListener server;

        private Socket connection;

        private static List<Socket> clientSockets = new List<Socket>();

        private static List<User> clientUsers = new List<User>();

        private static readonly byte[] buffer = new byte[2048];

        private Thread ServerSide;

        private Thread ClientSide;

        private string ip;

        private string port;

        private string message = "";

        private static uint connectionNumber = 0;

        public chatSockets()
        {
            InitializeComponent();

            ServerSide = new Thread(new ThreadStart(RunServer));

            ClientSide = new Thread(new ThreadStart(RunClient));

            btnSend.Enabled = false;

            txtBxSendMsg.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null)
                connection.Close();

            if (clientSockets.Count > 0)
            {
                clientSockets.ForEach(client =>
                {
                    client.Close();
                });

                server.Stop();
            }

            Application.Exit();
        }



        public void RunServer()

        {
            server = new TcpListener(System.Net.IPAddress.Any, Convert.ToInt32(port));

            server.Start();

            sendToChatBox("", "Aguardando Conexoes" + Environment.NewLine);

            server.BeginAcceptSocket(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = server.EndAcceptSocket(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSockets.Add(socket);

            //adiciona o novo cliente conectado na lista do cliente e atribui a ele um ID
            clientUsers.Add(new User(socket, connectionNumber++));

            socket.BeginReceive(buffer, 0, 2048, SocketFlags.None, ReceiveCallback, socket);

            sendToChatBox("Append", "Cliente novo conectado " + socket.RemoteEndPoint.ToString());

            server.BeginAcceptSocket(AcceptCallback, null);

            //System.Net.EndPoint temp = socket.RemoteEndPoint;
           


        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;

            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                sendToChatBox("Append", "Cliente desconectado " + current.RemoteEndPoint.ToString());

                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();

                return;
            }

            byte[] recBuf = new byte[received];

            Array.Copy(buffer, recBuf, received);

            /*
             Aqui vai uma função que deve separar a mensagem e trata-la de acordo com os usuarios recebidos
             */

            string text = Encoding.ASCII.GetString(recBuf);

            sendToChatBox("Append", text);

            if (text.Contains("FIM")) // Client wants to exit gracefully
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);

                current.Close();

                clientSockets.Remove(current);

                sendToChatBox("Append", "Cliente desconectado" + current.RemoteEndPoint.ToString());

                return;
            }
          
            current.BeginReceive(buffer, 0, 2048, SocketFlags.None, ReceiveCallback, current);
        }

        public void RunClient()
        {

            TcpClient cliente;

            byte[] messageRcv = new byte[2048];

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
                            int received = connection.Receive(messageRcv);

                            var data = new byte[received];

                            Array.Copy(messageRcv, data, received);

                            message = Encoding.ASCII.GetString(data);

                            sendToChatBox("Append", message);

                            messageRcv = new byte[2048];
                        }
                    }

                    catch (Exception)
                    {
                        Environment.Exit(Environment.ExitCode);

                    }

                } while (!message.Contains("FIM"));

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
                if (e.KeyCode == Keys.Enter && (connection != null || clientSockets.Count > 0))
                {
                    byte[] msgSend = Encoding.ASCII.GetBytes(txtBxNick.Text + ": " + txtBxSendMsg.Text);

                    if (connection != null)
                    {
                        if (connection.Connected)
                        {
                            if (txtBxSendMsg.Text == "FIM")
                            {
                                connection.Close();
                            }

                            connection.Send(msgSend);

                            txtBxChat.Text += txtBxNick.Text + ": " + txtBxSendMsg.Text + Environment.NewLine;

                            txtBxSendMsg.Clear();
                        }
                    }

                    if (clientSockets.Count > 0)
                    {
                        clientSockets.ForEach(client =>
                        {
                            if (client.Connected)
                            {
                                client.Send(msgSend);

                                if (txtBxSendMsg.Text == "FIM")
                                {
                                    client.Close();
                                }
                            }
                        });


                        if (txtBxSendMsg.Text == "FIM")
                        {
                            server.Stop();
                            Application.Exit();
                        }

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

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection != null || clientSockets.Count > 0)
                {
                    byte[] msgSend = Encoding.ASCII.GetBytes(txtBxNick.Text + ": " + txtBxSendMsg.Text);

                    if (connection != null)
                    {
                        if (connection.Connected)
                        {
                            if (txtBxSendMsg.Text == "FIM")
                            {
                                connection.Close();
                            }

                            connection.Send(msgSend);

                            txtBxChat.Text += txtBxNick.Text + ": " + txtBxSendMsg.Text + Environment.NewLine;

                            txtBxSendMsg.Clear();
                        }
                    }

                    if (clientSockets.Count > 0)
                    {
                        //tirar essa função daqui e mandar para uma que trate no servidor (procura o(s) cliente(s) e manda a mensagem)
                        clientSockets.ForEach(client =>
                        {
                            if (client.Connected)
                            {
                                client.Send(msgSend);

                                if (txtBxSendMsg.Text == "FIM")
                                {
                                    client.Close();
                                }
                            }
                        });


                        if (txtBxSendMsg.Text == "FIM")
                        {
                            server.Stop();

                            Application.Exit();
                        }

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
                if (clientSockets.Count > 0)
                {
                    clientSockets.ForEach(client =>
                    {
                        client.Close();
                    });
                }

                server.Stop();
                Application.Exit();
            }

        }

    }
}
