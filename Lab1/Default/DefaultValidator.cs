using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Default
{
	public class DefaultValidator: IValidator
	{
		public virtual CryptingInfo<string> Info { get; set; }
		public virtual void Validate()
		{
			if (Info is null)
				throw new NullReferenceException();

			if (Info.Key == "" || Info.Source == "")
				throw new ArgumentException();

		}

	}
}
