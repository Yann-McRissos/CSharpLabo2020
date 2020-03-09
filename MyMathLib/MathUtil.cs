using System;

namespace MyMathLib
{
	public class MathUtil
    {
		#region	METHODES
		public static double Distance(double x1, double y1, double x2, double y2)
		{
			double xTemp, yTemp;

			if (x1 > x2)
				xTemp = x1 - x2;
			else
				xTemp = x2 - x1;

			if (y1 > y2)
				yTemp = y1 - y2;
			else
				yTemp = y2 - y1;

			// pythagore
			return Math.Sqrt(Math.Pow(xTemp, 2) + Math.Pow(yTemp, 2));
		}
		#endregion
	}
}
