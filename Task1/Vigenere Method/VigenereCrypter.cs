using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Vigenere_Method
{
	public class VigenereCrypter : ICrypter
	{
		private readonly CryptingInfo<string, string> _cryptingInfo;

		public VigenereCrypter(CryptingInfo<string, string> cryptingInfo)
		{
			_cryptingInfo = cryptingInfo;
		}

		public object CryptData()
		{
			var builder = new StringBuilder(_cryptingInfo.Source.Length);

			for (int i = 0; i < _cryptingInfo.Key.Length; i++)
			{
				int letterInd = (_cryptingInfo.Alphabet.IndexOf(_cryptingInfo.Key[i])  + _cryptingInfo.Alphabet.IndexOf(_cryptingInfo.Source[i])) % (_cryptingInfo.Alphabet.Length);
				builder.Append(_cryptingInfo.Alphabet[letterInd]);
			}

			return builder.ToString();
		}

		public object DecryptData()
		{
			var builder = new StringBuilder(_cryptingInfo.Source.Length);

			for (int i = 0; i < _cryptingInfo.Key.Length; i++)
			{
				int letterInd = Math.Abs((_cryptingInfo.Alphabet.IndexOf(_cryptingInfo.Source[i]) 
										- _cryptingInfo.Alphabet.IndexOf(_cryptingInfo.Key[i]) 
										+ _cryptingInfo.Alphabet.Length)
										% _cryptingInfo.Alphabet.Length);
				builder.Append(_cryptingInfo.Alphabet[letterInd]);
			}

			return builder.ToString();
		}
	}
}
