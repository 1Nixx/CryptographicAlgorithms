using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public class ConcreteBuilder : Builder
	{
		private ICryptographer currentBuilder;

		public override void CreateCryptographer(ICryptographer cryptographer)
		{
			currentBuilder = cryptographer;
		}

		public override void BuildInputFilter(IInputFilter inputFilter)
		{
			if (currentBuilder is null)
				throw new ArgumentNullException(nameof(currentBuilder));
			currentBuilder.InputFilter = inputFilter;
		}

		public override void BuildValidator(IValidator validator)
		{
			if (currentBuilder is null)
				throw new InvalidOperationException(nameof(currentBuilder));
			currentBuilder.Validator = validator;
		}

		public override void BuildСrypter(ICrypter crypter)
		{
			if (currentBuilder is null)
				throw new InvalidOperationException(nameof(currentBuilder));
			currentBuilder.Crypter = crypter;
		}


		public override ICryptographer GetCryptographer()
		{
			if (currentBuilder is null)
				throw new InvalidOperationException(nameof(currentBuilder));
			return currentBuilder;
		}

	}
}
