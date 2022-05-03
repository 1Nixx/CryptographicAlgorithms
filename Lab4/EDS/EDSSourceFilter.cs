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
		public CryptingInfo<string, EDSSourceKey> SourceData { get; set; }
		public CryptingInfo<string, EDSKey> DataResult { get; set; }
		public void Filter()
		{
			if (SourceData.Source is null)
				throw new ArgumentException("Source data error");
		}
	}
}
