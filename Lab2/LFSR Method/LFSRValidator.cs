using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.LFSR_Method
{
	public class LFSRValidator : IValidator
	{
		public CryptingInfo<byte[], LFSRSourceKey> SourceData { get; set; }
		public CryptingInfo<byte[], LFSRKey> KeyResult { get; set; }

		public void Validate()
		{
			if (KeyResult.Key.RegisterLength != SourceData.Key.ShiftSource.Length)
				throw new ArgumentException("Incorrect length of register");
		}
	}
}
