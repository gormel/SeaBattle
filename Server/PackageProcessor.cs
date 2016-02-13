using System;
using System.Linq;
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
			userList.Users.AddRange(mServer.Lobby.Values.Where(u => u.Room == user.Room || u.Room.ID == user.Room.ID).Select(u => u.ID));
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
			user.Name = foundUser.Name;
			if (foundUser.RoomID != Guid.Empty && mServer.Rooms.ContainsKey(foundUser.RoomID))
				user.Room = mServer.Rooms[foundUser.RoomID];

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

		public async Task ProcessPackage(User user, UserInfoPackage package)
		{
			var result = new UserInfoPackage();
			if (!mServer.Lobby.ContainsKey(package.ID))
			{
				result.ID = Guid.Empty;
				await mServer.ClientListener.Send(user, result);
				return;
			}

			var info = mServer.Lobby[package.ID];
			result.ID = info.ID;
			result.Name = info.Name;
			result.RoomID = info.Room != null ? info.Room.ID : Guid.Empty;
			await mServer.ClientListener.Send(user, result);
		}

		public async Task ProcessPackage(User user, RoomInfoPackage package)
		{
			var result = new RoomInfoPackage();
			if (!mServer.Rooms.ContainsKey(package.ID))
			{
				result.ID = Guid.Empty;
				await mServer.ClientListener.Send(user, result);
				return;
			}
			var info = mServer.Rooms[package.ID];
			result.ID = info.ID;
			result.Name = info.Name;
			result.UserCount = mServer.Lobby.Values.Count(u => u.Room != null && u.Room.ID == package.ID);
			await mServer.ClientListener.Send(user, result);
		}

		public async Task ProcessPackage(User user, RoomCreatePackage package)
		{
			var room = new Room();
			room.Name = package.Name;

			mServer.Rooms.TryAdd(room.ID, room);

			var result = new RoomCreatePackage();
			result.ID = room.ID;
			await mServer.ClientListener.Send(user, result);
		}

		public async Task ProcessPackage(User user, RoomList package)
		{
			RoomList result = new RoomList();
			result.Rooms.AddRange(mServer.Rooms.Values.Select(r => r.ID));
			await mServer.ClientListener.Send(user, result);
		}
	}
}
