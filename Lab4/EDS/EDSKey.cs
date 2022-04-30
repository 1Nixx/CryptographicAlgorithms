using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.EDS
{
	public class EDSKey
	{
		public BigInteger P { get; set; }
		public BigInteger Q { get; set; }
		public BigInteger E { get; set; }
		public BigInteger D { get; set; }
		public BigInteger? EDS { get; set; }
	}
}
