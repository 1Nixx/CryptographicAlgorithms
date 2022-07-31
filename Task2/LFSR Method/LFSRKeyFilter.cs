using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.LFSR_Method
{
	public class LFSRKeyFilter : IFilter
	{
		public CryptingInfo<byte[], LFSRSourceKey> SourceData { get; set; }
		public CryptingInfo<byte[], LFSRKey> KeyResult { get; set; }

		public void Filter()
		{
			var XOREl = ShiftRegister.GetXORElements(SourceData.Key.Polynomial);
			if (XOREl is null)
				throw new ArgumentException("Polynomial error");
			KeyResult.Key.Polynomial = XOREl;
			KeyResult.Key.RegisterLength = XOREl[0];

			if (SourceData.Key.ShiftSource.Length == 0)
				throw new ArgumentException("Shift source error");

			SourceData.Key.ShiftSource.Trim();
			for (int i = 0; i < SourceData.Key.ShiftSource.Length; i++)
				if (!SourceData.Alphabet.Contains(SourceData.Key.ShiftSource[i]))
					throw new ArgumentException($"Shift source error. Pos {i+1}");

			KeyResult.Key.ShiftSource = Convert.ToInt64(SourceData.Key.ShiftSource, 2);
		}
	}
}
