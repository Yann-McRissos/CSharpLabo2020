using System.Collections.Generic;

namespace MyCartographyObjects
{
	public class MyPolylineBoundingBoxComparer : IComparer<Polyline>
	{
		public int Compare(Polyline pl1, Polyline pl2)
		{
			if(pl1 != null && pl2 != null)
			{
				if (pl1.ListeCoord != null && pl2.ListeCoord != null)
				{
					double xMin = 100, xMax = -100, yMin = 100, yMax = -100;
					double surfacePL1 = 0, surfacePL2 = 0;

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

					xMin = 100; xMax = -100; yMin = 100; yMax = -100;
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
			}
			return 0;
		}
	}
}
