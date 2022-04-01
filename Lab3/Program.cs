using Core.Models;
using Lab3.Elgamal_Method;
using System.Text;

class Program
{
	private static void Main()
	{

		////FileStream reader = new FileStream(@"C:\Универ\4 Семестр\ТИ\Лабы\TILabs\test.txt", FileMode.Open, FileAccess.Read);
		//var str = File.ReadAllBytes(@"C:\Универ\4 Семестр\ТИ\Лабы\TILabs\.gitignore.zip");
		////reader.Close();

		//var list = ShiftRegister.GetXORElements("x^28+x^3+1");
		//var key = new LFSRKey() { RegisterLength = 28, ShiftSource = Convert.ToInt64(new String('1', 20), 2), Polynomial = list };
		//var info = new CryptingInfo<byte[], LFSRKey>("01", key, str);
		//var crypter = new LFSRCrypter(info);
		//var result = crypter.CryptData();

		//File.WriteAllBytes(@"C:\Универ\4 Семестр\ТИ\Лабы\TILabs\asd", result);
		//var str = File.ReadAllBytes(@"C:\Универ\4 Семестр\ТИ\Лабы\TILabs\asd");

		//var list = ShiftRegister.GetXORElements("x^28+x^3+1");


		//byte[] data = { 66, 83,  85,  73,  82 };
		//var key = new ElgamalKey() { P = 431, X = 429, G = 7, K = 41, Y = 308 };
		//var info = new CryptingInfo<byte[], ElgamalKey>(key, data);
		//var crypter = new ElgamalCrypter(info);
		//var result = (byte[])crypter.CryptData();

		//var newInfo = new CryptingInfo<byte[], ElgamalKey>(key, result);
		//var decrypter = new ElgamalCrypter(newInfo);
		//var asd = (byte[])decrypter.DecryptData();
		//Console.ReadLine();

		while (true)
		{
			int numb = int.Parse(Console.ReadLine());
			
			var res = ElgamalValidator.GetOpenKey(numb);
			for (int i = 0; i < res.Count; i++)
			{
				Console.WriteLine($"{i} = {res[i]}");
			}
		}
		
	}
}