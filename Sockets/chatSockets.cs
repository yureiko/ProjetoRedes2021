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

        private byte myID;

        private string message = "";

        private static uint connectionNumber = 0;

        //private Protocol protocol = new Protocol();

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
            Protocol protocol = new Protocol();

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
            clientUsers.Add(new User(socket, connectionNumber));

            socket.BeginReceive(buffer, 0, 2048, SocketFlags.None, ReceiveCallback, socket);

            sendToChatBox("Append", "Cliente novo conectado " + socket.RemoteEndPoint.ToString());

            //requisita o nickname do cliente
            socket.Send(protocol.parseData(0x01, 0xFF, Enumerable.Repeat(Convert.ToByte(connectionNumber), 8).ToArray(), Enumerable.Repeat((byte)0x00, 8).ToArray()));
            connectionNumber++;

            server.BeginAcceptSocket(AcceptCallback, null);

            //System.Net.EndPoint temp = socket.RemoteEndPoint;
           


        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            Protocol protocol = new Protocol();

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

                //procura o usuário cujo socket é igual ao que está sendo finalizado
                if (clientUsers.Count > 0)
                {
                    clientUsers.Remove(clientUsers.Find(x => x.socket == current));
                    clientSockets.Remove(current);
                    updateClientTable();
                }

                return;
            }

            byte[] recBuf = new byte[received];

            Array.Copy(buffer, recBuf, received);

            /*
             Aqui vai uma função que deve separar a mensagem e trata-la de acordo com os usuarios recebidos
             */

            if (protocol.unParseData(recBuf))
            {
                switch(protocol.opcode)
                {
                    case 0x01:

                        for(int count = 0; count < clientUsers.Count; count++)
                        {
                            if(clientUsers[count].userGetID() == Convert.ToByte(protocol.idOrigin))
                            {
                                clientUsers[count].userSetNick(protocol.message.Replace("\0", string.Empty));

                                //setou o nick name corretamente, agora precisa mandar a tabela atualizada para todos os clientes
                                updateClientTable();

                                break;
                            }
                        }

                        break;

                    case 0x02:




                        break;
                }


            }

            string text = Encoding.ASCII.GetString(recBuf);

            sendToChatBox("Append", text);

            if (text.Contains("FIM")) // Client wants to exit gracefully
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);

                current.Close();

                clientSockets.Remove(current);

                //procura o usuário cujo socket é igual ao que está sendo finalizado
                clientUsers.Remove(clientUsers.Find(x => x.socket == current));

                updateClientTable();

                sendToChatBox("Append", "Cliente desconectado" + current.RemoteEndPoint.ToString());

                return;
            }
          
            current.BeginReceive(buffer, 0, 2048, SocketFlags.None, ReceiveCallback, current);
        }

        public void RunClient()
        {

            TcpClient cliente;
            Protocol protocol = new Protocol();

            string clientTable = "";

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

                            if(received > 0)
                            {
                                //manda dados recebidos para serem separados e se retonar verdadeiro, trata 
                                if(protocol.unParseData(data))
                                { 
                                    switch(protocol.opcode)
                                    {
                                        case 0x01:

                                            //verifica se veio do servidor
                                            if (protocol.idOrigin == 0xFF)
                                            {
                                                myID = protocol.idDestination[0];

                                                if (connection != null)
                                                {
                                                    //manda o nickname para o servidor
                                                    connection.Send(protocol.parseData(0x01, myID, Enumerable.Repeat((byte)0x00, 8).ToArray(), Encoding.ASCII.GetBytes(txtBxNick.Text)));

                                                }
                                            }

                                            break;

                                        case 0x02:

                                            if (protocol.idOrigin == 0xFF)
                                            {
                                                txtBxUsers.Clear();
                                                clientTable = "";
                                                char[] messageChars = protocol.message.ToCharArray();

                                                //busca os caracteres e atualiza a tabela de usuários
                                                foreach(char c in messageChars)
                                                {
                                                   
                                                    if (c == '{')
                                                    {
                                                        clientTable += " ";
                                                    }
                                                    else if (c == '}')
                                                    {
                                                        clientTable += Environment.NewLine;
                                                    }
                                                    else
                                                        clientTable += c;
                                                }

                                                txtBxUsers.Text = clientTable;
                                            

                                            }

                                                break;

                                    }
                                }
                            }

                            //sendToChatBox("Append", message);

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
             if (e.KeyCode == Keys.Enter && (connection != null || clientSockets.Count > 0))
             {
                 sendConnectionMessage();
             }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            sendConnectionMessage();
        }

        private void sendConnectionMessage()
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

        private void updateClientTable()
        {
            Protocol protocol = new Protocol();

            string clientTable = "";

            if (clientUsers.Count > 0)
            {
                clientUsers.ForEach(User =>

                clientTable += "[" + User.userGetID().ToString() + "]" + "{" + User.userGetNick() + "}"

                );

                clientSockets.ForEach(client =>
                {
                    if (client.Connected)
                    {
                        client.Send(protocol.parseData(0x02, 0xFF, Enumerable.Repeat((byte)0x00, 8).ToArray(), Encoding.ASCII.GetBytes(clientTable)));
                        
                    }
                });



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
