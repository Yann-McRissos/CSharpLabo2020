using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_2020
{
	public class POI : Coordonnees
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

		public POI()
		{
			Latitude = 50.6109846;
			Longitude = 5.5098916;
			Description = "Haute Ecole de la Province de Liège";
		}
		#endregion

		#region METHODES
		public override void Draw()
		{
			Console.WriteLine("Id: " + Id);
		}

		public override string ToString()
		{
			string formattedlat = string.Format("{0:N3}", this.Latitude);
			string formattedlon = string.Format("{0:N3}", this.Longitude);

			Draw();
			return Description + " (" + formattedlat + ", " + formattedlon + ")";
		}
		#endregion
	}
}
