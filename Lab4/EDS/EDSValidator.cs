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
		public CryptingInfo<byte[], EDSSourceKey> SourceData { get; set; }
		public CryptingInfo<byte[], EDSKey> DataResult { get; set; }

		public void Validate()
		{
			if (!IsPrimeNumber((int)DataResult.Key.P) || !IsPrimeNumber((int)DataResult.Key.Q))
				throw new ArgumentException("Key: not prime");

			var r = DataResult.Key.P * DataResult.Key.Q;
			var phiFunc = CalcEulerFunc((int)r);
			var E = CalculateOpenKey((int)r, phiFunc);

			if ((E * DataResult.Key.D) % phiFunc != 1)
				throw new ArgumentException("Key: e*d mod f != 1");

			DataResult.Key.E = E;
		}

		private static int CalculateOpenKey(int rKey, int phiFunc)
		{
			int result = 0;
			var modSet = new HashSet<int>();
			for (int i = 1; i <= rKey - 1; i++)
			{
				bool IsError = false;
				for (int j = 1; j <= phiFunc; j++)
				{
					var modRes = BigInteger.ModPow(i, j, rKey);

					if (modRes == 0 || modSet.Contains((int)modRes))
					{
						IsError = true;
						break;
					}
					else
						modSet.Add((int)modRes);
				}

				if (!IsError)
				{
					result = i;
					break;
				}
			}

			return result;
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
