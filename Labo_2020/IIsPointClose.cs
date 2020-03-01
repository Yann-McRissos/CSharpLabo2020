using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_2020
{
	public interface IIsPointClose
	{
		bool IsPointClose(double latitude, double longitude, int precision);
	}
}
