using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects
{
	public class MyPolylineBoundingBoxComparer : IComparer<Polyline>
	{
		public int Compare(Polyline pl1, Polyline pl2)
		{
			if(pl1.ListeCoord != null && pl2.ListeCoord != null)
			{
				double xMin = 0, xMax = 0, yMin = 0, yMax = 0, surfacePL1 = 0, surfacePL2 = 0;

				foreach (Coordonnees c in pl1.ListeCoord)
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
				surfacePL1 = (xMax - xMin) * (yMax - yMin);

				foreach (Coordonnees c in pl2.ListeCoord)
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
				surfacePL2 = (xMax - xMin) * (yMax - yMin);

				return surfacePL1.CompareTo(surfacePL2);
			}
			return 0;
		}
	}
}
