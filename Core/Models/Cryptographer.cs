using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Cryptographer<TKey>: ICryptographer
	{
		public IInputFilter? InputFilter { get; set; }

		public IValidator? Validator { get; set; }

		public ICrypter? Crypter { get; set; }

		private CryptingInfo<TKey>? CryptingInfo { get; set; }

		public Cryptographer(string alphabet, TKey key, string source)
		{
			CryptingInfo = new CryptingInfo<TKey>(alphabet, key, source);
		}

		public Cryptographer(CryptingInfo<TKey> info)
		{
			CryptingInfo = info;
		}

		public string StartCrypting()
		{
			throw new NotImplementedException();
		}
	}
}
