using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_02_Tachometr
{
    internal class Tachometr
    {
		public Tachometr()
		{
			Stav = 0;
		}

		public int Stav { get; private set; } = 0;

		public int Ujed(int kilometry)
		{
			if (kilometry < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
            Stav += kilometry;
			return Stav;
		}




	}
}
