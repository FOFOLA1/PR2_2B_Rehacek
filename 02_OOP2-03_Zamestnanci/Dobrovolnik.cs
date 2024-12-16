using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_03_Zamestnanci
{
    internal class Dobrovolnik : Zamestnanec
    {
        public override int Mzda() => 0;

        public Dobrovolnik(string jmeno, string prijmeni) : base(jmeno, prijmeni)
        {
        }

    }
}
