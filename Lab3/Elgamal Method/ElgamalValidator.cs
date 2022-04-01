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
			if (DataResult.Key.P <= 1 || DataResult.Key.X <= 1 || DataResult.Key.K <= 1)
				throw new ArgumentException("Key: less/eq than 1");

			if (DataResult.Key.X >= DataResult.Key.P - 1)
				throw new ArgumentException("Key: X more than P-1");

			if (DataResult.Key.K >= DataResult.Key.P - 1)
				throw new ArgumentException("Key: K more than P-1");
			if (!IsCoprime(DataResult.Key.K, DataResult.Key.P - 1))
				throw new ArgumentException("Key: K is not coprime P");

			if (!DataResult.Key.G.HasValue)
				DataResult.Key.G = GetOpenKey(DataResult.Key.P).First();

			DataResult.Key.Y = (int)BigInteger.ModPow(DataResult.Key.G.Value, DataResult.Key.X, DataResult.Key.P);
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
			var primeDiv = GetPrimeDiv(pKey - 1);

			var result = new List<int>();
			for (int i = 1; i <= pKey - 1; i++)
			{
				bool IsDiv = false;
				foreach (var numb in primeDiv)
				{
					var divRes = BigInteger.ModPow(i, (pKey - 1) / numb, pKey);
					if (divRes == 1)
					{
						IsDiv = true;
						break;
					}
				}
				if (!IsDiv)
					result.Add(i);
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
	}
}
