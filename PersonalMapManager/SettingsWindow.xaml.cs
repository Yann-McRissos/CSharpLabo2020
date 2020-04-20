using Microsoft.Win32;
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
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace PersonalMapManager
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
		public delegate void DataUpdaterHandler(string data);
		public event DataUpdaterHandler UpdateCurrentDir;
		public event DataUpdaterHandler UpdateColorText;
		public event DataUpdaterHandler UpdateColorBackground;

        public SettingsWindow()
        {
            InitializeComponent();
        }

		private void ButtonDir_Click(object sender, RoutedEventArgs e)
		{
			CommonOpenFileDialog dlg = new CommonOpenFileDialog
			{
				IsFolderPicker = true,
				InitialDirectory = @"C:\Users\Yannick\OneDrive\Prog\",
				Multiselect = false
			};
			if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
			{
				tb_directory.Text = dlg.FileName;
			}
		}

		private void Tb_directory_TextChanged(object sender, TextChangedEventArgs e)
		{
			if(tb_directory.Foreground == Brushes.Gray)
				tb_directory.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
		}

		private void Bt_Ok_Click(object sender, RoutedEventArgs e)
		{
			if (UpdateCurrentDir != null)
			{
				UpdateCurrentDir(tb_directory.Text);
				UpdateColorText(cp_lbColorText.SelectedColor.ToString());
				UpdateColorBackground(cp_lbColorBackground.SelectedColor.ToString());
			}
			this.Close();
		}
		
		private void Bt_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Bt_Apply_Click(object sender, RoutedEventArgs e)
		{
			if (UpdateCurrentDir != null)
			{
				UpdateCurrentDir(tb_directory.Text);
				UpdateColorText(cp_lbColorText.SelectedColor.ToString());
				UpdateColorBackground(cp_lbColorBackground.SelectedColor.ToString());
			}
		}
	}
}
