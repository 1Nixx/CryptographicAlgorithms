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
	public class EDSCrypter : ICrypter
	{
		private readonly CryptingInfo<string, EDSKey> _cryptingInfo;
		private readonly HashCalculator _hashCalculator;

		public EDSCrypter(CryptingInfo<string, EDSKey> crypterInfo, HashCalculator hashCalculator)
		{
			_cryptingInfo = crypterInfo;
			_hashCalculator = hashCalculator;
		}
		public object CryptData()
		{
			BigInteger r = _cryptingInfo.Key.Q * _cryptingInfo.Key.P;
			var hash = _hashCalculator.CalculateHashCode(_cryptingInfo.Source, r);
			BigInteger s = BigInteger.ModPow(hash, _cryptingInfo.Key.D, r);
			return s;
		}

		public object DecryptData()
		{
			BigInteger r = _cryptingInfo.Key.Q * _cryptingInfo.Key.P;
			var hash = _hashCalculator.CalculateHashCode(_cryptingInfo.Source, r);

			BigInteger s = BigInteger.ModPow(_cryptingInfo.Key.EDS!.Value, _cryptingInfo.Key.E, r);
			bool isValid = false;
			if (s == hash)
				isValid = true;
			return isValid;
		}
	}
}
