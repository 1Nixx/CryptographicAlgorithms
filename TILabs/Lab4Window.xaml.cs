using Core.Interfaces;
using Infrastructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
	/// Interaction logic for Lab4Window.xaml
	/// </summary>
	public partial class Lab4Window : Window
	{
		private Window _ownerWindow { get; }
		private Builder _builder { get; }
		private Director _director { get; }

		private ICryptographer _cryptographer { get; set; }

		private string _inputText { get; set; }

		public Lab4Window(Window ownerWindow)
		{
			_ownerWindow = ownerWindow;
			_builder = new ConcreteBuilder();
			_director = new Director(_builder);
			InitializeComponent();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			_ownerWindow.Show();
			this.Hide();
		}

		private void btnLoadFromFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				var FilePath = openFileDialog.FileName;

				using var reader = new StreamReader(FilePath);
				var text = reader.ReadToEnd();
				_inputText = text;
				tbSource.Text = text;
			}
		}

		private void btnSaveToFile_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if ((_inputText != null) && (saveFileDialog.ShowDialog()) == true)
			{
				using var writer = new StreamWriter(saveFileDialog.FileName);
				writer.Write(_inputText);
			}
		}

		private void btnCrypt_Click(object sender, RoutedEventArgs e)
		{
			lbError.Visibility = Visibility.Hidden;
			lbError.Foreground = new SolidColorBrush(Colors.Red);
			if (_inputText is null || tbPkey.Text == "" || tbQKey.Text == "" || tbDKey.Text == "")
			{
				lbError.Content = "Ошибка в параметрах";
				lbError.Visibility = Visibility.Visible;
				return;
			}

			try
			{
				_director.ConstructEDSMethod(tbPkey.Text, tbQKey.Text, tbDKey.Text, null, _inputText);
			}
			catch (Exception ex)
			{
				lbError.Content = ex.Message;
				lbError.Visibility = Visibility.Visible;
				return;
			}

			var cryptographer = _builder.GetCryptographer();
			try
			{
				bool dataRes = cryptographer.PrepareData();
				if (!dataRes)
				{
					lbError.Content = "Ошибка в параметрах";
					lbError.Visibility = Visibility.Visible;
					return;
				}
			}
			catch (ArgumentException ex)
			{
				lbError.Content = ex.Message;
				lbError.Visibility = Visibility.Visible;
				return;
			}


			var result = ((BigInteger hash, BigInteger EDS, BigInteger openKey))cryptographer.Crypting();

			tbEDS.Text = result.EDS.ToString();
			tbHashText.Text = result.hash.ToString();
			tbOpenKey.Text = result.openKey.ToString();
			_inputText = GetHashedString(_inputText, result.EDS);
			tbSource.Text = _inputText;
		}

		private void btnDecrypt_Click(object sender, RoutedEventArgs e)
		{
			lbError.Visibility = Visibility.Hidden;
			lbError.Foreground = new SolidColorBrush(Colors.Red);
			if (_inputText is null || tbPkey.Text == "" || tbQKey.Text == "" || tbDKey.Text == "")
			{
				lbError.Content = "Ошибка в параметрах";
				lbError.Visibility = Visibility.Visible;
				return;
			}
			string edsText;
			try
			{
				var data = DevideHashedString(_inputText);
				edsText = data.hash;
				_director.ConstructEDSMethod(tbPkey.Text, tbQKey.Text, tbDKey.Text, data.hash, data.text);
			}
			catch (Exception ex)
			{
				lbError.Content = ex.Message;
				lbError.Visibility = Visibility.Visible;
				return;
			}

			var cryptographer = _builder.GetCryptographer();
			try
			{
				bool dataRes = cryptographer.PrepareData();
				if (!dataRes)
				{
					lbError.Content = "Ошибка в параметрах";
					lbError.Visibility = Visibility.Visible;
					return;
				}
			}
			catch (ArgumentException ex)
			{
				lbError.Content = ex.Message;
				lbError.Visibility = Visibility.Visible;
				return;
			}


			var result = ((BigInteger hash, bool EDSValid, BigInteger openKey))cryptographer.Decrypting();

			tbEDS.Text = edsText;
			tbHashText.Text = result.hash.ToString();
			tbOpenKey.Text = result.openKey.ToString();

			lbError.Visibility = Visibility.Visible;
			lbError.Foreground = new SolidColorBrush(Colors.Black);
			var message = result.EDSValid ? "Valid" : "Not valid";
			lbError.Content = $"EDS - {message}";
		}

		private string GetHashedString(string str, BigInteger hash)
		{
			var hashStr = hash.ToString();
			var result = str + ", " + hashStr;
			return result;
		}

		private (string hash, string text) DevideHashedString(string str)
		{
			var pos = str.LastIndexOf(',');
			if (pos == -1)
				throw new ArgumentException("Source: no EDS");

			var textStr = str.Substring(0, pos);
			var hashStr = str.Substring(pos+2);

			return (hashStr, textStr);
		}

	}
}
