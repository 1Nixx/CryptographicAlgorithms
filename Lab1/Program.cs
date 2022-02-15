

using Core.Interfaces;
using Core.Models;
using Lab1.Column_Method;
using Lab1.Decimation_Method;
using Lab1.Default;
using Lab1.Vigenere_Method;
using System.Text;

class Program
{
	private static void Main()
	{
		/*var builder = new StringBuilder();
		for (char i = 'A'; i <= 'Z'; i++)
			builder.Append(i);


		string key = "XYZ";
		string source = "Cryptography";

		string crypted = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

		var info = new CryptingInfo<string>(builder.ToString(), key, source);
		//var info = new CryptingInfo<int>(builder.ToString(), key, source);
		var filter = new DefaultSourceFilter<string>() { Info = info };
		//var filter2 = new DefaultKeyStringFilter() { Info = info };
		filter.Filter();
		//filter2.Filter();
		//var crypter = new DecimationCrypter(info);
		Console.WriteLine();

		var validator = new VigenereValidator() { Info = info };
		validator.Validate();
		//Console.WriteLine(crypter.DecryptData());*/
	}

}