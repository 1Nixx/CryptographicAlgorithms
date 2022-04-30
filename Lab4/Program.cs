

using Core.Models;
using Lab4.EDS;
using System.Numerics;

public class Program
{ 
	public static void Main()
	{
		var str = "BSUIR";
		var key = new EDSKey()
		{
			P = BigInteger.Parse("1125899839733759 "),
			Q = BigInteger.Parse("18014398241046527"),
			D = BigInteger.Parse("4398042316799"),
			E = BigInteger.Parse("157"),
			EDS = BigInteger.Parse("1451")
		};
		var info = new CryptingInfo<string, EDSKey>(key, str);
		var crypter = new EDSCrypter(info, HashCalculator.GetInstance());
		crypter.CryptData();
	}
}