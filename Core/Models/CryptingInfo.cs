using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class CryptingInfo<TKey>
	{
		public CryptingInfo(string alphabet, TKey key, string source)
		{
			Alphabet = alphabet;
			Key = key;
			Source = source;
		}

		public string Alphabet { get; set; }

		public TKey Key { get; set; }

		public string Source { get; set; }
	}
}
