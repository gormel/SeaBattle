using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class UserList : BasePackage
	{
		public List<Guid> Users { get; set; }

		public UserList()
		{
			Users = new List<Guid>();
		}
	}
}
