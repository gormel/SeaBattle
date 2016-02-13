using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using BD;
using BD.Factorys;
using Core;

namespace Server
{
	public class Server
	{
		public ConcurrentDictionary<Guid, User> Lobby { get; private set; }
		public ConcurrentDictionary<Guid, Room> Rooms { get; private set; } 

		public ServerConfig Config { get; private set; }
		private Socket mListener;
		private Task mainTask;
		public ClientTalker ClientListener { get; private set; }
		public PackageProcessor PackageProcessor { get; private set; }
		public IBD BD { get; private set; }

		public Server()
		{
			Lobby = new ConcurrentDictionary<Guid, User>();
			Rooms = new ConcurrentDictionary<Guid, Room>();
			Config = null;
			mListener = new Socket(SocketType.Stream, ProtocolType.Tcp);
			ClientListener = new ClientTalker();
			PackageProcessor = new PackageProcessor(this);
			BD = BDFactory.FileBD;
		}
		
		public void LoadConfig()
		{
			Config = new ServerConfig();
		}

		

		private async Task MainTaskBody()
		{
			if (mListener != null)
				mListener.Dispose();
			mListener = new Socket(SocketType.Stream, ProtocolType.Tcp);
			mListener.Bind(new IPEndPoint(IPAddress.Parse(Config.IP), Config.Port));
			mListener.Listen(Config.LoginQueueSize);
			while (true)
			{
				var task = Task.Factory.FromAsync(mListener.BeginAccept, new Func<IAsyncResult, Socket>(mListener.EndAccept), null);
				var client = await task;
				var user = new User(client);
				Console.WriteLine("Connected user: " + user.ID);
				Lobby.TryAdd(user.ID, user);
				ClientListener.StartListen(user, p => PackageActions(user, p))
							  .ContinueWith(t =>
				{
					User us;
					Lobby.TryRemove(user.ID, out us);
					if (t.IsFaulted || t.IsCanceled)
						Console.WriteLine("Disconnected user: " + user.ID);
				});
			}
		}

		private void PackageActions(User user, BasePackage package)
		{
			PackageProcessor.ProcessPackage(user, package).ContinueWith(t =>
			{
				if (t.IsFaulted)
				{
					Console.WriteLine("Error while processing pacage: \r\n" + t.Exception);
				}
			});
		}

		public void Run()
		{
			if (Config == null)
				throw new Exception("Config is not loaded!");
			mainTask = Task.Run(() => MainTaskBody());
		}
	}
}
