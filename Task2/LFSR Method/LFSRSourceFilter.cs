using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.LFSR_Method
{
	public class LFSRSourceFilter : IFilter
	{
		public CryptingInfo<byte[], LFSRSourceKey> SourceData { get; set; }
		public CryptingInfo<byte[], LFSRKey> DataResult { get; set; }

		public void Filter()
		{
			if (DataResult.Source.Length == 0)
				throw new ArgumentException("Source data error");
		}
	}
}
