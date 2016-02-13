using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BD.BDImplimentations
{
	internal class FileBD : IBD
	{
		private readonly JsonSerializerSettings mSerializerSettings = new JsonSerializerSettings()
		{
			TypeNameHandling = TypeNameHandling.All
		};

		public string Folder { get; private set; }

		public FileBD(string folder)
		{
			Folder = folder;
			Directory.CreateDirectory(Folder);
		}

		private FileInfo GetFullPath(Guid id)
		{
			var idStr = id.ToString();
			return new FileInfo(Path.Combine(Folder, idStr.Substring(0, 2), idStr));
		}

		public async Task Add(IBDObject obj)
		{
			var data = await Task.Run(() => JsonConvert.SerializeObject(obj, Formatting.Indented, mSerializerSettings));
			var path = GetFullPath(obj.ID);

			Directory.CreateDirectory(path.DirectoryName);
			using (var stream = new FileStream(path.FullName, FileMode.OpenOrCreate))
			using (var writer = new StreamWriter(stream))
			{
				await writer.WriteAsync(data);
			}
		}

		public async Task Remove(IBDObject obj)
		{
			var path = GetFullPath(obj.ID);
			await Task.Run(() => File.Delete(path.FullName));
		}

		public async Task<IBDObject> Get(Guid id)
		{
			var path = GetFullPath(id);
			if (!path.Exists)
				return null;

			using (var reader = new StreamReader(path.FullName))
			{
				var text = await reader.ReadToEndAsync().ConfigureAwait(false);
				var obj = JsonConvert.DeserializeObject(text, mSerializerSettings);
				return (IBDObject) obj;
			}
		}

		public async Task<IEnumerable<T>> Find<T>(Func<T, bool> condition) where T : IBDObject
		{
			var files = Directory.EnumerateFiles(Folder, "*", SearchOption.AllDirectories);
			var tasks = files.Select(Path.GetFileName).Select(n => Get(Guid.Parse(n))).ToArray();
			await Task.WhenAll(tasks).ConfigureAwait(false);
			return tasks.Select(t => t.Result).OfType<T>().Where(condition);
		}

		public async Task Update(IBDObject obj)
		{
			var path = GetFullPath(obj.ID);
			if (!path.Exists)
				return;
			var data = await Task.Run(() => JsonConvert.SerializeObject(obj, Formatting.Indented, mSerializerSettings));

			using (var stream = new FileStream(path.FullName, FileMode.OpenOrCreate))
			using (var writer = new StreamWriter(stream))
			{
				await Task.Run(() => stream.SetLength(0));
				await writer.WriteAsync(data);
			}
		}
	}
}
