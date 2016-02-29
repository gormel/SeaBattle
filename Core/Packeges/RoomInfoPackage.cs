using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packeges
{
	public class RoomInfoPackage : BasePackage
	{
		public Guid ID { get; set; }

		public string Name { get; set; }
		public int UserCount { get; set; }
	}
}
