using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace Labo_2020
{
	public class Polygon : CartoObj, IIsPointClose, IPointy
	{
		#region VARIABLES MEMBRES
		private List<Coordonnees> _listCoord;
		private Color Remplissage { get; set; }
		private Color Contour { get; set; }
		private double _opacite;
		#endregion

		#region PROPRIETES
		public List<Coordonnees> ListeCoord
		{
			get { return _listCoord; }
			set { _listCoord = value; }
		}

		public double Opacite
		{
			get { return _opacite; }
			set
			{
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
		}

		public Polygon(List<Coordonnees> liste, Color remp, Color cont, double opa) : base()
		{
			ListeCoord = liste;
			Remplissage = remp;
			Contour = cont;
			Opacite = opa;
		}
		#endregion

		#region METHODES
		public override string ToString()
		{
			if (ListeCoord == null)
				return string.Format("Id Polygon: {0:00}", Id) + "\nR: " + Remplissage + "\nC: " + Contour + "\nO: " + Opacite + "\n";
			else
				return string.Format("Id Polygon:{0:00}\n", Id) + "(\n\t" + string.Join("\n\t", ListeCoord) + "\n)\nR: " + Remplissage + "\nC: " + Contour + "\nO: " + Opacite + "\n";
		}

		public override void Draw()
		{
			Console.WriteLine(ToString());
		}

		public override bool IsPointClose(double latitude, double longitude, double precision)
		{
			// si notre point est compris entre les points min et max, il se trouve dans la bounding box
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
			}

			return (xMin <= longitude && longitude <= xMax) && (yMin <= latitude && latitude <= yMax);
		}

		public int NbPoints
		{
			get { return ListeCoord.Count; }
		}
		#endregion
	}
}
