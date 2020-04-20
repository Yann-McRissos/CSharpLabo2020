using System;
using System.Collections.Generic;

namespace MyCartographyObjects
{
	public class MyNombreCoordonneesComparer : IComparer<CartoObj>
	{
		public int Compare(CartoObj x, CartoObj y)
		{
			// tester x et y avant de les comparer
			if(x != null && y != null)
			{
				Console.WriteLine("X: " + x + " Y: " + y);
				if (x is IPointy)
				{
					if (y is IPointy)
					{
						Console.WriteLine("Result: " + ((IPointy)x).NbPoints.CompareTo(((IPointy)y).NbPoints));
						return ((IPointy)x).NbPoints.CompareTo(((IPointy)y).NbPoints);
					}
					else
					{
						Console.WriteLine("Result: " + (((IPointy)x).NbPoints - 1));
						return ((IPointy)x).NbPoints - 1;
					}
				}
				else
				{
					if (y is IPointy)
					{
						Console.WriteLine("Result: " + (1 - ((IPointy)y).NbPoints));
						return 1 - ((IPointy)y).NbPoints;
					}
					else
						return 0;
				}
			}
			else
			{
				return -1;
			}
		}
	}
}
