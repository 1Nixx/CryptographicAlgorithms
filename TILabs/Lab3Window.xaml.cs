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
using System.Numerics;
using Core.Interfaces;
using Core.Models;
using Lab3.Elgamal_Method;

namespace TILabs
{
	/// <summary>
	/// Interaction logic for Lab3Window.xaml
	/// </summary>
	public partial class Lab3Window : Window
	{
		private Window _ownerWindow { get; }
		private Builder _builder { get; }
		private Director _director { get; }

		private ICryptographer _cryptographer { get; set; }

		private bool _IsEnableToCrypt { get; set; }

		private byte[] _inputFile { get; set; }
		private byte[] _outputFile { get; set; }

		public Lab3Window(Window ownerWindow)
		{
			_ownerWindow = ownerWindow;
			_builder = new ConcreteBuilder();
			_director = new Director(_builder);
			InitializeComponent();
			cbOpenKey.IsEnabled = false;
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

				var data = File.ReadAllBytes(FilePath);
				_inputFile = data;
				tbSource.Text = GetFileString(data, 1);
				tbPkey_KeyDown(sender, new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Enter));
			}
		}

		private string GetFileString(byte[] data, int itemSize)
		{
			var builder = new StringBuilder(data.Length/itemSize); 
			
			for (int i = 0; i < data.Length / itemSize; i++)
			{
				var number = new BigInteger(data.Skip(i * itemSize).Take(itemSize).ToArray(), isUnsigned: true);
				builder.Append(number.ToString() + " ");
			}

			return builder.ToString();
		}

		private void btnSaveToFile_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if ((_outputFile != null) && (saveFileDialog.ShowDialog()) == true)
				File.WriteAllBytes(saveFileDialog.FileName, _outputFile);
		}

		private void tbPkey_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				lbError.Visibility = Visibility.Hidden;
				if (tbPkey.Text != "" && tbKKey.Text != "" && tbXKey.Text != "")
				{
					try
					{
						_director.ConstructElgamalMethod(tbPkey.Text, tbXKey.Text, tbKKey.Text, _inputFile);
					}
					catch (Exception ex)
					{
						lbError.Content = ex.Message;
						lbError.Visibility = Visibility.Visible;
						_IsEnableToCrypt = false;
						return;
					}

					_cryptographer = _builder.GetCryptographer();

					try
					{
						bool dataRes = _cryptographer.PrepareData();
						if (!dataRes)
						{
							lbError.Content = "Ошибка в параметрах";
							lbError.Visibility = Visibility.Visible;
							_IsEnableToCrypt = false;
							return;
						}
					}
					catch (ArgumentException ex)
					{
						lbError.Content = ex.Message;
						lbError.Visibility = Visibility.Visible;
						_IsEnableToCrypt = false;
						return;
					}

					List<int> keys = ElgamalValidator.GetOpenKey((_cryptographer as Cryptographer<byte[], ElgamalKey>).CryptingInfo.Key.P);
					cbOpenKey.Items.Clear();
					foreach (int key in keys)
						cbOpenKey.Items.Add(key);
					cbOpenKey.SelectedIndex = 0;
					cbOpenKey.IsEnabled = true;
					_IsEnableToCrypt = true;
				}
				else
				{
					cbOpenKey.Items.Clear();
					cbOpenKey.IsEnabled = false;
					_IsEnableToCrypt = false;
				}
			}
		}

		private void cbOpenKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_cryptographer is not null)
			{
				if (cbOpenKey.SelectedItem != null)
				{
					tbResult.Text = "";
					_outputFile = null;
					(_cryptographer as Cryptographer<byte[], ElgamalKey>).CryptingInfo.Key.G = (int)cbOpenKey.SelectedItem;
				}
			}
		}

		private void btnCrypt_Click(object sender, RoutedEventArgs e)
		{
			if (_cryptographer is null || _inputFile is null || _IsEnableToCrypt == false)
				return;

			var cryptedData = (byte[])_cryptographer.Crypting();
			_outputFile = cryptedData;

			var cryptedFileString = GetFileString(cryptedData, (new BigInteger((_cryptographer as Cryptographer<byte[], ElgamalKey>).CryptingInfo.Key.P)).GetByteCount(isUnsigned: true));

			tbResult.Text = cryptedFileString;
		}

		private void btnDecrypt_Click(object sender, RoutedEventArgs e)
		{
			if (_cryptographer is null || _inputFile is null || _IsEnableToCrypt == false)
				return;

			var cryptedData = (byte[])_cryptographer.Decrypting();
			_outputFile = cryptedData;

			var cryptedFileString = GetFileString(cryptedData, 1);

			tbResult.Text = cryptedFileString;
		}

		private void tbData_TextChanged(object sender, TextChangedEventArgs e)
		{
			tbResult.Text = "";

			if (ReferenceEquals(sender, tbPkey))
			{
				cbOpenKey.IsEnabled = false;
				cbOpenKey.Items.Clear();
			}
		}


	}
}
