using Lab1.Default;
using System.Text;

namespace Lab1.Vigenere_Method
{
	public class VigenereValidator : DefaultValidator
	{

		public override void Validate()
		{
			base.Validate();

			var builder = new StringBuilder(Info.Source.Length);
			string prevKey = Info.Key;
			builder.Append(prevKey);

			while (builder.Length < builder.Capacity)
			{
				prevKey = GetShiftedKey(prevKey, Info.Alphabet);
				if (builder.Length + prevKey.Length < builder.Capacity)
					builder.Append(prevKey);
				else
					builder.Append(prevKey.Substring(0, builder.Capacity - builder.Length));
			}

			string resultkey = builder.ToString();
			if (resultkey.Length > Info.Source.Length)
				Info.Key = resultkey.Substring(0, Info.Source.Length);
			else
				Info.Key = resultkey;
		}

		private string GetShiftedKey(string key, string alphabet)
		{
			var builder = new StringBuilder();
			for (int i = 0; i < key.Length; i++)
			{
				int alphPos = alphabet.IndexOf(key[i]);
				int newCharPos = (alphPos + 1) % alphabet.Length;
				builder.Append(alphabet[newCharPos]);
			}		
			return builder.ToString();
		}
	}
}
