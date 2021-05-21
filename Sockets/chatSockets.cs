using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

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

        private static uint connectionNumber = 1;

        private static int chBxCounter = 0;

        private byte[] idDestination = new byte[8];

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

            if (connectionNumber < 255)
            {
                server.BeginAcceptSocket(AcceptCallback, null);
            }
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            Protocol protocol = new Protocol();
            
            User localUser;

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

                    case 0x03:

                        for (uint count = 0; count < 8; count++)
                        {
                            if (clientUsers.Count > count)
                            {
                                localUser = clientUsers.Find(client => client.userGetID() == protocol.idDestination[count]);

                                if (localUser != null && localUser.socket.Connected)
                                {
                                    
                                    localUser.socket.Send(protocol.parseData(0x03, protocol.idOrigin, protocol.idDestination, Encoding.UTF8.GetBytes(protocol.message)));

                                }
                            }

                        }

                        sendToChatBox("Append", clientUsers.Find(c => c.userGetID() == protocol.idOrigin).userGetNick() + ": " + protocol.message.Replace("\0", string.Empty));

                        break;
                }

            }
          
            current.BeginReceive(buffer, 0, 2048, SocketFlags.None, ReceiveCallback, current);
        }

        public void RunClient()
        {

            TcpClient cliente;
            Protocol protocol = new Protocol();
            User user;

            string clientTable = "";
            string clientId;
            string clientNick;

            bool clientNumberFlag = false;
            bool clientNickNameflag = false;

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
                                                    connection.Send(protocol.parseData(0x01, myID, Enumerable.Repeat((byte)0x00, 8).ToArray(), Encoding.UTF8.GetBytes(txtBxNick.Text)));
                                                }
                                            }

                                            break;

                                        case 0x02:

                                            if (protocol.idOrigin == 0xFF)
                                            {
                                                clientTable = "";

                                                clientUsers.Clear();

                                                char[] messageChars = protocol.message.ToCharArray();

                                                clientId = "";
                                                clientNick = "";

                                                //busca os caracteres e atualiza a tabela de usuários
                                                foreach (char c in messageChars)
                                                {

                                                    if (c == ']')
                                                    {
                                                        clientNumberFlag = false;
                                                    }
                                                    else if (c == '}')
                                                    {
                                                        clientNickNameflag = false;
                                                        clientTable += Environment.NewLine;

                                                        user = new User(null, Convert.ToUInt16(clientId));
                                                        user.userSetNick(clientNick);

                                                        clientUsers.Add(user);

                                                        clientId = "";
                                                        clientNick = "";

                                                    }

                                                    if (clientNumberFlag )
                                                    {
                                                        clientId += c;
                                                    }
                                                    if(clientNickNameflag)
                                                    {
                                                        clientNick += c;
                                                    }

                                                    if (c == '[')
                                                    {
                                                        clientNumberFlag = true;
                                                    }
                                                    else if (c == '{')
                                                    {
                                                        clientNickNameflag = true;
                                                        clientTable += " ";
                                                    }
                                                    else if(c != ']' && c != '}')
                                                        clientTable += c;
                                                }

                                                sendToUsersBox("",clientTable);
                                            }

                                            break;

                                        case 0x03:

                                            if(protocol.idOrigin == 0xFF)
                                            {
                                                sendToChatBox("Append", "Server: " + protocol.message.Replace("\0", string.Empty));
                                            }
                                            else
                                            {
                                                sendToChatBox("Append", clientUsers.Find(c => c.userGetID() == protocol.idOrigin).userGetNick() + ": " + protocol.message.Replace("\0", string.Empty));
                                            }
  
                                            break;
                                    }
                                }
                            }

                            messageRcv = new byte[2048];
                           
                        }
                    }

                    catch (Exception)
                    {
                        Environment.Exit(Environment.ExitCode);
                    }

                } while (true);

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
            Protocol protocol = new Protocol();
            try
            {
                if (connection != null || clientSockets.Count > 0)
                {
                    byte[] msgSend = Encoding.UTF8.GetBytes(txtBxSendMsg.Text);

                    //parte do cliente
                    if (connection != null)
                    {
                        if (connection.Connected)
                        {
                            findIdDestination();

                            connection.Send(protocol.parseData(0x03, myID, idDestination, msgSend));

                            txtBxChat.Text += txtBxNick.Text + ": " + txtBxSendMsg.Text + Environment.NewLine;

                            txtBxSendMsg.Clear();
                        }
                    }

                    //parte do servidor
                    if (clientSockets.Count > 0)
                    {
                        clientSockets.ForEach(client =>
                        {
                            if (client.Connected)
                            {
                                client.Send(protocol.parseData(0x03, 0xFF, Enumerable.Repeat((byte)0x00, 8).ToArray(), msgSend));

                            }
                        });

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


        private void sendToUsersBox(string cmd, string s)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                if (cmd == "Append")
                    txtBxUsers.Text += s + Environment.NewLine;
                else
                    txtBxUsers.Text = s;
            };
            if (this.InvokeRequired)
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
                        client.Send(protocol.parseData(0x02, 0xFF, Enumerable.Repeat((byte)0x00, 8).ToArray(), Encoding.UTF8.GetBytes(clientTable)));
                    }
                });

            }

        }

        private void findIdDestination()
        {
            uint i = 0;
            idDestination = new byte[8];

            if (chBxUser1.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[0].userGetID());

            if (chBxUser2.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[1].userGetID());

            if (chBxUser3.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[2].userGetID());

            if (chBxUser4.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[3].userGetID());

            if (chBxUser5.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[4].userGetID());

            if (chBxUser6.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[5].userGetID());

            if (chBxUser7.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[6].userGetID());

            if (chBxUser8.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[7].userGetID());

            if (chBxUser9.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[8].userGetID());

            if (chBxUser10.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[9].userGetID());

            if (chBxUser11.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[10].userGetID());

            if (chBxUser12.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[11].userGetID());

            if (chBxUser13.Checked && clientUsers.Count > i)
                idDestination[i++] = Convert.ToByte(clientUsers[12].userGetID());
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
                txtBxNick.Enabled = false;

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
                txtBxNick.Text = "Server";
                txtBxNick.Enabled = false;

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

        private void chBxUsers_Click(object sender, EventArgs e)
        {
            CheckBox chBx = (CheckBox)sender;
            if (chBx.Checked)
            {
                if (chBxCounter >= 8)
                {
                    sendToChatBox("Append", "NUMERO MAXIMO DE USUARIOS SELECIONADOS DEVE SER MENOR OU IGUAL A 8");
                    chBx.Checked = false;
                }
                else
                    chBxCounter++;
            }
            else
                chBxCounter--;

        }
    }
}
