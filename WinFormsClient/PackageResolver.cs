using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace WinFormsClient
{
	public class PackageResolver
	{
		public event EventHandler<List<string>> OnUpdateUserList;
		public void ResolvePackage(BasePackage pack)
		{
			dynamic p = pack;
			ResolvePackage(p);
		}

		private void ResolvePackage(UserList pack)
		{
			if (OnUpdateUserList != null)
				OnUpdateUserList(this, new List<string>(pack.Users.Select(id => id.ToString())));
		}
	}
}
