

using Core.Models;
using Lab4.EDS;
using System.Numerics;

public class Program
{ 
	public static void Main()
	{
		var str = "   ";
		var key = new EDSKey()
		{
			P = BigInteger.Parse("41"),
			Q = BigInteger.Parse("59"),
			D = BigInteger.Parse("133"),
			E = BigInteger.Parse("157"),
			EDS = BigInteger.Parse("2306")
		};
		var info = new CryptingInfo<string, EDSKey>(key, str);
		var crypter = new EDSCrypter(info, HashCalculator.GetInstance());
		crypter.DecryptData();
	}
}