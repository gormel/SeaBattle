using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public class ServerConfig
	{
		public int Port { get; private set; }
		public string IP { get; private set; }
		public int LoginQueueSize { get; private set; }

		public ServerConfig()
		{
			Port = 666;
			IP = "0.0.0.0";
			LoginQueueSize = 100;
		}

	}
}
