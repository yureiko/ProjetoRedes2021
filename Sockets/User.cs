using System;
using System.Net.Sockets;


public class User
{
	private uint id;
	private string nick;
	//private System.Net.EndPoint ip;
	public Socket socket;

	public User(Socket socketRcv, uint connectionNumber)
	{
		socket = socketRcv;
		id = connectionNumber;
	}

	public uint userGetID()
    {
		return id;
	}
	public void userSetID(uint number)
	{
		id = number;
	}

	public string userGetNick()
	{
		return nick;
	}
	public void userSetNick(string name)
	{
		nick = name;
	}

	public System.Net.EndPoint userGetIP()
    {
		return socket.RemoteEndPoint;
    }


}
