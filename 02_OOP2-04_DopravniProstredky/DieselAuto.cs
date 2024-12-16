﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_04_DopravniProstredky
{
    internal sealed class DieselAuto : Automobil
    {
        public DieselAuto(double maxRychlost, int pocetMist) : base(TypPohonu.SpalovaciMotor, maxRychlost, pocetMist)
        {
        }

        public override void Natankuj()
        {
            Console.WriteLine("Plním nádrž naftou");
        }
    }
}