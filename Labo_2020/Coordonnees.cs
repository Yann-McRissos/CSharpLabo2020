using System;
using System.ComponentModel;
using MyMathLib;

namespace MyCartographyObjects
{
	[Serializable]
	public class Coordonnees : CartoObj, INotifyPropertyChanged
    {
        #region VARIABLES MEMBRES
        private double _latitude;	// Y
        private double _longitude;  // X
		#endregion
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		#region PROPRIETES

		public double Latitude
        {
            get { return _latitude; }
            set
			{
				_latitude = value;
				if(PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
				}
			}
        }

        public double Longitude
        {
            get { return _longitude; }
			set
			{
				_longitude = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Longitude"));
			}
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
		public override string ToString()
        {
			return string.Format("[{0:00}]", Id) + " (" + Latitude + ", " + Longitude + ")";
        }

		public override bool IsPointClose(double latitude, double longitude, double precision)
		{
			return (MathUtil.Distance(Longitude, Latitude, longitude, latitude) <= precision);
		}
		#endregion
	}
}
