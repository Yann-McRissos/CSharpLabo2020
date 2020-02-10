using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Labo_2020
{
	public abstract class CartoObj
	{
		private static int _id;    // must be automatically generated using a "static" object instance counter
		
		#region CONSTRUCTEURS
		public CartoObj()
		{
			Interlocked.Increment(ref _id);
		}
		#endregion

		#region METHODES
		#endregion
	}
}
