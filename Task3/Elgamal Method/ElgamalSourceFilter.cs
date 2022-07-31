using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Elgamal_Method
{
	public class ElgamalSourceFilter : IFilter
	{
		public CryptingInfo<byte[], ElgamalSourceKey> SourceData { get; set; }
		public CryptingInfo<byte[], ElgamalKey> DataResult { get; set; }
		public void Filter()
		{
			if (SourceData.Source.Length == 0)
				throw new ArgumentException("Source data error");
		}
	}
}
