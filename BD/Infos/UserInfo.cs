using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Infos
{
	public class UserInfo : IBDObject
	{
		public Guid ID { get; set; }

		public string Name { get; set; }
		public string PasswordHash { get; set; }
		public Guid RoomID { get; set; }
	}
}
