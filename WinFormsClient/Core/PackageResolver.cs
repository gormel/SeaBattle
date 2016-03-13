using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Packeges;

namespace WinFormsClient
{
	public class PackageResolver
	{
		private readonly Dictionary<Type, Delegate> mHandlers = new Dictionary<Type, Delegate>(); 

		public void AddHandler<T>(Action<T> handler) where T : BasePackage
		{
			if (!mHandlers.ContainsKey(typeof (T)))
			{
				mHandlers[typeof (T)] = handler;
				return;
			}

			var dHandler = mHandlers[typeof(T)];
			mHandlers[typeof(T)] = Delegate.Combine(dHandler, handler);
		}

		public void RemoveHandler<T>(Action<T> handler) where T : BasePackage
		{
			if (mHandlers.ContainsKey(typeof (T)))
			{
				var result = Delegate.Remove(mHandlers[typeof (T)], handler);
				if (result == null)
					mHandlers.Remove(typeof (T));
				else
					mHandlers[typeof (T)] = result;
			}
		}

		public void ResolvePackage(BasePackage pack)
		{
			if (mHandlers.ContainsKey(pack.GetType()))
				mHandlers[pack.GetType()].DynamicInvoke(pack);
		}
	}
}
