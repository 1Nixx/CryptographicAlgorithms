using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TILabs
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnTask1_Click(object sender, RoutedEventArgs e)
		{
			var lab1Window = new Lab1Window(this);
			this.Hide();
			lab1Window.Show();
		}

		private void btnTask2_Click(object sender, RoutedEventArgs e)
		{
			var lab2Window = new Lab2Window(this);
			this.Hide();
			lab2Window.Show();
		}

		private void btnTask3_Click(object sender, RoutedEventArgs e)
		{
			var lab3Window = new Lab3Window(this);
			this.Hide();
			lab3Window.Show();
		}
	}
}
