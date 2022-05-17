using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.EDS
{
	public class EDSValidator : IValidator
	{
		public CryptingInfo<string, EDSSourceKey> SourceData { get; set; }
		public CryptingInfo<string, EDSKey> DataResult { get; set; }

		public void Validate()
		{
			if (!IsPrimeNumber((int)DataResult.Key.P) || !IsPrimeNumber((int)DataResult.Key.Q))
				throw new ArgumentException("Key: not prime");

			var r = DataResult.Key.P * DataResult.Key.Q;
			var phiFunc = CalcEulerFunc((int)r);
			var E = inverse((int)DataResult.Key.D, phiFunc);

			if (E <= 1)
				throw new ArgumentException("Key: E error");
			if ((E * DataResult.Key.D) % phiFunc != 1)
				throw new ArgumentException("Key: e*d mod f != 1");

			DataResult.Key.E = E;
		}

		void extended_euclid(long a, long b,out long x, out long y, out long d)
		{
			long q, r, x1, x2, y1, y2;

			if (b == 0)
			{

				d = a; x = 1; y = 0;

				return;
			}

			x2 = 1; x1 = 0; y2 = 0; y1 = 1;

			while (b > 0)
			{
				q = a / b;
				r = a - q * b;
				x = x2 - q * x1;
				y = y2 - q * y1;
				a = b;
				b = r;
				x2 = x1;
				x1 = x;
				y2 = y1;
				y1 = y;
			}
			d = a;
			x = x2;
			y = y2;
		}

		long inverse(long a, long n)
		{
			long d, x, y;

			extended_euclid(a, n,out x,out y,out d);
			if (d == 1) return x;
			return 0;

		}

		private static bool IsPrimeNumber(int x)
        {
			if (x < 2) 
				return false;
			for (int i = 2; i * i <= x; i++)
				if ((x % i) == 0) 
					return false;
			return true;
		}

		private static int CalcEulerFunc(int pKey)
		{
			int counter = 0;
			for (int i = 1; i <= pKey - 1; i++)
			{
				if (IsCoprime(i, pKey))
					counter++;
			}
			return counter;
		}

		private static bool IsCoprime(int num1, int num2)
		{
			if (num1 == num2)
			{
				return num1 == 1;
			}
			else
			{
				if (num1 > num2)
				{
					return IsCoprime(num1 - num2, num2);
				}
				else
				{
					return IsCoprime(num2 - num1, num1);
				}
			}
		}
	}
}
