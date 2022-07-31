using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Column_Method
{
	public class ColumnCrypter: ICrypter
	{
		private readonly CryptingInfo<string, string> _cryptingInfo;

		public ColumnCrypter(CryptingInfo<string, string> cryptingInfo)
		{
			_cryptingInfo = cryptingInfo;
		}

		public object CryptData()
		{
			var sortedKey = SortKey(_cryptingInfo.Key, _cryptingInfo.Alphabet);

			var cryptedString = new StringBuilder(_cryptingInfo.Source.Length);
			char prevChar = sortedKey[0];
			int prevIndex = 0;
			for (int i = 0; i < sortedKey.Length; i++)
			{
				if (prevChar != sortedKey[i])
					prevIndex = 0;

				int columnIndex = _cryptingInfo.Key.IndexOf(sortedKey[i], prevIndex);

				prevChar = sortedKey[i];
				prevIndex = columnIndex + 1;

				for (int j = columnIndex; j < _cryptingInfo.Source.Length; j += _cryptingInfo.Key.Length)
					cryptedString.Append(_cryptingInfo.Source[j]);
			}

			return cryptedString.ToString();
		}

		public object DecryptData()
		{
			int arrHigh = (int)Math.Ceiling((double)_cryptingInfo.Source.Length / _cryptingInfo.Key.Length);
			char[,] letters = new char[arrHigh, _cryptingInfo.Key.Length];

			var sortedKey = SortKey(_cryptingInfo.Key, _cryptingInfo.Alphabet);
			char prevChar = sortedKey[0];
			int prevIndex = 0;
			int stringReader = 0;

			int additionalChars = _cryptingInfo.Source.Length % _cryptingInfo.Key.Length;
			int itemsInDefaultColumnAmount = _cryptingInfo.Source.Length / _cryptingInfo.Key.Length;

			for (int i = 0; i < sortedKey.Length; i++)
			{
				if (prevChar != sortedKey[i])
					prevIndex = 0;

				int columnIndex = _cryptingInfo.Key.IndexOf(sortedKey[i], prevIndex);

				prevChar = sortedKey[i];
				prevIndex = columnIndex + 1;

				int itemsInColumnAmount = itemsInDefaultColumnAmount;
				if (columnIndex + 1 <= additionalChars)
					itemsInColumnAmount++;

				for (int j = 0; j < itemsInColumnAmount; j++)
					letters[j, columnIndex] = _cryptingInfo.Source[stringReader++];
			}

			var decryptedString = new StringBuilder(_cryptingInfo.Source.Length);

			for (int i = 0; i < letters.GetLength(0); i++)
				for (int j = 0; j < letters.GetLength(1); j++)
					if (_cryptingInfo.Alphabet.Contains(letters[i, j]))
						decryptedString.Append(letters[i, j]);

			return decryptedString.ToString();
		}

		private char[] SortKey(string key, string alphabet)
		{
			var sortedKey = key.ToArray();
			Array.Sort(sortedKey, (x, y) =>
			{
				if (alphabet.IndexOf(x) > alphabet.IndexOf(y))
					return 1;
				else if (alphabet.IndexOf(x) < alphabet.IndexOf(y))
					return -1;
				else
					return 0;
			});
			return sortedKey;
		}
	}
}
