using System.Windows;
using System.IO;
using System.Collections.Generic;

namespace PersonalMapManager
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
			//var files = Directory.EnumerateFiles(@"C:\Users\Yannick\OneDrive\Prog", ".csv");
			DirectoryInfo di = new DirectoryInfo(@"C:\Users\Yannick\OneDrive\Prog");
			bool found = false;
			
			foreach(var file in di.GetFiles())
			{
				if (file.Name == (textBox_Nom.GetLineText(0) + ".csv"))
				{
					found = true;
					MainWindow mw = new MainWindow();
					mw.Show();
					this.Close();
				}
			}
			if (found == false)
			{
				MainWindow mw = new MainWindow(textBox_Nom.Text, textBox_Prenom.Text, textBox_Email.Text);
				mw.Show();
				this.Close();
			}
		}

		private void Button_Annuler_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void TextBox_Email_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (textBox_Prenom.Text != "" && textBox_Nom.Text != "" && textBox_Email.Text != "")
				button_Connexion.IsEnabled = true;
			else
				button_Connexion.IsEnabled = false;
		}

		private void TextBox_Nom_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (textBox_Prenom.Text != "" && textBox_Nom.Text != "" && textBox_Email.Text != "")
				button_Connexion.IsEnabled = true;
			else
				button_Connexion.IsEnabled = false;
		}

		private void TextBox_Prenom_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (textBox_Prenom.Text != "" && textBox_Nom.Text != "" && textBox_Email.Text != "")
				button_Connexion.IsEnabled = true;
			else
				button_Connexion.IsEnabled = false;
		}
	}
}
