using System;

namespace MyCartographyObjects
{
	public class POI : Coordonnees, ICartoObj
	{
		private string _description;

		#region PROPRIETES
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}
		#endregion

		#region CONSTRUCTEURS
		public POI(double lat, double lon, string desc)
		{
			Latitude = lat;
			Longitude = lon;
			Description = desc;
		}

		public POI() : this(50.6109846, 5.5098916, "HEPL")
		{
		}
		#endregion

		#region METHODES
		public override string ToString()
		{
			string formattedlat = string.Format("{0:N3}", this.Latitude);
			string formattedlon = string.Format("{0:N3}", this.Longitude);

			return string.Format("Id: {0:00}", Id) + " " + Description + " (" + formattedlat + ", " + formattedlon + ")";
		}
		#endregion
	}
}
