using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Labo_2020
{
	public abstract class CartoObj : IIsPointClose
	{
		private int _id;				// must be automatically generated using a "static" object instance counter
		private static int _cpt = 0;	// static object instance counter, is updated everytime the constructor is called

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
			Id = ++_cpt;
		}
		#endregion

		#region METHODES
		public override string ToString()
		{
			return string.Format("{0:00}", Id);
        }

		public virtual void Draw()
		{
			Console.WriteLine(ToString());
		}

		public virtual bool IsPointClose(double latitude, double longitude, double precision)
		{
			return false;
		}
		#endregion
	}
}
