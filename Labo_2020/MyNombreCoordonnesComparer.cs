using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects
{
	public class MyNombreCoordonnesComparer : IComparer<CartoObj>
	{
		public int Compare(CartoObj x, CartoObj y)
		{
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
