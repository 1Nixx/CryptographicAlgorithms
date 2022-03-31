using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class CryptingInfo<TSource, TKey>
	{
		public CryptingInfo(string alphabet, TKey key, TSource source)
		{
			Alphabet = alphabet;
			Key = key;
			Source = source;
		}

		public CryptingInfo(TKey key, TSource source)
		{
			Key = key;
			Source = source;
		}

		public string? Alphabet { get; set; }

		public TKey Key { get; set; }

		public TSource Source { get; set; }
	}
}
