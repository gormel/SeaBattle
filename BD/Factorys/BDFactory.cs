using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.BDImplimentations;

namespace BD.Factorys
{
	public static class BDFactory
	{
		public static readonly IBD FileBD = new FileBD("./BD");
	}
}
