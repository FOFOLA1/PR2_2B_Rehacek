using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_02_BankovniUcet
{
    internal class Ucet
    {
        public double Stav { get; protected set; }

        public virtual void Uloz (double castka)
        {
            if (castka < 0) throw new ArgumentException();
            Stav += castka;
        }

        public virtual void Vyber (double castka)
        {
            if (Stav < castka || castka < 0) throw new ArgumentException();
            Stav -= castka;
        }

        public override string ToString()
        {
            return "Aktuální zůstatek " + Stav;
        }

    }
}
