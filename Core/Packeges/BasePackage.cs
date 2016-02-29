using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class BasePackage
	{
		public string ClassName { get { return GetType().FullName; } }
	}
}
