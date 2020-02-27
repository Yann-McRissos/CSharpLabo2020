using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_2020
{
    public class Coordonnees : CartoObj
    {
        #region VARIABLES MEMBRES
        private double _latitude;
        private double _longitude;
        #endregion

        #region PROPRIETES
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public Coordonnees(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public Coordonnees() : this(0, 0)
        {
            // le constructeur par défaut est censé utiliser 
            // le constructeur d'initialisation selon l'énoncé (p5)
            // d'ou le this(0, 0)
        }
		#endregion

		#region METHODES
		public override void Draw()
		{
			Console.WriteLine(string.Format("{0:00}", Id));
        }

		public override string ToString()
        {
			return string.Format("Id: {0:00}", Id) + " (" + Latitude + ", " + Longitude + ")";
        }
		#endregion
	}
}
