using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Elgamal_Method
{
	public class ElgamalValidator : IValidator
	{
		public CryptingInfo<byte[], ElgamalSourceKey> SourceData { get; set; }
		public CryptingInfo<byte[], ElgamalKey> DataResult { get; set; }

		public void Validate()
		{
			if (!IsPrimeNumber(DataResult.Key.P))
				throw new ArgumentException("Key: P");

			if (DataResult.Key.P <= 1 || DataResult.Key.X <= 1 || DataResult.Key.K <= 1)
				throw new ArgumentException("Key: less/eq than 1");

			if (DataResult.Key.X >= DataResult.Key.P - 1)
				throw new ArgumentException("Key: X more than P-1");

			if (DataResult.Key.K >= DataResult.Key.P - 1)
				throw new ArgumentException("Key: K more than P-1");
			if (!IsCoprime(DataResult.Key.K, DataResult.Key.P - 1))
				throw new ArgumentException("Key: K is not coprime P");

			if (!DataResult.Key.G.HasValue)
			{
				var openKeys = GetOpenKey(DataResult.Key.P);
				if (openKeys.Count == 0)
					throw new ArgumentException("Key: P don't have original root");

				DataResult.Key.G = openKeys.First();
			}
				

			DataResult.Key.Y = (int)BigInteger.ModPow(DataResult.Key.G.Value, DataResult.Key.X, DataResult.Key.P);
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
	
		public static List<int> GetOpenKey(int pKey)
		{
			var phiFunc = CalcEulerFunc(pKey);

			var result = new List<int>();
			var modSet = new HashSet<int>();
			for (int i = 1; i <= pKey - 1; i++)
			{
				bool IsError = false;
				for (int j = 1; j <= phiFunc; j++)
				{
					var modRes = BigInteger.ModPow(i, j, pKey);

					if (modRes == 0 || modSet.Contains((int)modRes))
					{
						IsError = true;
						break;
					}
					else
						modSet.Add((int)modRes);
				}

				if (!IsError)
					result.Add(i);

				modSet.Clear();
			}

			return result;	
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

		private static List<int> GetOpenKeys(int pKey)
		{
			var divs = GetPrimeDiv(pKey);
			List<int> result = new List<int>();
			for (int i = 0; i < pKey; i++)
			{
				bool a = true;
				foreach (var item in divs)
				{
					if (BigInteger.ModPow(i, CalcEulerFunc(pKey) / item, pKey) == 1 && BigInteger.ModPow(i, CalcEulerFunc(pKey), pKey) != 1)
					{
						a = false;
						break;
					}
				}
				if (a)
				{
					result.Add(i);
				}
			}
			return result;
		}

		private static ICollection<int> GetPrimeDiv(int sourceNumb)
		{
			HashSet<int> result = new HashSet<int>();
			for (int i = 2; i <= Math.Sqrt(sourceNumb); i++)
			{
				while (sourceNumb % i == 0)
				{
					result.Add(i);
					sourceNumb /= i;
				}
			}

			if (sourceNumb != 1)
				result.Add(sourceNumb);

			return result;
		}

		static int ModPow(int x, int y, int p)
		{
			int res = 1; 

			x = x % p; 

			if (x == 0)
				return 0; 

			while (y > 0)
			{
				if ((y & 1) != 0)
					res = (res * x) % p;

				y = y >> 1;
				x = (x * x) % p;
			}
			return res;
		}
	}
}
