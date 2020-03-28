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
	}
}
