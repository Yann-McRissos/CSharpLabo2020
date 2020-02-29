using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Labo_2020
{
	public class Polyline : CartoObj, IIsPointClose
	{
        #region VARIABLES MEMBRES
        private List<Coordonnees> _listCoord = new List<Coordonnees>();
        private Color _couleur;
        private int _epaisseur;
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

        public Color Couleur
        {
            get { return _couleur; }
            set { _couleur = value; }
        }

        public int Epaisseur
        {
            get { return _epaisseur; }
            set { _epaisseur = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public Polyline()
        {
            Couleur = Colors.White;
            Epaisseur = 10;
        }

        public Polyline(List<Coordonnees> liste, Color cl, int ep)
        {
            foreach (Coordonnees cd in liste)
            {
                this.AddCoord(cd);
            }
            Couleur = cl;
            Epaisseur = ep;
        }
        #endregion

        #region METHODES
        public override string ToString()
        {
            return string.Format("{0:00}", Id) + " " + IECoord.ToString() + " " + Couleur.ToString() + " " + Epaisseur;
        }

        public override void Draw()
        {
            Console.WriteLine(string.Format("Id: {0:00}", Id));
			foreach (Coordonnees coord in _listCoord)
			{
				Console.WriteLine("\t" + coord);
			}
			Console.WriteLine("\tCouleur: " + Couleur);
			Console.WriteLine("\tEpaisseur: " + Epaisseur+"\n");
        }

		public bool IsPointClose(double latitude, double longitude, int precision)
		{
			Coordonnees temp = new Coordonnees(latitude, longitude);

			foreach (Coordonnees c in IECoord)
			{
				double totalLat = c.Latitude - temp.Latitude;
				double totalLon = c.Longitude - temp.Longitude;
				if (Math.Abs(totalLat) <= precision && Math.Abs(totalLon) <= precision)
					return true;
				else
					return false;
			}
			return false;
		}
		#endregion
	}
}
