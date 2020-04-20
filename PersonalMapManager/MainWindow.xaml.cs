using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using MyCartographyObjects;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;

namespace PersonalMapManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MyPersonalMapData currentUser = new MyPersonalMapData();
		List<Coordonnees> listeCoordonnees = new List<Coordonnees>();   // pour la combobox des Polylines/Polygons
		POI temppoi; Polyline temppl; Polygon temppg;
		bool ModifyNotAdd = false;
		string CurrentDir = @"C:\Users\Yannick\OneDrive\Prog\";
		string lb_Fg; string lb_Bg;

		public MainWindow(string Nom, string Prenom, string Mail)
		{
			currentUser.Nom = Nom;
			currentUser.Prenom = Prenom;
			currentUser.Mail = Mail;
			currentUser.Load("");
			foreach (ICartoObj obj in currentUser.OcICartoObj)
			{
				if (obj is POI)
				{
					temppoi = obj as POI;
					Pushpin newPin = new Pushpin();
					newPin.Location = new Location(temppoi.Latitude, temppoi.Longitude);
					bingmap.Children.Add(newPin);
				}
				if(obj is Polyline)
				{
					temppl = obj as Polyline;
					MapPolyline newMPL = new MapPolyline();
					newMPL.Stroke = new SolidColorBrush(temppl.Couleur);
					newMPL.StrokeThickness = temppl.Epaisseur;
					newMPL.Opacity = 1;
					newMPL.Locations = new LocationCollection();
					foreach (Coordonnees cd in temppl.ListeCoord)
					{
						newMPL.Locations.Add(new Location(cd.Latitude, cd.Longitude));
					}
					bingmap.Children.Add(newMPL);
				}
				if(obj is Polygon)
				{
					temppg = obj as Polygon;
					MapPolygon newMPG = new MapPolygon();
					newMPG.Fill = new SolidColorBrush(temppg.Remplissage);
					newMPG.Stroke = new SolidColorBrush(temppg.Contour);
					newMPG.Opacity = temppg.Opacite;
					newMPG.Locations = new LocationCollection();
					foreach (Coordonnees cd in temppg.ListeCoord)
					{
						newMPG.Locations.Add(new Location(cd.Latitude, cd.Longitude));
					}
					bingmap.Children.Add(newMPG);
				}
			}

			InitializeComponent();
			//MessageBox.Show(currentUser.ToString());

			lb_objects.ItemsSource = currentUser.OcICartoObj;
		}

		public MainWindow() : this("", "", "")
		{
		}

		#region MENUCLICKS
		private void ToolsSettings_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SettingsWindow sw = new SettingsWindow();
				sw.UpdateCurrentDir += value => CurrentDir = value;
				sw.UpdateColorText += value => lb_Fg = value;
				sw.UpdateColorBackground += value => lb_Bg = value;
				sw.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Console.WriteLine(ex.Message);
			}
		}

		private void FileOpen_Click(object sender, RoutedEventArgs e)
		{
			string path = "";
			try
			{
				OpenFileDialog openDlg = new OpenFileDialog
				{
					DefaultExt = ".dat",
					InitialDirectory = CurrentDir
				};
				openDlg.ShowDialog();
				path = openDlg.FileName;    // get filepath

				if (path != "")
				{
					currentUser.Load(path); // loads but list doesn't update without next line
					lb_objects.ItemsSource = currentUser.OcICartoObj;
					//MessageBox.Show(currentUser.ToString());	// DEBUG
					foreach (ICartoObj obj in currentUser.OcICartoObj)
					{
						if (obj is POI)
						{
							temppoi = obj as POI;
							Pushpin newPin = new Pushpin();
							newPin.Location = new Location(temppoi.Latitude, temppoi.Longitude);
							bingmap.Children.Add(newPin);
						}
						if (obj is Polyline)
						{
							temppl = obj as Polyline;
							MapPolyline newMPL = new MapPolyline();
							newMPL.Stroke = new SolidColorBrush(temppl.Couleur);
							newMPL.StrokeThickness = temppl.Epaisseur;
							newMPL.Opacity = 1;
							newMPL.Locations = new LocationCollection();
							foreach (Coordonnees cd in temppl.ListeCoord)
							{
								newMPL.Locations.Add(new Location(cd.Latitude, cd.Longitude));
							}
							bingmap.Children.Add(newMPL);
						}
						if (obj is Polygon)
						{
							temppg = obj as Polygon;
							MapPolygon newMPG = new MapPolygon();
							newMPG.Fill = new SolidColorBrush(temppg.Remplissage);
							newMPG.Stroke = new SolidColorBrush(temppg.Contour);
							newMPG.Opacity = temppg.Opacite;
							newMPG.Locations = new LocationCollection();
							foreach (Coordonnees cd in temppg.ListeCoord)
							{
								newMPG.Locations.Add(new Location(cd.Latitude, cd.Longitude));
							}
							bingmap.Children.Add(newMPG);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void FileSave_Click(object sender, RoutedEventArgs e)
		{
			// we're saving the collection of a PersonalMapData object by using the Save() method, 
			// here we're just letting the user select the file to save to
			string path = "";
			try
			{
				SaveFileDialog saveDlg = new SaveFileDialog
				{
					DefaultExt = ".dat",
					InitialDirectory = CurrentDir
				};
				saveDlg.ShowDialog();
				path = saveDlg.FileName;    // get filepath
				if (path != "")
				{
					currentUser.Save(path);
					/* Alright, now listen here buddy cause I got a story for you, 
					 * a story about the many ways this fucking method drilled me another asshole.
					 * You see, to serialize an object, you need to mark the class as [Serializable], easy enough, right ?
					 * Well the teachers wanted us to use the Windows.Something.Media.Color class which is not serializable,
					 * "but, don't you have to just write [NonSerialized] in front of it and it will ignore it ?" I hear you say ?
					 * Well yes but also fuck no it's not that easy. See, I used autoimplemented properties because I'm a lazy fuck
					 * (that is: public Color clr { get; set; } and then you don't have to add the properties, they're right there).
					 * This means that putting [NonSerialized] in front of the now Properties and not field resulted in the compiler
					 * telling me to FUCK RIGHT OFF.
					 * To the internet we go but I didn't really find any conclusive answers.
					 * I ended up finding about that if I set the propertes apart from the variable declaration, 
					 * I could put [NonSerialized] without getting an error, yay !
					 * But now I'm not saving the Color properties from my objects... This fortunately had an easy solution as I simply
					 * had to add a string to save the Color as that (a string) and I could serialize the string.
					 * BUT WAIT, THERE'S MOOOOOOOOOOORE !
					 * Since this is a WPF app (that means it has a GUI), there are events going left and right and events in my classes,
					 * events are not serializab... FUCK!
					 * Well of to the interwebz we go once again then, and I find that I simply have to put [field: NonSerialized]
					 * to remove the compiler error, so I do that.
					 * *gets a different compiler error* "ARE YOU JDKLFJDSFKLFJSDKLFJKLDJFLKRJFLKRJFLKSJFLKSDFJKLSDJF"
					 * Turns out you apparently need a more recent version of the .NET framework than the one I'm working with, 
					 * since this is a school project, I wasn't sure about updating my solution to a more recent version of .NET framework.
					 * So I used the same trick as for the Colors and implemented INotifyPropertyChanged explicitely and I wouldn't even have 
					 * to write [NonSerialized] anymore, great !
					 * ...
					 * Except that broke the PropetyChanged thingelything fuck, basically, my databinded controls were not updating anymore
					 * when a change was happening because I didn't learn to implement interfaces that way and I don't have time to learn.
					 * So either my Serialization doesn't work or my UI is broken.
					 * AND THEN AFTER ALL THAT
					 * After days and hours, after choosing a non working Serialization rather than a broken UI, 
					 * I once again type [field: NonSerialized] in front of my event.
					 * ...
					 * It.
					 * Fucking.
					 * Works.
					 * Not compiler error, no nothing, it just works.
					 * Did I ever tell you what the definition of insanity is?
					 * 
					 * It's 5:51am, fuck me
					 */
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
			// here we're trying to load a .csv file containing info about a POI object
			string path = "";
			try
			{
				OpenFileDialog openDlg = new OpenFileDialog
				{
					DefaultExt = ".csv",
					InitialDirectory = @"C:\Users\Yannick\OneDrive\Prog"
				};
				openDlg.ShowDialog();
				path = openDlg.FileName;    // get filepath

				if (path != "")
				{
					
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
		#endregion

		#region TOOLBARCLICKS
		private void Button_Creer_Click(object sender, RoutedEventArgs e)
		{
			tabC_objectProperties.IsEnabled = true;
			tb_Description.IsEnabled = true;
			bt_objectAppliquer.IsEnabled = true;

			cb_TrajetCoordonnees.ItemsSource = null;
			cb_SurfaceCoordonnees.ItemsSource = null;
			listeCoordonnees.Clear();
		}

		private void Button_Modifier_Click(object sender, RoutedEventArgs e)
		{	// we want to modify the selected object from the listbox, so we go through it to find the object then load it's info in the appropriate controls
			foreach(ICartoObj current in currentUser.OcICartoObj)
			{
				if(current == lb_objects.SelectedItem)
				{
					tabC_objectProperties.IsEnabled = true;
					tb_Description.IsEnabled = true;
					bt_objectAppliquer.IsEnabled = true;
					ModifyNotAdd = true;
					if (current is POI)
					{
						// clear unnecessary controls
						cb_TrajetCoordonnees.ItemsSource = null;
						listeCoordonnees.Clear();
						// open appropriate tab
						tabC_objectProperties.SelectedIndex = 0;
						// cast object
						temppoi = current as POI;
						// fill controls
						tb_Description.Text = temppoi.Description;
						tb_LatitudePOI.Text = temppoi.Latitude.ToString();
						tb_LongitudePOI.Text = temppoi.Longitude.ToString();
					}
					if(current is Polyline)
					{
						cb_TrajetCoordonnees.ItemsSource = null;
						cb_SurfaceCoordonnees.ItemsSource = null;
						listeCoordonnees.Clear();
						tabC_objectProperties.SelectedIndex = 1;

						temppl = current as Polyline;
						tb_Description.Text = temppl.Description;
						cp_plCouleur.SelectedColor = (Color?)temppl.Couleur;
						slider_plEpaisseur.Value = temppl.Epaisseur;
						/* a copy of the list is necessary as doing listeCoordonnees = temppl.ListeCoord
						 * copies the reference(address) of ListeCoord to listeCoordonnees which means
						 * we end up modifying ListeCoord in realtime
						 */
						foreach (Coordonnees cd in temppl.ListeCoord)
						{
							listeCoordonnees.Add(new Coordonnees(cd.Latitude, cd.Longitude));
						}
						if(listeCoordonnees.Count > 0)
						{
							tb_LatitudeTrajet.Text = listeCoordonnees[0].Latitude.ToString();
							tb_LongitudeTrajet.Text = listeCoordonnees[0].Longitude.ToString();
						}
						cb_TrajetCoordonnees.ItemsSource = listeCoordonnees;
						//MessageBox.Show(temppl.Couleur.ToString() + " " + cp_plCouleur.SelectedColor.ToString());	// DEBUG
					}
					if (current is Polygon)
					{
						cb_TrajetCoordonnees.ItemsSource = null;
						cb_SurfaceCoordonnees.ItemsSource = null;
						listeCoordonnees.Clear();
						tabC_objectProperties.SelectedIndex = 2;

						temppg = current as Polygon;
						tb_Description.Text = temppg.Description;
						cp_pgRemplissage.SelectedColor = (Color?)temppg.Remplissage;
						cp_pgContour.SelectedColor = (Color?)temppg.Contour;
						slider_pgOpacite.Value = temppg.Opacite;
						foreach (Coordonnees cd in temppg.ListeCoord)
						{
							listeCoordonnees.Add(new Coordonnees(cd.Latitude, cd.Longitude));
						}
						if (listeCoordonnees.Count > 0)
						{
							tb_LatitudeSurface.Text = listeCoordonnees[0].Latitude.ToString();
							tb_LongitudeSurface.Text = listeCoordonnees[0].Longitude.ToString();
						}
						cb_SurfaceCoordonnees.ItemsSource = listeCoordonnees;
						//MessageBox.Show(temppg.Remplissage.ToString() + "/" + cp_pgRemplissage.SelectedColor.ToString() + "\n" + temppg.Contour.ToString() + "/" + cp_pgContour.SelectedColor.ToString());	// DEBUG
					}
					break;
				}
			}
		}

		private void Button_Supprimer_Click(object sender, RoutedEventArgs e)
		{
			currentUser.OcICartoObj.RemoveAt(lb_objects.SelectedIndex);

			cb_TrajetCoordonnees.ItemsSource = null;
			cb_SurfaceCoordonnees.ItemsSource = null;
			listeCoordonnees.Clear();
		}
		#endregion

		#region OBJMANIPULATIONCLICS
		private void Bt_objectAppliquer_Click(object sender, RoutedEventArgs e)
		{
			switch(tabC_objectProperties.SelectedIndex)
			{
				case 0:
					double lat, lon;
					// convert string to double
					if(!double.TryParse(tb_LatitudePOI.Text, out lat))
					{
						MessageBox.Show("Erreur de conversion POI.Lat");
						break;
					}
					if (!double.TryParse(tb_LongitudePOI.Text, out lon))
					{
						MessageBox.Show("Erreur de conversion POI.Lon");
						break;
					}
					if(ModifyNotAdd)
					{
						temppoi.Description = tb_Description.Text;
						temppoi.Latitude = lat;
						temppoi.Longitude = lon;
						ModifyNotAdd = false;
					}
					else
					{
						POI POItoAdd = new POI(lat, lon, tb_Description.Text);
						currentUser.OcICartoObj.Add(POItoAdd);
					}

					tb_Description.Text = "";
					tb_LatitudePOI.Text = "";
					tb_LongitudePOI.Text = "";
					break;
				case 1:
					if(ModifyNotAdd)
					{
						temppl.Description = tb_Description.Text;
						temppl.Couleur = (Color)cp_plCouleur.SelectedColor;
						temppl.Epaisseur = (int)slider_plEpaisseur.Value;
						/* a copy of the list is necessary as doing listeCoordonnees = temppl.ListeCoord
						 * copies the reference(address) of ListeCoord to listeCoordonnees which means
						 * we end up modifying ListeCoord in realtime
						 */
						temppl.ListeCoord.Clear();
						foreach (Coordonnees cd in listeCoordonnees)
						{
							temppl.ListeCoord.Add(new Coordonnees(cd.Latitude, cd.Longitude));
						}
						ModifyNotAdd = false;
					}
					else
					{
						List<Coordonnees> trajet = new List<Coordonnees>();
						trajet.Clear();
						foreach (Coordonnees cd in listeCoordonnees)
						{
							trajet.Add(new Coordonnees(cd.Latitude, cd.Longitude));
						}
						currentUser.OcICartoObj.Add(new Polyline(tb_Description.Text, (Color)cp_plCouleur.SelectedColor, (int)slider_plEpaisseur.Value, trajet));
					}

					tb_Description.Text = "";
					cp_plCouleur.SelectedColor = (Color?)ColorConverter.ConvertFromString("#FFFFFFFF");
					slider_plEpaisseur.Value = 0;
					tb_LatitudeTrajet.Text = "";
					tb_LongitudeTrajet.Text = "";
					cb_TrajetCoordonnees.ItemsSource = null;
					listeCoordonnees.Clear();
					break;
				case 2:
					if(ModifyNotAdd)
					{
						temppg.Description = tb_Description.Text;
						temppg.Remplissage = (Color)cp_pgRemplissage.SelectedColor;
						temppg.Contour = (Color)cp_pgContour.SelectedColor;
						temppg.Opacite = slider_pgOpacite.Value;
						/* a copy of the list is necessary as doing listeCoordonnees = temppl.ListeCoord
						 * copies the reference(address) of ListeCoord to listeCoordonnees which means
						 * we end up modifying ListeCoord in realtime
						 */
						temppg.ListeCoord.Clear();
						foreach (Coordonnees cd in listeCoordonnees)
						{
							temppg.ListeCoord.Add(new Coordonnees(cd.Latitude, cd.Longitude));
						}
						ModifyNotAdd = false;
					}
					else
					{
						List<Coordonnees> surface = new List<Coordonnees>();
						surface.Clear();
						foreach (Coordonnees cd in listeCoordonnees)
						{
							surface.Add(new Coordonnees(cd.Latitude, cd.Longitude));
						}
						currentUser.OcICartoObj.Add(new Polygon(tb_Description.Text, (Color)cp_pgRemplissage.SelectedColor, (Color)cp_pgContour.SelectedColor, slider_pgOpacite.Value, surface));
					}

					tb_Description.Text = "";
					cp_pgRemplissage.SelectedColor = (Color?)ColorConverter.ConvertFromString("#FFFFFFFF");
					cp_pgContour.SelectedColor = (Color?)ColorConverter.ConvertFromString("#FFFFFFFF");
					slider_pgOpacite.Value = 0;
					tb_LatitudeSurface.Text = "";
					tb_LongitudeSurface.Text = "";
					cb_SurfaceCoordonnees.ItemsSource = null;
					listeCoordonnees.Clear();
					break;
			}
			
			tabC_objectProperties.IsEnabled = false;
			tb_Description.IsEnabled = false;
			bt_objectAppliquer.IsEnabled = false;
		}

		private void Bt_AddCoordTrajet_Click(object sender, RoutedEventArgs e)
		{
			if (cb_TrajetCoordonnees.ItemsSource == null)
				cb_TrajetCoordonnees.ItemsSource = listeCoordonnees;

			// convert string to double
			if (!double.TryParse(tb_LatitudeTrajet.Text, out double lat))
			{
				MessageBox.Show("Erreur de conversion Polyline.Coordonnees.Lat");
			}
			if (!double.TryParse(tb_LongitudeTrajet.Text, out double lon))
			{
				MessageBox.Show("Erreur de conversion Polyline.Coordonnees.Lon");
			}
			listeCoordonnees.Add(new Coordonnees(lat, lon));
		}

		private void Bt_SubCoordTrajet_Click(object sender, RoutedEventArgs e)
		{
			if (listeCoordonnees.Count > 0)
				listeCoordonnees.RemoveAt(listeCoordonnees.Count - 1);
			else
				MessageBox.Show("La liste est vide");
		}

		private void Bt_AddCoordSurface_Click(object sender, RoutedEventArgs e)
		{
			if (cb_SurfaceCoordonnees.ItemsSource == null)
				cb_SurfaceCoordonnees.ItemsSource = listeCoordonnees;

			// convert string to double
			if (!double.TryParse(tb_LatitudeSurface.Text, out double lat))
			{
				MessageBox.Show("Erreur de conversion Polygon.Coordonnees.Lat");
			}
			if (!double.TryParse(tb_LongitudeSurface.Text, out double lon))
			{
				MessageBox.Show("Erreur de conversion Polygon.Coordonnees.Lon");
			}
			listeCoordonnees.Add(new Coordonnees(lat, lon));
		}

		private void Bt_SubCoordSurface_Click(object sender, RoutedEventArgs e)
		{
			if (listeCoordonnees.Count > 0)
				listeCoordonnees.RemoveAt(listeCoordonnees.Count - 1);
			else
				MessageBox.Show("La liste est vide");
		}
		#endregion

		#region GESTION_TEXTBOX
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

		private void Tb_LatitudePOI_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LatitudePOI.Text == "Latitude (Y)" && tb_LatitudePOI.Foreground == Brushes.Gray)
				{
					tb_LatitudePOI.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_LatitudePOI.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LatitudePOI_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LatitudePOI.Text == "")
				{
					tb_LatitudePOI.Foreground = Brushes.Gray;
					tb_LatitudePOI.Text = "Latitude (Y)";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LongitudePOI_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LongitudePOI.Text == "Longitude (X)" && tb_LongitudePOI.Foreground == Brushes.Gray)
				{
					tb_LongitudePOI.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_LongitudePOI.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LongitudePOI_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LongitudePOI.Text == "")
				{
					tb_LongitudePOI.Foreground = Brushes.Gray;
					tb_LongitudePOI.Text = "Longitude (X)";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LatitudeTrajet_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LatitudeTrajet.Text == "Latitude (Y)" && tb_LatitudeTrajet.Foreground == Brushes.Gray)
				{
					tb_LatitudeTrajet.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_LatitudeTrajet.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LatitudeTrajet_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LatitudeTrajet.Text == "")
				{
					tb_LatitudeTrajet.Foreground = Brushes.Gray;
					tb_LatitudeTrajet.Text = "Latitude (Y)";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LongitudeTrajet_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LongitudeTrajet.Text == "Longitude (X)" && tb_LongitudeTrajet.Foreground == Brushes.Gray)
				{
					tb_LongitudeTrajet.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_LongitudeTrajet.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LongitudeTrajet_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LongitudeTrajet.Text == "")
				{
					tb_LongitudeTrajet.Foreground = Brushes.Gray;
					tb_LongitudeTrajet.Text = "Longitude (X)";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LatitudeSurface_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LatitudeSurface.Text == "Latitude (Y)" && tb_LatitudeSurface.Foreground == Brushes.Gray)
				{
					tb_LatitudeSurface.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_LatitudeSurface.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LatitudeSurface_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LatitudeSurface.Text == "")
				{
					tb_LatitudeSurface.Foreground = Brushes.Gray;
					tb_LatitudeSurface.Text = "Latitude (Y)";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LongitudeSurface_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LongitudeSurface.Text == "Longitude (X)" && tb_LongitudeSurface.Foreground == Brushes.Gray)
				{
					tb_LongitudeSurface.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF1F1F1"));
					tb_LongitudeSurface.Text = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Tb_LongitudeSurface_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tb_LongitudeSurface.Text == "")
				{
					tb_LongitudeSurface.Foreground = Brushes.Gray;
					tb_LongitudeSurface.Text = "Longitude (X)";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region METHODES
		#endregion

		private void MapWithPushPins_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
		}
	}
}
