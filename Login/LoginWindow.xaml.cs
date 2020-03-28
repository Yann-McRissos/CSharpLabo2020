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
using System.IO;


namespace Login
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		private void Button_Connexion_Click(object sender, RoutedEventArgs e)
		{
			var files = Directory.EnumerateFiles(@"C:\Users\Yannick\OneDrive\Prog", ".csv");

			foreach(string file in files)
			{
				if (file.ToString() == textBox_Nom.GetLineText(0) + ".csv")
				{

				}
			}
		}

		private void Button_Annuler_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
