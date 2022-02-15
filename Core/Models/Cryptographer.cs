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
		public IFilter? KeyFilter { get; set; }

		public IFilter? SourceFilter { get; set; }

		public IValidator? Validator { get; set; }

		public ICrypter? Crypter { get; set; }

		private CryptingInfo<TKey> CryptingInfo { get; set; }

		public Cryptographer(string alphabet, TKey key, string source)
		{
			CryptingInfo = new CryptingInfo<TKey>(alphabet, key, source);
		}

		public Cryptographer(CryptingInfo<TKey> info)
		{
			CryptingInfo = info;
		}

		public bool PrepareData()
		{
			try
			{
				if (KeyFilter is not null)
					KeyFilter.Filter();
				if (SourceFilter is not null)
					SourceFilter.Filter();
				if (Validator is not null)
					Validator.Validate();
			}
			catch (ArgumentException)
			{
				return false;
			}
			catch (NullReferenceException)
			{
				return false;
			}
			return true;
		}

		public string Crypting()
		{
			return Crypter.CryptData();
		}

		public string Decrypting()
		{
			return Crypter.DecryptData();
		}
	}
}
