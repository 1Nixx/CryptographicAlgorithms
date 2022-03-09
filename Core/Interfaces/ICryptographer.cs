using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface ICryptographer
	{
		IFilter? KeyFilter { get; set; }
		IFilter? SourceFilter { get; set; }
		IValidator? Validator { get; set; }
		ICrypter? Crypter { get; set; }

		bool PrepareData();
		object Crypting();
		object Decrypting();
	}
}
