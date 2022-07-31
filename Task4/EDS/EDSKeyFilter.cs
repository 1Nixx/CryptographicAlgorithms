using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.EDS
{
	public class EDSKeyFilter : IFilter
	{
		public CryptingInfo<string, EDSSourceKey> SourceData { get; set; }
		public CryptingInfo<string, EDSKey> DataResult { get; set; }
		public void Filter()
		{
			try
			{
				DataResult.Key.P = BigInteger.Parse(SourceData.Key.P);
				DataResult.Key.Q = BigInteger.Parse(SourceData.Key.Q);
				DataResult.Key.D = BigInteger.Parse(SourceData.Key.D);
				if (SourceData.Key.EDS is not null)
				{
					DataResult.Key.EDS = BigInteger.Parse(SourceData.Key.EDS);
				}
			}
			catch (FormatException)
			{
				throw new ArgumentException("Key: not a number");
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentException("Key: empty key");
			}

		}
	}
}
