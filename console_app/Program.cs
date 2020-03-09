using System;
using System.Collections.Generic;
using MyCartographyObjects;
using System.Windows.Media;

namespace console_app
{
	class Program
    {
        static void Main(string[] args)
        {
			#region DECLARATION
            Coordonnees c = new Coordonnees(150, 120);
			Coordonnees cDef = new Coordonnees();

			POI poi = new POI(125, 38, "Test");
			POI poiDef = new POI();

			List<Coordonnees> maliste = new List<Coordonnees>();
			{
				maliste.Add(c);
				maliste.Add(cDef);
			}
			Polyline pl = new Polyline(maliste, Color.FromRgb(255, 0, 0), 17);
			Polyline plDef = new Polyline();

			Polygon pg = new Polygon(maliste, Colors.Red, Colors.Blue, 0);
			Polygon pgDef = new Polygon();

			List<CartoObj> listeCO = new List<CartoObj>() { c, cDef, poi, poiDef, pl, plDef, pg, pgDef };

			// Polyline
			List<Coordonnees> listCD2 = new List<Coordonnees>();
			{
				listCD2.Add(new Coordonnees(6, 9));
				listCD2.Add(new Coordonnees(7, 2));
			}
			List<Coordonnees> listCD4 = new List<Coordonnees>();
			{
				listCD4.Add(new Coordonnees(8, 4));
				listCD4.Add(new Coordonnees(3, 0));
				listCD4.Add(new Coordonnees(2, 9));
			}
			List<Coordonnees> listCD5 = new List<Coordonnees>();
			{
				listCD5.Add(new Coordonnees(8, 3));
				listCD5.Add(new Coordonnees(3, 3));
				listCD5.Add(new Coordonnees(5, 7));
			}
			Polyline pl1 = new Polyline();
			Polyline pl2 = new Polyline(listCD2, Colors.Aqua, 5);
			Polyline pl3 = new Polyline();
			Polyline pl4 = new Polyline(listCD4, Colors.Red, 7);
			Polyline pl5 = new Polyline(listCD5, Colors.Green, 9);

			List<Polyline> listePL = new List<Polyline>() { pl1, pl2, pl3, pl4, pl5 };
			#endregion

			#region TESTS
			bool exit = false;
			ConsoleKeyInfo choix;
			
			while(!exit)
			{
				PrintMenu();
				choix = Console.ReadKey();
				Console.Clear();
				switch (choix.Key)
				{
					case ConsoleKey.D1:
						#region TEST 01
						Console.WriteLine("Test Coordonnées: ");
						Console.WriteLine(c.ToString());
						Console.WriteLine(cDef.ToString());
						Console.ReadKey();
						Console.Clear();

						Console.WriteLine("Test POI: ");
						Console.WriteLine(poi.ToString());
						Console.WriteLine(poiDef.ToString());
						Console.ReadKey();
						Console.Clear();

						Console.WriteLine("Test Polyline: ");
						pl.Draw();
						plDef.Draw();
						Console.ReadKey();
						Console.Clear();

						Console.WriteLine("Test Polygon: ");
						pg.Draw();
						pgDef.Draw();
						Console.ReadKey();
						Console.Clear();
						#endregion
						break;
					case ConsoleKey.D2:
						#region TEST 02
						Console.WriteLine("Liste générique d'objets CartoObjs: ");

						foreach (CartoObj co in listeCO)
						{
							Console.WriteLine(co.ToString() + "\n");
						}
						Console.ReadKey();
						Console.Clear();

						Console.WriteLine("Objets implémentant IPointy: ");
						foreach (CartoObj co in listeCO)
						{
							if (co is IPointy)
								Console.WriteLine(co.ToString() + "\n");
						}
						Console.ReadKey();
						Console.Clear();

						Console.WriteLine("Objets n'implémentant pas IPointy: ");
						foreach (CartoObj co in listeCO)
						{
							if (!(co is IPointy))
								Console.WriteLine(co.ToString() + "\n");
						}
						Console.ReadKey();
						Console.Clear();
						#endregion
						break;
					case ConsoleKey.D3:
						#region TEST 03
						Console.WriteLine("Liste générique de 5 objets Polyline: ");
						foreach (Polyline polyline in listePL)
							polyline.Draw();
						Console.ReadKey();
						Console.Clear();

						Console.WriteLine("Tri par ordre croissant de longueur:");
						listePL.Sort();
						foreach (Polyline polyline in listePL)
							polyline.Draw();
						Console.ReadKey();
						Console.Clear();

						Console.WriteLine("Tri par ordre croissant de surface:");
						MyPolylineBoundingBoxComparer polylineCmp = new MyPolylineBoundingBoxComparer();
						listePL.Sort(polylineCmp);
						foreach (Polyline polyline in listePL)
							polyline.Draw();
						Console.ReadKey();
						Console.Clear();
						#endregion
						break;
					case ConsoleKey.D4:
						#region TEST 04
						Console.WriteLine("Comparaison à un Polyline de référence:");
						listePL.Add(pl2);
						foreach (Polyline polyline in listePL.FindAll(cmp => cmp.Equals(pl1)))
							Console.WriteLine(polyline.ToString());
						listePL.Remove(pl2);
						Console.ReadKey();
						Console.Clear();

						Console.WriteLine("Polylines proches d'un point passé en paramètre:");
						foreach (Polyline polyline in listePL)
						{
							if (polyline.IsPointClose(5, 5, 2))
								Console.WriteLine(polyline.ToString());
						}
						Console.ReadKey();
						Console.Clear();
						#endregion
						break;
					case ConsoleKey.D5:
						#region TEST 05
						Console.WriteLine("Mécanisme qui trie une liste de CartoObjs selon le nombre de coordonnées:");
						foreach (CartoObj co in listeCO)
							Console.WriteLine(co.ToString() + "\n");
						Console.ReadKey();
						Console.Clear();
						MyNombreCoordonnesComparer ncComparer = new MyNombreCoordonnesComparer();
						listeCO.Sort(ncComparer);
						foreach (CartoObj co in listeCO)
							Console.WriteLine(co.ToString() + "\n");
						#endregion
						break;
					case ConsoleKey.Escape:
						exit = true;
						break;
				}
			}
			#endregion
		}

		static void PrintMenu()
		{
			Console.Clear();
			Console.WriteLine("1) Test des classes");
			Console.WriteLine("2) Liste générique d'objets CartoObjs");
			Console.WriteLine("3) Liste générique de 5 objets Polyline");
			Console.WriteLine("4) Comparaison des objets de List<Polyline>");
			Console.WriteLine("5) Tri de List<CartoObjs> selon le nb de Coord");
			Console.WriteLine("Esc) Quitter");
		}
	}
}
