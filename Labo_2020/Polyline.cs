using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MyMathLib;

namespace Labo_2020
{
	public class Polyline : CartoObj, IIsPointClose, IPointy, IComparable<Polyline>
	{
		#region VARIABLES MEMBRES
		private List<Coordonnees> _listCoord;
        private Color Couleur { get; set; }
        private int Epaisseur { get; set; }
        #endregion

        #region PROPRIETES
        public List<Coordonnees> ListeCoord
		{
			get { return _listCoord; }
			set { _listCoord = value; }
		}
		#endregion

		#region CONSTRUCTEURS
		public Polyline()
        {
			new List<Coordonnees>();
			Couleur = Colors.White;
            Epaisseur = 0;
        }

        public Polyline(List<Coordonnees> liste, Color cl, int ep) : base()
        {
			ListeCoord = liste;
            Couleur = cl;
            Epaisseur = ep;
        }
        #endregion

        #region METHODES
        public override string ToString()
        {
			if(ListeCoord == null)
				return string.Format("Id Polyline: {0:00}", Id) + "\nC: " + Couleur + "\nE: " + Epaisseur + "\n";
			else
				return string.Format("Id Polyline: {0:00}\n", Id) + "(\n\t" + string.Join("\n\t", ListeCoord) + "\n)\nC: " + Couleur + "\nE: " + Epaisseur + "\n";
        }

        public override void Draw()
        {
			Console.WriteLine(ToString());
        }

		public override bool IsPointClose(double latitude, double longitude, double precision)
		{
			POI temp = new POI(latitude, longitude, "Comparer");
			double xMin = 0, xMax = 0, yMin = 0, yMax = 0;

			foreach (Coordonnees c in ListeCoord)
			{
				if (c.Longitude > xMax)
					xMax = c.Longitude;
				if (c.Longitude < xMin)
					xMin = c.Longitude;
				if (c.Latitude > yMax)
					yMax = c.Latitude;
				if (c.Latitude < yMin)
					yMin = c.Latitude;

				// si la distance qui sépare le point d’un des segments de celle-ci est inférieure à la précision
				if (temp.IsPointClose(c.Latitude, c.Longitude, precision))
					return true;
			}

			/* un point se trouve proche de la ligne si elle est proche d’un de ses points
			 * si notre point est compris entre les points min et max, il est proche
			 */
			return (xMin <= longitude && longitude <= xMax) && (yMin <= latitude && latitude <= yMax);
		}

		public int NbPoints
		{
			get { return ListeCoord.Count; }
		}

		public int CompareTo(Polyline other)
		{
			return Longueur().CompareTo(other.Longueur());
		}

		public double Longueur()
		{
			if(ListeCoord != null)
			{
				Coordonnees cPrev = null;
				double segment = 0;

				// La longueur d’une polyline se mesure par la somme des longueurs des segments qui la compose
				// on calcule la distance entre 2 points et on additionne
				foreach (Coordonnees c in ListeCoord)
				{
					if (cPrev != null)
						segment += MathUtil.Distance(c.Longitude, c.Latitude, cPrev.Longitude, cPrev.Latitude);
					cPrev = c;
				}
				return segment;
			}
			return 0;
		}
		#endregion
	}
}
