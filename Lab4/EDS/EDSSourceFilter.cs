using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.EDS
{
	public class EDSSourceFilter : IFilter
	{
		public CryptingInfo<byte[], EDSSourceKey> SourceData { get; set; }
		public CryptingInfo<byte[], EDSKey> DataResult { get; set; }
		public void Filter()
		{
			if (SourceData.Source.Length == 0)
				throw new ArgumentException("Source data error");
		}
	}
}
