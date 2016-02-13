using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Infos;
using Core;
using Core.Packeges;

namespace Server
{
	public class PackageProcessor
	{
		private readonly Server mServer;

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

		public async Task ProcessPackage(User user, Login package)
		{
			var users = (await mServer.BD.Find<UserInfo>(u => u.Name == package.Name)).ToArray();
			var result = new Login();
			if (!users.Any())
			{
				result.ID = Guid.Empty;
				await mServer.ClientListener.Send(user, result);
				return;
			}
			var foundUser = users.Single();
			if (!BCrypt.Net.BCrypt.Verify(package.Password, foundUser.PasswordHash))
			{
				result.ID = Guid.Empty;
				await mServer.ClientListener.Send(user, result);
				return;
			}

			user.ID = foundUser.ID;
			result.ID = foundUser.ID;
			await mServer.ClientListener.Send(user, result);
		}

		public async Task ProcessPackage(User user, Register package)
		{
			var users = (await mServer.BD.Find<UserInfo>(u => u.Name == package.Name)).ToArray();
			var result = new Register();
			if (users.Any())
			{
				result.Result = false;
				await mServer.ClientListener.Send(user, result);
				return;
			}
			await mServer.BD.Add(new UserInfo
			{
				ID = Guid.NewGuid(),
				Name = package.Name,
				RoomID = Guid.Empty,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(package.Password)
			});
			result.Result = true;
			await mServer.ClientListener.Send(user, result);

		}
	}
}
