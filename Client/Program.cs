using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
			socket.Connect("localhost", 666);
			while (true)
			{
				var command = Console.ReadLine();
				var builder = new StringBuilder();
				JsonSerializer.CreateDefault().Serialize(new StringWriter(builder), new GetUserList() );
				var bytes = Encoding.UTF8.GetBytes(builder.ToString());
				var sended = 0;
				Send(socket, BitConverter.GetBytes(bytes.Length));
				Send(socket, bytes);
			}
		}

		private static void Send(Socket s, byte[] data)
		{
			var sended = 0;
			while ((sended = sended + s.Send(data, sended, data.Length - sended, SocketFlags.None)) < data.Length) ;
		}
	}
}
