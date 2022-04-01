using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Elgamal_Method
{
	public class ElgamalKeyFilter : IFilter
	{
		public CryptingInfo<byte[], ElgamalSourceKey> SourceData { get; set; }
		public CryptingInfo<byte[], ElgamalKey> DataResult { get; set; }

		public void Filter()
		{
			try
			{
				DataResult.Key.P = int.Parse(SourceData.Key.P);
				DataResult.Key.X = int.Parse(SourceData.Key.X);
				DataResult.Key.K = int.Parse(SourceData.Key.K);
			}
			catch (FormatException)
			{
				throw new ArgumentException("Key: not a number");
			}
			catch (OverflowException)
			{
				throw new ArgumentException("Key: big number");
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentException("Key: empty key");
			}
			
		}
	}
}
