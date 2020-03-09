using System.Collections.Generic;

namespace MyCartographyObjects
{
	public class MyNombreCoordonnesComparer : IComparer<CartoObj>
	{
		public int Compare(CartoObj x, CartoObj y)
		{
			// tester x et y avant de les comparer
			if (x is IPointy)
			{
				if (y is IPointy)
					return ((IPointy)x).NbPoints.CompareTo(((IPointy)y).NbPoints);
				else
					return ((IPointy)x).NbPoints - 1;
			}
			else
			{
				if (y is IPointy)
					return 1 - ((IPointy)y).NbPoints;
				else
					return 0;
			}
		}
	}
}
