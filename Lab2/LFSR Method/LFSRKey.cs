using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.LFSR_Method
{
	public class LFSRKey
	{
		public int RegisterLength { get; set; }
		public long ShiftSource { get; set; }
		public List<int> Polynomial { get; set; }
	}
}
