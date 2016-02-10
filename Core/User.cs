using System;
using System.Net.Sockets;
using Server;

namespace Core
{
	public class User
	{
		public readonly AsyncLock Lock = new AsyncLock();
		public Guid ID { get; private set; }
		public Socket Socket { get; private set; }
		public Room Room { get; set; }

		public User(Socket socket)
		{
			Socket = socket;
			ID = Guid.NewGuid();
		}
	}
}
