using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
	public interface IBD
	{
		Task Add(IBDObject obj);
		Task Remove(IBDObject obj);
		Task<IBDObject> Get(Guid id);
		Task Update(IBDObject obj);
		Task<IEnumerable<T>> Find<T>(Func<T, bool> condition) where T : IBDObject;
	}
}
