using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathLib
{
    public class MathUtil
    {
		#region	METHODES
		public static double Distance(double x1, double y1, double x2, double y2)
		{
			double xTemp, yTemp;

			xTemp = Math.Abs(x1) - Math.Abs(x2);
			yTemp = Math.Abs(y1) - Math.Abs(y2);

			// pythagore
			return Math.Sqrt(Math.Pow(xTemp, 2) + Math.Pow(yTemp, 2));
		}
		#endregion
	}
}
