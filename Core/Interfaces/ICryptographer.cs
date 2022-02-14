using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface ICryptographer
	{
		IInputFilter? InputFilter { get; set; }
		IValidator? Validator { get; set; }
		ICrypter? Crypter { get; set; }
		string StartCrypting();
	}
}
