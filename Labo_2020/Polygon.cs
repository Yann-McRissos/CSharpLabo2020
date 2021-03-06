﻿using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.ComponentModel;

namespace MyCartographyObjects
{
	[Serializable]
	public class Polygon : CartoObj, IPointy, ICartoObj
	{
		#region VARIABLES MEMBRES
		private List<Coordonnees> _listCoord;
		[NonSerialized]
		private Color _remplissage;
		public string CodeRemplissage { get; set; }
		[NonSerialized]
		private Color _contour;
		public string CodeContour { get; set; }
		private double _opacite;
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

		public Color Remplissage
		{
			get { return _remplissage; }
			set
			{
				_remplissage = value;
				CodeRemplissage = Remplissage.ToString();
			}
		}

		public Color Contour
		{
			get { return _contour; }
			set
			{
				_contour = value;
				CodeContour = Contour.ToString();
			}
		}

		public double Opacite
		{
			get { return _opacite; }
			set
			{
				value = value / 10;
				if(value >= 1)
					_opacite = 1;
				if (value == 0)
					_opacite = 0;
				else
					Console.WriteLine("Value must be between 0 and 1.");
			}
		}
		#endregion

		#region CONSTRUCTEURS
		public Polygon()
		{
			new List<Coordonnees>();
			Remplissage = Colors.White;
			Contour = Colors.White;
			Opacite = 0;
			Description = "";
		}

		public Polygon(string description, Color remp, Color cont, double opa) : base()
		{
			Remplissage = remp;
			Contour = cont;
			Opacite = opa;
			Description = description;
		}

		public Polygon(Color remp, Color cont, double opa, List<Coordonnees> liste) : this("", remp, cont, opa, liste)
		{
		}

		public Polygon(string description, Color remp, Color cont, double opa, List<Coordonnees> liste) : base()
		{
			ListeCoord = liste;
			Remplissage = remp;
			Contour = cont;
			Opacite = opa;
			Description = description;
		}
		#endregion

		#region METHODES
		public override string ToString()
		{
			if (ListeCoord == null)
				return string.Format("[{0:00}] Polygon ", Id) + Description + "\nRemplissage: " + Remplissage + "(" + CodeRemplissage + ")" + "\tContour: " + Contour + "(" + CodeContour + ")" + "\tOpacite: " + Opacite;
			else
				return string.Format("[{0:00}] Polygon ", Id) + Description + "\n(\n\t" + string.Join("\n\t", ListeCoord) + "\n)\nRemplissage: " + Remplissage + "(" + CodeRemplissage + ")" + "\tContour: " + Contour + "(" + CodeContour + ")" + "\tOpacite: " + Opacite;
		}

		public override bool IsPointClose(double latitude, double longitude, double precision)
		{
			if(ListeCoord != null)
			{
				// si notre point est compris entre les points min et max, il se trouve dans la bounding box
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
				}

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
		#endregion
	}
}
