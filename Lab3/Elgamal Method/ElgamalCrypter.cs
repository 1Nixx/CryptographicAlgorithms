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
	public class ElgamalCrypter : ICrypter
	{
		private readonly CryptingInfo<byte[], ElgamalKey> _cryptingInfo;

		public ElgamalCrypter(CryptingInfo<byte[], ElgamalKey> crypterInfo)
		{
			_cryptingInfo = crypterInfo;
		}

		public object CryptData()
		{
			var itemSize = (new BigInteger(_cryptingInfo.Key.P)).GetByteCount(true);

			byte[] cryptedData = new byte[_cryptingInfo.Source.Length*itemSize*2];
			for (int i = 0; i < _cryptingInfo.Source.Length; i++)
			{
				var a = BigInteger.ModPow(_cryptingInfo.Key.G, _cryptingInfo.Key.K, _cryptingInfo.Key.P).ToByteArray(isUnsigned: true);
				var b = (BigInteger.Pow(_cryptingInfo.Key.Y, _cryptingInfo.Key.K) * _cryptingInfo.Source[i] % _cryptingInfo.Key.P).ToByteArray(isUnsigned: true);
				
				var aNumb = ConvertToStoreSize(a, itemSize);
				var bNumb = ConvertToStoreSize(b, itemSize);

				aNumb.CopyTo(cryptedData, i * itemSize * 2);
				bNumb.CopyTo(cryptedData, i * itemSize * 2 + itemSize);
			}

			return cryptedData;
		}

		public object DecryptData()
		{
			var itemSize = (new BigInteger(_cryptingInfo.Key.P)).GetByteCount(true);

			byte[] decryptedData = new byte[_cryptingInfo.Source.Length/(2 * itemSize)];
			for (int i = 0; i < _cryptingInfo.Source.Length / (2 * itemSize); i++) 
			{
				byte[] a = _cryptingInfo.Source.Skip(i * 2 * itemSize).Take(itemSize).ToArray();		
				byte[] b = _cryptingInfo.Source.Skip(i * 2 * itemSize + itemSize).Take(itemSize).ToArray();
				byte m = (byte)(new BigInteger(b, isUnsigned: true) * BigInteger.Pow(new BigInteger(a, isUnsigned: true), _cryptingInfo.Key.P - 1 - _cryptingInfo.Key.X) % _cryptingInfo.Key.P);
				decryptedData[i] = m;
			}
			return decryptedData;
		}

		private static byte[] ConvertToStoreSize(byte[] sourceNumber, int destSize)
		{
			if (sourceNumber.Length == destSize)
				return sourceNumber;

			byte[] result = new byte[destSize];	
			Array.Copy(sourceNumber, result, sourceNumber.Length);

			return result;
		}
	}
}
