using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.EDS
{
	public class HashCalculator
	{
		private static HashCalculator _hashCalculator;
		private readonly int _startH;
		private HashCalculator()
		{
			_startH = 100;
		}

		public static HashCalculator GetInstance()
		{
			return _hashCalculator ?? (_hashCalculator = new HashCalculator());
		}

		public BigInteger CalculateHashCode(string text, BigInteger n)
		{
			BigInteger H = _startH;
			var arr = Encoding.ASCII.GetBytes(text);
			for (int i = 0; i < arr.Length; i++)
			{					
				H = BigInteger.ModPow(H + arr[i], 2, n);
			}
			return H;
		}
	}
}
