using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Server
{
	public class Room
	{
		public Guid ID { get; private set; }
		public Room()
		{
			ID = Guid.NewGuid();
		}

	}
}
