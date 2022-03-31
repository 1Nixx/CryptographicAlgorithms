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
	internal class ElgamalCrypter : ICrypter
	{
		private readonly CryptingInfo<byte[], ElgamalKey> _cryptingInfo;

		public ElgamalCrypter(CryptingInfo<byte[], ElgamalKey> crypterInfo)
		{
			_cryptingInfo = crypterInfo;
		}

		public object CryptData()
		{
			byte[] cryptedData = new byte[_cryptingInfo.Source.Length*2];
			for (int i = 0; i < _cryptingInfo.Source.Length; i++)
			{
				byte a = (byte)BigInteger.ModPow(_cryptingInfo.Key.G, _cryptingInfo.Key.K, _cryptingInfo.Key.P);
				byte b = (byte)(BigInteger.Pow(_cryptingInfo.Key.Y, _cryptingInfo.Key.K) * _cryptingInfo.Source[i] % _cryptingInfo.Key.P);

				cryptedData[i * 2] = a;
				cryptedData[i * 2 + 1] = b;
			}

			return cryptedData;
		}

		public object DecryptData()
		{
			byte[] decryptedData = new byte[_cryptingInfo.Source.Length/2];
			for (int i = 0; i < _cryptingInfo.Source.Length/2; i++)
			{
				byte a = _cryptingInfo.Source[i * 2];
				byte b = _cryptingInfo.Source[i * 2 + 1];

				byte m = (byte)(b * BigInteger.Pow(a, _cryptingInfo.Key.P - 1 - _cryptingInfo.Key.X) % _cryptingInfo.Key.P);
				decryptedData[i] = m;
			}
			return decryptedData;
		}
	}
}
