using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Decimation_Method
{
	public class DecimationValidator : IValidator
	{
		public CryptingInfo<string, int> Info { get; set; }
		public void Validate()
		{
			if (Info is null)
				throw new NullReferenceException();

			if (Info.Source == "")
				throw new ArgumentException();

			if (Info.Key <= 0 || !IsCoprime(Info.Key, Info.Alphabet.Length))
				throw new ArgumentException();
		}

		private bool IsCoprime(int a, int b)
		{
			return a == b
				   ? a == 1
				   : a > b
						? IsCoprime(a - b, b)
						: IsCoprime(b - a, a);
		}
	}
}
