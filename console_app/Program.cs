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
			// test classe Coordonnees
            Coordonnees c = new Coordonnees(150, 120);
			Coordonnees cDef = new Coordonnees();

			Console.WriteLine(c.ToString());
			Console.WriteLine(cDef.ToString());

			// test classe POI
			POI poi = new POI(125, 38, "Test");
			POI poiDef = new POI();

			Console.WriteLine(poi.ToString());
			Console.WriteLine(poiDef.ToString());
            
            List<Coordonnees> maliste = new List<Coordonnees>();
			maliste.Add(c);
			maliste.Add(cDef);
			
            Polyline pl = new Polyline(maliste, Color.FromRgb(255, 0, 0), 17);
            Polyline plDef = new Polyline();

			Console.WriteLine(pl.ToString());
			Console.WriteLine(plDef.ToString());

			pl.Draw();
			plDef.Draw();

			Polygon pg = new Polygon(maliste, Colors.Red, Colors.Blue, 0);
			Polygon pgDef = new Polygon();

			pg.Draw();
			pgDef.Draw();
		}
    }
}
