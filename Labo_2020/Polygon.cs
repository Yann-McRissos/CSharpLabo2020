using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace Labo_2020
{
	public class Polygon : CartoObj, IPointy
	{
		#region VARIABLES MEMBRES
		private List<Coordonnees> _listCoord = new List<Coordonnees>();
		private Color _remplissage;
		private Color _contour;
		private double _opacite;
		#endregion

		#region PROPRIETES
		public void AddCoord(Coordonnees cd)
		{
			_listCoord.Add(cd);
		}

		public IEnumerable<Coordonnees> IECoord
		{
			get { return _listCoord; }
		}

		public Color Remplissage
		{
			get { return _remplissage; }
			set { _remplissage = value; }
		}

		public Color Contour
		{
			get { return _contour; }
			set { _contour = value; }
		}

		public double Opacite
		{
			get { return _opacite; }
			set { _opacite = value; }
		}
		#endregion

		#region CONSTRUCTEURS
		public Polygon()
		{
			Remplissage = Colors.White;
			Contour = Colors.White;
			Opacite = 0;
		}

		public Polygon(List<Coordonnees> liste, Color remp, Color cont, double opa)
		{
			foreach (Coordonnees cd in liste)
			{
				this.AddCoord(cd);
			}
			Remplissage = remp;
			Contour = cont;
			Opacite = opa;
		}
		#endregion

		#region METHODES
		public override string ToString()
		{
			return string.Format("{0:00}", Id)+" "+IECoord.ToString()+" "+Remplissage+" "+Contour+" "+Opacite;
		}

		public override void Draw()
		{
			Console.WriteLine(string.Format("Id: {0:00}", Id));
			foreach (Coordonnees coord in _listCoord)
			{
				Console.WriteLine("\t" + coord);
			}
			Console.WriteLine("\tRemplissage: "+Remplissage);
			Console.WriteLine("\tContour: " + Contour);
			Console.WriteLine("\tOpacite:" + Opacite+"\n");
		}

		public byte NbPoints
		{
			get
			{
				byte b = 0;
				bool exists = false;
				foreach (Coordonnees point in IECoord)
				{
					foreach (Coordonnees cmp in IECoord)
					{
						if (point.Id == cmp.Id)
						{
							exists = true;
							break;
						}
					}
					if (exists == false)
						b++;
				}
				return b;
			}
		}
		#endregion
	}
}
