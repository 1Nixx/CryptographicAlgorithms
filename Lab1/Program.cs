

using Core.Models;
using Lab1.Column_Method;
using System.Text;

class Program
{
	private static void Main()
	{
		var builder = new StringBuilder();
		for (char i = 'а'; i <= 'я'; i++)
			builder.Append(i);

		string key = "криптография";
		string source = "ааоооякооэнибблритаортаатпрк";

		var info = new CryptingInfo<string>(builder.ToString(), key, source);
		var crypter = new ColumnCrypter(info);

		Console.WriteLine(crypter.DecryptData());
	}
}