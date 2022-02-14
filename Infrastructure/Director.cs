using Core.Models;
using Lab1.Column_Method;
using Lab1.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure
{
	public class Director
	{
		private Builder _builder;

		public Director(Builder builder)
		{
			_builder = builder;
		}

		public void ConstructColumnMethod(string alphabet, string key, string sourceData)
		{
			var cryptInfo = new CryptingInfo<string>(alphabet, key, sourceData);
			var cryptographer = new Cryptographer<string>(cryptInfo);

			_builder.CreateCryptographer(cryptographer);
			_builder.BuildInputFilter(new DefaultInputFilter());
			_builder.BuildValidator(new ColumnValidator());
			_builder.BuildСrypter(new ColumnCrypter(cryptInfo));
		}
	}
}
