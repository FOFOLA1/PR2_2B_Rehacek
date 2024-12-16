using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_04_DopravniProstredky
{
    internal sealed class ElektroAuto : Automobil
    {
        public ElektroAuto(double maxRychlost, int pocetMist) : base(TypPohonu.Elektromotor, maxRychlost, pocetMist)
        {
        }

        public override void Natankuj()
        {
            Console.WriteLine("Připojuji na nabíječku");
        }
    }
}
