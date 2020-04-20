using System;
using System.ComponentModel;

namespace MyCartographyObjects
{
	[Serializable]
	public class POI : Coordonnees, ICartoObj
	{
		private string _description;
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
		#endregion

		#region CONSTRUCTEURS
		public POI(string description, Coordonnees cd) : this(cd.Latitude, cd.Longitude, description)
		{

		}

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

			return (Description + " (" + formattedlat + ", " + formattedlon + ")");
		}
		#endregion
	}
}
