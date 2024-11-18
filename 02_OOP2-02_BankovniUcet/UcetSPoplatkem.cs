using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_02_BankovniUcet
{
    internal class UcetSPoplatkem : Ucet
    {
        public double PoplatekZaVyber {  get; set; }
        public double PoplatekZaVklad {  get; set; }

        public override void Uloz(double castka)
        {
            if (Stav + castka - PoplatekZaVklad <= 0) throw new ArgumentException();
            Stav += castka - PoplatekZaVklad;
        }

        public override void Vyber(double castka)
        {
            if (Stav - castka - PoplatekZaVyber <= 0) throw new ArgumentException();
            Stav -= castka + PoplatekZaVyber;
        }

    }
}
