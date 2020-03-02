using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo_2020;
using System.Windows.Media;

namespace console_app
{
    class Program
    {
        static void Main(string[] args)
        {
			/*
			#region TESTS CLASSES
			Console.WriteLine("Test Coordonnées: ");
            Coordonnees c = new Coordonnees(150, 120);
			Coordonnees cDef = new Coordonnees();

			Console.WriteLine(c.ToString());
			Console.WriteLine(cDef.ToString());
			Console.ReadKey();
			Console.Clear();

			Console.WriteLine("Test POI: ");
			POI poi = new POI(125, 38, "Test");
			POI poiDef = new POI();

			Console.WriteLine(poi.ToString());
			Console.WriteLine(poiDef.ToString());
			Console.ReadKey();
			Console.Clear();

			Console.WriteLine("Test Polyline: ");
			List<Coordonnees> maliste = new List<Coordonnees>();
			{
				maliste.Add(c);
				maliste.Add(cDef);
			}
			Polyline pl = new Polyline(maliste, Color.FromRgb(255, 0, 0), 17);
			Polyline plDef = new Polyline();

			pl.Draw();
			plDef.Draw();
			Console.ReadKey();
			Console.Clear();

			Console.WriteLine("Test Polygon: ");
			Polygon pg = new Polygon(maliste, Colors.Red, Colors.Blue, 0);
			Polygon pgDef = new Polygon();

			pg.Draw();
			pgDef.Draw();
			Console.ReadKey();
			Console.Clear();
			#endregion

			#region LISTE GENERIQUE CartoObjs
			Console.WriteLine("Liste générique d'objets CartoObjs: ");
			List<CartoObj> listeCO = new List<CartoObj>() { c, cDef, poi, poiDef, pl, plDef, pg, pgDef };
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
			*/
			#region LISTE GENERIQUE Polyline
			Console.WriteLine("Liste générique d'objets Polyline: ");
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

			foreach (Polyline polyline in listePL)
				polyline.Draw();
			Console.ReadKey();
			Console.Clear();

			Console.WriteLine("Après tri:");
			listePL.Sort();
			foreach (Polyline polyline in listePL)
				polyline.Draw();
			Console.ReadKey();
			Console.Clear();


			#endregion
		}
	}
}
