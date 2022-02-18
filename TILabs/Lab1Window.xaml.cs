using Infrastructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace TILabs
{
	/// <summary>
	/// Interaction logic for Lab1Window.xaml
	/// </summary>
	public partial class Lab1Window : Window
	{
		private Window _ownerWindow { get; }
		private Builder _builder { get; }
		private Director _director { get; }
		List<string> _list { get; }


		private string alphabetEng { get; }
		private string alphabetRus { get; }

		public Lab1Window(Window ownerWindow)
		{
			_ownerWindow = ownerWindow;
			_builder = new ConcreteBuilder();
			_director = new Director(_builder);
			_list = new List<string> { "Столбцовый(Англ)", "Виженера(Рус)", "Децимации(Англ)" };

			InitializeComponent();

			cbCryptTypes.ItemsSource = _list;
			alphabetEng = GenerateEnglishAlphabet();
			alphabetRus = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			_ownerWindow.Show();
			this.Hide();
		}

		private void btnCrypt_Click(object sender, RoutedEventArgs e)
		{	
			lbError.Visibility = Visibility.Hidden;
			try
			{
				ConstructMethod(tbKey.Text, tbSource.Text);
			}
			catch (ArgumentException ex)
			{
				lbError.Content = ex.Message;
				lbError.Visibility = Visibility.Visible;
				return;
			}

			var cryptographer = _builder.GetCryptographer();

			bool dataRes = cryptographer.PrepareData();
			if (!dataRes)
			{
				lbError.Content = "Ошибка в параметрах";
				lbError.Visibility = Visibility.Visible;
				return;
			}

			string cryptedString = cryptographer.Crypting();
			tbResult.Text = cryptedString;
		}
		private void btnDecrypt_Click(object sender, RoutedEventArgs e)
		{
			lbError.Visibility = Visibility.Hidden;
			try
			{
				ConstructMethod(tbKey.Text, tbSource.Text);
			}
			catch (ArgumentException ex)
			{
				lbError.Content = ex.Message;
				lbError.Visibility = Visibility.Visible;
				return;
			}

			var cryptographer = _builder.GetCryptographer();

			bool dataRes = cryptographer.PrepareData();
			if (!dataRes)
			{
				lbError.Content = "Ошибка в параметрах";
				lbError.Visibility = Visibility.Visible;
				return;
			}

			string cryptedString = cryptographer.Decrypting();
			tbResult.Text = cryptedString;
		}

		private void ConstructMethod(string key, string sourceData)
		{
			switch (cbCryptTypes.SelectedIndex)
			{
				case 0:
					_director.ConstructColumnMethod(alphabetEng, key, sourceData);
					break;
				case 1:
					_director.ConstructVigenereMethod(alphabetRus, key, sourceData);
					break;
				case 2:
					bool res = int.TryParse(key, out int intValue);
					if (res)
						_director.ConstructDecimationMethod(alphabetEng, intValue, sourceData);
					else
						throw new ArgumentException("Key not Integer");
					break;
				default:
					throw new ArgumentException("Choose encryption type");
			}
		}

		private static string GenerateEnglishAlphabet()
		{
			var builder = new StringBuilder();
			for (char i = 'A'; i <= 'Z'; i++)
				builder.Append(i);
			return builder.ToString();
		}

		private void btnLoadFromFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				var FilePath = openFileDialog.FileName;

				using StreamReader sr = new StreamReader(FilePath);
				var text = sr.ReadToEnd();
				tbSource.Text = text;
			}	
		}

		private void btnSaveToFile_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == true)
				File.WriteAllText(saveFileDialog.FileName,tbResult.Text );
		}

		private void cbCryptTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			tbKey.Text = "";
		}
	}
}
