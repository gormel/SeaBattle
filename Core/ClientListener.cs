using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core
{
	public class ClientTalker
	{
		private readonly JsonSerializer mSerializer = JsonSerializer.CreateDefault();
		
		public async Task Send(User user, BasePackage package)
		{
			var builder = new StringBuilder();
			JsonSerializer.CreateDefault().Serialize(new StringWriter(builder), package );
			var bytes = Encoding.UTF8.GetBytes(builder.ToString());
			using (await user.Lock.LockAsync())
			{
				await SendBytes(user.Socket, BitConverter.GetBytes(bytes.Length));
				await SendBytes(user.Socket, bytes);
			}
		}

		public async Task StartListen(User user, Action<BasePackage> packageActions )
		{
			while (true)
			{
				var countBytes = await ReadBytes(user.Socket, 4);
				var count = BitConverter.ToInt32(countBytes, 0);

				var data = await ReadBytes(user.Socket, count);

				var stringData = Encoding.UTF8.GetString(data);

				dynamic obj = JObject.Parse(stringData);
				var className = (string)obj.ClassName;
				var type = typeof(BasePackage).Assembly.GetTypes().First(t => t.FullName == className);
				var realObj = (BasePackage)mSerializer.Deserialize(new StringReader(stringData), type);
				packageActions(realObj);
			}
		}

		private async Task SendBytes(Socket s, byte[] data)
		{
			var sended = 0;
			while (sended < data.Length)
			{
				sended += await s.SendTaskAsync(data, sended, data.Length - sended, SocketFlags.None);
			}
		}

		private async Task<byte[]> ReadBytes(Socket s, int byteCount)
		{
			var recived = 0;
			var buffer = new byte[byteCount];
			while (recived < byteCount)
			{
				var nowRecived = await s.ReceiveTaskAsync(buffer, recived, byteCount - recived, SocketFlags.None);
				if (nowRecived == 0)
					throw new IOException("Disconnected!");
				recived += nowRecived;
			}
			return buffer;
		}
	}
}
