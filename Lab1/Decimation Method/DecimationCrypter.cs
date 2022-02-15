using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Decimation_Method
{
	public class DecimationCrypter : ICrypter
	{
		private readonly CryptingInfo<int> _cryptingInfo;

		public DecimationCrypter(CryptingInfo<int> cryptingInfo)
		{
			_cryptingInfo = cryptingInfo;
		}

		public string CryptData()
		{
			var builder = new StringBuilder(_cryptingInfo.Source.Length);

			for (int i = 0; i < _cryptingInfo.Source.Length; i++)
			{
				int letterInd = (_cryptingInfo.Alphabet.IndexOf(_cryptingInfo.Source[i]) * _cryptingInfo.Key) % _cryptingInfo.Alphabet.Length;
				builder.Append(_cryptingInfo.Alphabet[letterInd]);
			}

			return builder.ToString();
		}

		public string DecryptData()
		{
			var builder = new StringBuilder(_cryptingInfo.Source.Length);

			for (int i = 0; i < _cryptingInfo.Source.Length; i++)
			{
				int letterInd = SourceChar(_cryptingInfo.Alphabet.IndexOf(_cryptingInfo.Source[i]));
				builder.Append(_cryptingInfo.Alphabet[letterInd]);
			}
			return builder.ToString();
		}

		private int SourceChar(int charInd)
		{
			for (int i = 0; i < _cryptingInfo.Alphabet.Length; i++)
			{
				int res = (i * _cryptingInfo.Key - charInd) % 26;
				if (Math.Abs(res) == 0) 
					return i;
			}
			return -1;
		}
	}
}
