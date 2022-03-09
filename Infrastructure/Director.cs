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
using Lab2.LFSR_Method;

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
			var cryptInfo = new CryptingInfo<string, string>(alphabet, key, sourceData);
			var cryptographer = new Cryptographer<string, string>(cryptInfo);

			_builder.CreateCryptographer(cryptographer);
			
			_builder.BuildKeyFilter(new DefaultKeyStringFilter() { Info = cryptInfo });
			_builder.BuildSourceFilter(new DefaultSourceStringFilter<string>() { Info = cryptInfo });
			_builder.BuildValidator(new DefaultValidator() { Info = cryptInfo });

			_builder.BuildСrypter(new ColumnCrypter(cryptInfo));
		}

		public void ConstructVigenereMethod(string alphabet, string key, string sourceData)
		{
			var cryptInfo = new CryptingInfo<string, string>(alphabet, key, sourceData);
			var cryptographer = new Cryptographer<string, string>(cryptInfo);

			_builder.CreateCryptographer(cryptographer);

			_builder.BuildKeyFilter(new DefaultKeyStringFilter() { Info = cryptInfo });
			_builder.BuildSourceFilter(new DefaultSourceStringFilter<string>() { Info = cryptInfo });
			_builder.BuildValidator(new VigenereValidator() { Info = cryptInfo});

			_builder.BuildСrypter(new VigenereCrypter(cryptInfo));
		}

		public void ConstructDecimationMethod(string alphabet, int key, string sourceData)
		{
			var cryptInfo = new CryptingInfo<string, int>(alphabet, key, sourceData);
			var cryptographer = new Cryptographer<string, int>(cryptInfo);

			_builder.CreateCryptographer(cryptographer);

			_builder.BuildSourceFilter(new DefaultSourceStringFilter<int>() { Info = cryptInfo });
			_builder.BuildValidator(new DecimationValidator() { Info = cryptInfo });

			_builder.BuildСrypter(new DecimationCrypter(cryptInfo));
		}

		public void ConstructLFSRCrypt(string registerSource, string polynomial, byte[] sourceData)
		{
			var sourceKey = new LFSRSourceKey() { Polynomial = polynomial, ShiftSource = registerSource };
			var sourceInfo = new CryptingInfo<byte[], LFSRSourceKey>("01", sourceKey, sourceData);
			var resultKey = new LFSRKey() { Polynomial = null, ShiftSource = 0, RegisterLength = 0 };
			var resultInfo = new CryptingInfo<byte[], LFSRKey>(null, resultKey, sourceData);

			var cryptographer = new Cryptographer<byte[], LFSRKey>(resultInfo);

			_builder.CreateCryptographer(cryptographer);

			_builder.BuildSourceFilter(new LFSRSourceFilter() { DataResult = resultInfo, SourceData = sourceInfo });
			_builder.BuildKeyFilter(new LFSRKeyFilter() { KeyResult = resultInfo, SourceData = sourceInfo });
			_builder.BuildValidator(new LFSRValidator() { KeyResult = resultInfo, SourceData = sourceInfo });

			_builder.BuildСrypter(new LFSRCrypter(resultInfo));
		}
	}
}
