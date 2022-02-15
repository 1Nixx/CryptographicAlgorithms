using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Default
{
	public class DefaultSourceFilter<T> : IFilter
	{
		public CryptingInfo<T> Info { get; set; }

		public void Filter()
		{
			if (Info is null)
				throw new NullReferenceException();

			Info.Source = Info.Source.ToUpper();
			Info.Alphabet = Info.Alphabet.ToUpper();

			var builder = new StringBuilder();

			for (int i = 0; i < Info.Source.Length; i++)
				if (Info.Alphabet.Contains(Info.Source[i]))
					builder.Append(Info.Source[i]);

			Info.Source = builder.ToString();
		}
	}
}
