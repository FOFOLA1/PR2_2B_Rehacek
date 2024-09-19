using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_03_Test_vyrobku
{
    internal class Tester
    {
		private Vyrobek vzor;
		private double tolerance;

		public Vyrobek Vzor
		{
			get { return vzor; }
			set 
			{ 
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				vzor = value; 
			}
		}

		public double Tolerance
		{
			get { return tolerance; }
			set 
			{ 
				if (value < 0 || value > 100)
				{
					throw new ArgumentOutOfRangeException();
				}
				tolerance = value; 
			}
		}

		public Tester(Vyrobek vzor, double tolerance)
		{
			if (tolerance < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			Tolerance = tolerance;
			Vzor = vzor;
		}

		public bool Vyhovuje(Vyrobek vyrobek)
		{
			double original = vzor.Rozmer;
            double max_tolerance = original * (tolerance / 100 + 1);
            double min_tolerance = original * ((100-tolerance)/100);
			if (max_tolerance >= vyrobek.Rozmer && vyrobek.Rozmer >= min_tolerance) return true;
			else return false;
		}

	}
}
