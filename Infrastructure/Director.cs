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
using Lab3.Elgamal_Method;
using Lab4.EDS;

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

		public void ConstructLFSRMethod(string registerSource, string polynomial, byte[] sourceData)
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

		public void ConstructElgamalMethod(string pKey, string xKey, string kKey, byte[] sourceData)
		{
			var sourceKey = new ElgamalSourceKey() { K = kKey, P = pKey, X = xKey };
			var sourceInfo = new CryptingInfo<byte[], ElgamalSourceKey>(sourceKey, sourceData);
			var resultKey = new ElgamalKey();
			var resultInfo = new CryptingInfo<byte[], ElgamalKey>(resultKey, sourceData);

			var cryptographer = new Cryptographer<byte[], ElgamalKey>(resultInfo);

			_builder.CreateCryptographer(cryptographer);

			_builder.BuildSourceFilter(new ElgamalSourceFilter() { DataResult = resultInfo, SourceData = sourceInfo });
			_builder.BuildKeyFilter(new ElgamalKeyFilter() { DataResult = resultInfo, SourceData = sourceInfo });
			_builder.BuildValidator(new ElgamalValidator() { DataResult = resultInfo, SourceData = sourceInfo});

			_builder.BuildСrypter(new ElgamalCrypter(resultInfo));
		}
	
		public void ConstructEDSMethod(string pKey, string qKey, string dKey, string? EDS, string sourceText)
		{
			var sourceKey = new EDSSourceKey() { P = pKey, Q = qKey, D = dKey, EDS = EDS };
			var sourceInfo = new CryptingInfo<string, EDSSourceKey>(sourceKey, sourceText);
			var resultKey = new EDSKey();
			var resultInfo = new CryptingInfo<string, EDSKey>(resultKey, sourceText);

			var cryptographer = new Cryptographer<string, EDSKey>(resultInfo);

			_builder.CreateCryptographer(cryptographer);

			_builder.BuildSourceFilter(new EDSSourceFilter() { DataResult = resultInfo, SourceData = sourceInfo });
			_builder.BuildKeyFilter(new EDSKeyFilter() { DataResult = resultInfo, SourceData = sourceInfo });
			_builder.BuildValidator(new EDSValidator() { DataResult = resultInfo, SourceData = sourceInfo });

			_builder.BuildСrypter(new EDSCrypter(resultInfo, HashCalculator.GetInstance()));
		}
	}
}
