﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packeges
{
	public class Login : BasePackage
	{
		public Guid ID { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
	}
}
