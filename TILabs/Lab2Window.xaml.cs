using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TILabs
{
	/// <summary>
	/// Interaction logic for Lab2Window.xaml
	/// </summary>
	public partial class Lab2Window : Window
	{
		private Window _ownerWindow { get; }
		private Builder _builder { get; }
		private Director _director { get; }

		public Lab2Window(Window ownerWindow)
		{
			_builder = new ConcreteBuilder();
			_director = new Director(_builder);
			InitializeComponent();
		}
	}
}
