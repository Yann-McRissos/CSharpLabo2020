using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using MyMathLib;

namespace MyCartographyObjects
{
	[Serializable]
	public class Polyline : CartoObj, IPointy, IComparable<Polyline>, IEquatable<Polyline>, ICartoObj
	{
		#region VARIABLES MEMBRES
		private List<Coordonnees> _listCoord;
		[NonSerialized]
		private Color _couleur;
		private string _codeCouleur;
		public int Epaisseur { get; set; }
		private string _description;
		#endregion
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		#region PROPRIETES
		public string Description
		{
			get { return _description; }
			set
			{
				_description = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
			}
		}

		public List<Coordonnees> ListeCoord
		{
			get { return _listCoord; }
			set
			{
				_listCoord = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListeCoord"));
			}
		}

		public Color Couleur
		{
			get { return _couleur; }
			set
			{
				_couleur = value;
				if(CodeCouleur != Couleur.ToString())
					CodeCouleur = Couleur.ToString();
			}
		}

		public string CodeCouleur
		{
			get { return _codeCouleur; }
			set
			{
				_codeCouleur = value;
				if (Couleur.ToString() != CodeCouleur)
					Couleur = (Color)ColorConverter.ConvertFromString(CodeCouleur);
			}
		}
		#endregion

		#region CONSTRUCTEURS
		public Polyline()
        {
			new List<Coordonnees>();
			Couleur = Colors.White;
            Epaisseur = 0;
        }

		public Polyline(Color cl, int ep) : this("", cl, ep)
		{
		}

		public Polyline(string description, Color cl, int ep) : base()
		{
			ListeCoord = null;
			Couleur = cl;
			Epaisseur = ep;
			Description = description;
		}

		// le 2e constructeur appellera le 3e
		public Polyline(List<Coordonnees> liste, Color cl, int ep) : this("", cl, ep, liste)
        {
		}

		// constructeur qui prend une descrption en plus
		public Polyline(string description, Color cl, int ep, List<Coordonnees> liste) : base()
        {
			ListeCoord = liste;
            Couleur = cl;
            Epaisseur = ep;
			Description = description;
        }
		#endregion

		#region METHODES
		public override string ToString()
        {
			if(ListeCoord == null)
				return string.Format("[{0:00}] Polyline ", Id) + Description + "\nCouleur: " + Couleur + "(" + CodeCouleur +")" + "\tEpaisseur: " + Epaisseur;
			else
				return string.Format("[{0:00}] Polyline ", Id) + Description + "\n(\n\t" + string.Join("\n\t", ListeCoord) + "\n)\nCouleur: " + Couleur + "(" + CodeCouleur + ")" + "\tEpaisseur: " + Epaisseur;
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
