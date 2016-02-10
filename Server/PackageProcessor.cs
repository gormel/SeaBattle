using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Server
{
	public class PackageProcessor
	{
		private Server mServer;

		public PackageProcessor(Server mServer)
		{
			this.mServer = mServer;
		}

		public async Task ProcessPackage(User user, BasePackage pack)
		{
			dynamic p = pack;
			ProcessPackage(user, p);
		}

		public async Task ProcessPackage(User user, GetUserList package)
		{
			var userList = new UserList();
			userList.Users.AddRange(mServer.Lobby.Values.Where(u => u.Room == user.Room).Select(u => u.ID));
			await mServer.ClientListener.Send(user, userList);
		}
	}
}
