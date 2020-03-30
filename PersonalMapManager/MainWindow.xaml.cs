using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;

namespace PersonalMapManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(string Nom, string Prenom, string Mail)
		{
			InitializeComponent();
			if(Nom != "")
				MessageBox.Show(Nom + " " + Prenom + " " + Mail);
		}

		public MainWindow() : this("", "", "")
		{

		}

		private void FileOpen_Click(object sender, RoutedEventArgs e)
		{
			string path = "";
			try
			{
				OpenFileDialog openDlg = new OpenFileDialog();
				openDlg.ShowDialog();
				path = openDlg.FileName;    // get filepath

				if (path != "")
				{
					this.Title = "PersonalMapData" + " - " + path;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void FileSave_Click(object sender, RoutedEventArgs e)
		{
			string path = "";
			try
			{
				SaveFileDialog saveDlg = new SaveFileDialog();
				saveDlg.ShowDialog();
				path = saveDlg.FileName;    // get filepath

				if (path != "")
				{
					if (!File.Exists(path))     // if file does not exist
					{
						
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void FileExit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void POIImport_Click(object sender, RoutedEventArgs e)
		{
			string path = "";
			try
			{
				OpenFileDialog openDlg = new OpenFileDialog();
				openDlg.ShowDialog();
				path = openDlg.FileName;    // get filepath

				if (path != "")
				{
					this.Title = "PersonalMapData" + " - " + path;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void POIExport_Click(object sender, RoutedEventArgs e)
		{
			string path = "";
			try
			{
				SaveFileDialog saveDlg = new SaveFileDialog();
				saveDlg.ShowDialog();
				path = saveDlg.FileName;    // get filepath

				if (path != "")
				{
					if (!File.Exists(path))     // if file does not exist
					{

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void TrajetImport_Click(object sender, RoutedEventArgs e)
		{
			string path = "";
			try
			{
				OpenFileDialog openDlg = new OpenFileDialog();
				openDlg.ShowDialog();
				path = openDlg.FileName;    // get filepath

				if (path != "")
				{
					this.Title = "PersonalMapData" + " - " + path;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void TrajetExport_Click(object sender, RoutedEventArgs e)
		{
			string path = "";
			try
			{
				SaveFileDialog saveDlg = new SaveFileDialog();
				saveDlg.ShowDialog();
				path = saveDlg.FileName;    // get filepath

				if (path != "")
				{
					if (!File.Exists(path))     // if file does not exist
					{

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void ToolsAbout_Click(object sender, RoutedEventArgs e)
		{
			AboutWindow aw = new AboutWindow();
			aw.Show();
		}

		private void Tb_Description_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_Description.Text == "Description" && tb_Description.Foreground == Brushes.Gray)
				{
					tb_Description.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_Description.Text = "";
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_Description_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if(tb_Description.Text == "")
				{
					tb_Description.Foreground = Brushes.Gray;
					tb_Description.Text = "Description";
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_Latitude_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_Latitude.Text == "Latitude (Y)" && tb_Latitude.Foreground == Brushes.Gray)
				{
					tb_Latitude.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_Latitude.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_Latitude_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_Latitude.Text == "")
				{
					tb_Latitude.Foreground = Brushes.Gray;
					tb_Latitude.Text = "Latitude (Y)";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_Longitude_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_Longitude.Text == "Longitude (X)" && tb_Longitude.Foreground == Brushes.Gray)
				{
					tb_Longitude.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_Longitude.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_Longitude_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_Longitude.Text == "")
				{
					tb_Longitude.Foreground = Brushes.Gray;
					tb_Longitude.Text = "Longitude (X)";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
