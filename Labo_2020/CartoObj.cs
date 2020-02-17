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
		private int _id;    // must be automatically generated using a "static" object instance counter
		private static int cpt = 0; 

		#region PROPRIETES
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		#endregion

		#region CONSTRUCTEURS
		public CartoObj()
		{
			Id = ++cpt;
		}
		#endregion

		#region METHODES
		public override string ToString()
		{
			return "Id: " + Id;
		}

		public virtual void Draw()
		{
			Console.WriteLine(ToString());
		}
		#endregion
	}
}
