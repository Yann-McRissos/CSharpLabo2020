using System;
using System.Collections.Generic;
using System.Windows.Media;
using MyMathLib;

namespace MyCartographyObjects
{
	public class Polyline : CartoObj, IPointy, IComparable<Polyline>, IEquatable<Polyline>, ICartoObj
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

		public override bool IsPointClose(double latitude, double longitude, double precision)
		{
			if(ListeCoord != null)
			{
				POI temp = new POI(latitude, longitude, "Comparer");
				double xMin = 100, xMax = -100, yMin = 100, yMax = -100;

				foreach (Coordonnees c in ListeCoord)
				{
					xMin = 100; xMax = -100; yMin = 100; yMax = -100;
					if (c.Longitude > xMax)
						xMax = c.Longitude;
					if (c.Longitude < xMin)
						xMin = c.Longitude;
					if (c.Latitude > yMax)
						yMax = c.Latitude;
					if (c.Latitude < yMin)
						yMin = c.Latitude;

					// si la distance qui sépare le point d’un des segments de celle-ci est inférieure à la précision
					if (temp.IsPointClose(c.Latitude, c.Longitude, precision)) // returns true when it shouldn't
						return true;
				}

				/* un point se trouve proche de la ligne si elle est proche d’un de ses points
				 * si notre point est compris entre les points min et max, il est proche
				 */
				return (xMin <= longitude && longitude <= xMax) && (yMin <= latitude && latitude <= yMax);
			}
			return false;
		}

		public int NbPoints
		{
			get
			{
				if (ListeCoord != null)
					return ListeCoord.Count;
				return 0;
			}
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

		public bool Equals(Polyline other)
		{
			// si les Id sont identiques, ce sont les mêmes points avec les mêmes coordonnées
			if (this.Id == other.Id)
				return true;

			// On regarde s'il y a de l'écart entre les 2 points
			double difference = 0;
			difference = other.Longueur() - this.Longueur();
			if (difference == 0)
				return true;
			else
				return false;
		}
		#endregion
	}
}
