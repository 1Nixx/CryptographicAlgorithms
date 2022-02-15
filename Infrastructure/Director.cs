using Core.Models;
using Lab1.Column_Method;
using Lab1.Decimation_Method;
using Lab1.Vigenere_Method;
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
			
			_builder.BuildKeyFilter(new DefaultKeyStringFilter() { Info = cryptInfo });
			_builder.BuildSourceFilter(new DefaultSourceFilter<string>() { Info = cryptInfo });
			_builder.BuildValidator(new DefaultValidator() { Info = cryptInfo });

			_builder.BuildСrypter(new ColumnCrypter(cryptInfo));
		}

		public void ConstructVigenereMethod(string alphabet, string key, string sourceData)
		{
			var cryptInfo = new CryptingInfo<string>(alphabet, key, sourceData);
			var cryptographer = new Cryptographer<string>(cryptInfo);

			_builder.CreateCryptographer(cryptographer);

			_builder.BuildKeyFilter(new DefaultKeyStringFilter() { Info = cryptInfo });
			_builder.BuildSourceFilter(new DefaultSourceFilter<string>() { Info = cryptInfo });
			_builder.BuildValidator(new VigenereValidator() { Info = cryptInfo});

			_builder.BuildСrypter(new VigenereCrypter(cryptInfo));
		}

		public void ConstructDecimationMethod(string alphabet, int key, string sourceData)
		{
			var cryptInfo = new CryptingInfo<int>(alphabet, key, sourceData);
			var cryptographer = new Cryptographer<int>(cryptInfo);

			_builder.CreateCryptographer(cryptographer);

			_builder.BuildSourceFilter(new DefaultSourceFilter<int>() { Info = cryptInfo });
			_builder.BuildValidator(new DecimationValidator() { Info = cryptInfo });

			_builder.BuildСrypter(new DecimationCrypter(cryptInfo));
		}
	}
}
