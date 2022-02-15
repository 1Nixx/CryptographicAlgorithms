
using Core.Interfaces;
using Infrastructure;
using System.Text;

ICryptographer cryptographer;
Builder B = new ConcreteBuilder();
Director D = new Director(B);

var builder = new StringBuilder();
for (char i = 'A'; i <= 'Z'; i++)
	builder.Append(i);

var rusAlph = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
D.ConstructDecimationMethod(builder.ToString(), 45, "Hello world");

cryptographer = B.GetCryptographer();

cryptographer.PrepareData();
Console.WriteLine(cryptographer.Crypting());