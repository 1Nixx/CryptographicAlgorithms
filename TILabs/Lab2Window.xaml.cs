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
using Microsoft.Win32;
using System.IO;
using System.Collections;

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

		private byte[] _inputFile { get; set; }
		private byte[] _outputFile { get; set; }

		private string alphabetBinary { get; } = "01";

		public Lab2Window(Window ownerWindow)
		{
			_ownerWindow = ownerWindow;
			_builder = new ConcreteBuilder();
			_director = new Director(_builder);
			InitializeComponent();
		}

		private void btnCrypt_Click(object sender, RoutedEventArgs e)
		{
			lbError.Visibility = Visibility.Hidden;
			if (_inputFile is  null || tbPolynomical.Text == "" || tbStartCond.Text == "")
			{
				lbError.Content = "Ошибка в параметрах";
				lbError.Visibility = Visibility.Visible;
				return;
			}

			try
			{
				_director.ConstructLFSRMethod(tbStartCond.Text, tbPolynomical.Text, _inputFile);
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


			(byte[] cryptedResult, byte[] key) = ((byte[] cryptedResult, byte[] key))cryptographer.Crypting();
			_outputFile = cryptedResult;
			tbKeyResult.Text = GetFileString(key);
			tbResult.Text = GetFileString(cryptedResult);
		}

		private void btnDecrypt_Click(object sender, RoutedEventArgs e)
		{
			lbError.Visibility = Visibility.Hidden;
			if (_inputFile is null || tbPolynomical.Text == "" || tbStartCond.Text == "")
			{
				lbError.Content = "Ошибка в параметрах";
				lbError.Visibility = Visibility.Visible;
				return;
			}

			try
			{
				_director.ConstructLFSRMethod(tbStartCond.Text, tbPolynomical.Text, _inputFile);
			}
			catch (Exception ex)
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

			(byte[] cryptedResult, byte[] key) = ((byte[] cryptedResult, byte[] key))cryptographer.Decrypting();
			_outputFile = cryptedResult;
			tbKeyResult.Text = GetFileString(key);
			tbResult.Text = GetFileString(cryptedResult);
		}

		private void btnLoadFromFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				var FilePath = openFileDialog.FileName;

				var data = File.ReadAllBytes(FilePath);
				_inputFile = data;
				tbSource.Text = GetFileString(data);
			}
		}

		private string GetFileString(byte[] data)
		{
			var bits = new BitArray(data);
			var builder = new StringBuilder(bits.Length);
			for (int i = 0; i < bits.Length; i++)
				builder.Append(bits[i].GetHashCode());
			
			return builder.ToString();
		}

		private void btnSaveToFile_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if ((saveFileDialog.ShowDialog()) == true && (_outputFile != null))
				File.WriteAllBytes(saveFileDialog.FileName, _outputFile);
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			_ownerWindow.Show();
			this.Hide();
		}
	}
}
