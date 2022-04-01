using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Cryptographer<TSource, TKey>: ICryptographer
	{
		public IFilter? KeyFilter { get; set; }

		public IFilter? SourceFilter { get; set; }

		public IValidator? Validator { get; set; }

		public ICrypter? Crypter { get; set; }

		public CryptingInfo<TSource, TKey> CryptingInfo { get; set; }

		public Cryptographer(string alphabet, TKey key, TSource source)
		{
			CryptingInfo = new CryptingInfo<TSource, TKey>(alphabet, key, source);
		}

		public Cryptographer(CryptingInfo<TSource, TKey> info)
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
			catch (ArgumentException ex)
			{
				throw;
			}
			catch (NullReferenceException)
			{
				return false;
			}
			return true;
		}

		public object Crypting()
		{
			return Crypter.CryptData();
		}

		public object Decrypting()
		{
			return Crypter.DecryptData();
		}
	}
}
