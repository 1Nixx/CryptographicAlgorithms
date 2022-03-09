using Core.Interfaces;
using Core.Models;
using Lab2.LFSR_Method;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab2.LFSR_Method
{
	public class LFSRCrypter : ICrypter
	{
		private readonly CryptingInfo<byte[], LFSRKey> _cryptingInfo;
		private ShiftRegister _shiftRegister;


		public LFSRCrypter(CryptingInfo<byte[], LFSRKey> crypterInfo)
		{
			_cryptingInfo = crypterInfo;
			_shiftRegister = new(crypterInfo.Key);
		}

		public object CryptData()
		{
			return StartCrypting();
		}

		public object DecryptData()
		{
			return StartCrypting();
		}

		private byte[] StartCrypting()
		{
			var keyBits = _shiftRegister.CalculateFinalKey(_cryptingInfo.Source.Length);
			var keyByte = keyBits.ToBytes();
			var sourceByte = (byte[])_cryptingInfo.Source.Clone();

			for (int i = 0; i < sourceByte.Length; i++)
				sourceByte[i] ^= keyByte[i];
			
			return sourceByte;
		}
	}

	public class ShiftRegister
	{
		private readonly long source;
		private readonly List<int> XORElement;
		private readonly int registerLength;

		public ShiftRegister(LFSRKey key)
		{
			source = key.ShiftSource;
			XORElement = key.Polynomial;
			registerLength = key.RegisterLength;
		}

		public BitArray CalculateFinalKey(long bytesAmount)
		{
			long stateMask = Convert.ToInt64(new String('1', registerLength), 2);
			long registerState = source;
			long amount = 0;
			long bitsAMount = bytesAmount * 8;
			BitArray outputResult = new(0);
			do
			{
				outputResult.Length++;
				outputResult[outputResult.Count-1] = Convert.ToBoolean((registerState >> registerLength - 1) & 1);
				
				byte newBit = GetNewBit(registerState, XORElement);
				registerState = (registerState << 1) & stateMask;
				registerState += newBit;
				amount++;
			} while (amount != bitsAMount-1);
			outputResult.Length++;
			outputResult[outputResult.Count - 1] = Convert.ToBoolean((registerState >> registerLength - 1) & 1);

			return outputResult;
		}

		private byte GetNewBit(long registerState, List<int> XORElement)
		{
			byte newBit = Convert.ToByte((registerState >> XORElement.FirstOrDefault() - 1) & 1);
			for (int i = 1; i < XORElement.Count; i++)
			{
				newBit ^= Convert.ToByte((registerState >> XORElement[i] - 1) & 1);
			}
			return newBit;
		}

		public static List<int> GetXORElements(string polynomial)
		{
			string polynomPattern = "(?<=\\^)\\d+(?=\\+)";

			List<int> result = new();
			foreach (Match match in Regex.Matches(polynomial, polynomPattern))
				result.Add(Convert.ToInt32(match.Value));
			result.Sort();
			result.Reverse();
			return result;
		}
	}

	internal static class Extencions
	{
		public static byte[] ToBytes(this BitArray bitarray)
		{
			if (bitarray.Length == 0)
			{
				throw new System.ArgumentException("must have at least length 1", "bitarray");
			}

			int num_bytes = bitarray.Length / 8;

			if (bitarray.Length % 8 != 0)
			{
				num_bytes += 1;
			}

			var bytes = new byte[num_bytes];
			bitarray.CopyTo(bytes, 0);
			return bytes;
		}
	}
}
