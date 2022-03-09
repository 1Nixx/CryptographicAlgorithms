using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Default
{
	public class DefaultKeyStringFilter: IFilter
	{
		public CryptingInfo<string, string> Info { get; set; }
		public void Filter()
		{

			if (Info == null)
				throw new NullReferenceException();

			Info.Alphabet = Info.Alphabet.ToUpper();
			Info.Key = Info.Key.ToUpper();

			var builder = new StringBuilder();

			for (int i = 0; i < Info.Key.Length; i++)
				if (Info.Alphabet.Contains(Info.Key[i]))
					builder.Append(Info.Key[i]);

			Info.Key = builder.ToString();
		}
	}
}
