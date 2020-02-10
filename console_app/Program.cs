using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo_2020;

namespace console_app
{// le constructeur par défaut est censé utiliser le constructeur d'initialisation selon l'énoncé (p5)
    class Program
    {
        static void Main(string[] args)
        {
			// test classe Coordonnees
            Coordonnees c = new Coordonnees(150, 120);
			Coordonnees cDef = new Coordonnees();

			Console.WriteLine("c: " + c.ToString());
			Console.WriteLine("cDef: " + cDef.ToString());

			// test classe POI
			POI poi = new POI(125, 38, "Test");
			POI poiDef = new POI();

			Console.WriteLine("poi: " + poi);
			Console.WriteLine("poiDef: " + poiDef);
		}
    }
}
