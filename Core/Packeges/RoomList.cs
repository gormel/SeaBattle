using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packeges
{
	public class RoomList : BasePackage
	{
		public RoomList()
		{
			Rooms = new List<Guid>();
		}

		public List<Guid> Rooms { get; private set; } 
	}
}
