using Core.Models;
using Lab2.LFSR_Method;
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
		var str = File.ReadAllBytes(@"C:\Универ\4 Семестр\ТИ\Лабы\TILabs\asd");

		var list = ShiftRegister.GetXORElements("x^28+x^3+1");
		var key = new LFSRKey() { RegisterLength = 28, ShiftSource = Convert.ToInt64(new String('1', 20), 2), Polynomial = list };
		var info = new CryptingInfo<byte[], LFSRKey>("01", key, str);
		var crypter = new LFSRCrypter(info);
		var result = crypter.DecryptData();

		File.WriteAllBytes(@"C:\Универ\4 Семестр\ТИ\Лабы\TILabs\res", (byte[])result);
	}
}