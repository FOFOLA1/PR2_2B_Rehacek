﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_06_cv_Utvary
{
    internal class Ctverec : IUtvar
    {
        public double Strana {  get; private set; }
        public string Nazev => "Čtverec";

        public Ctverec(double strana)
        {
            Strana = strana;
        }

        public double GetObvod() => 4 * Strana;
        public double GetObsah() => Strana * Strana;

        public override string? ToString() => $"Čtverec o straně {Strana} cm";
    }
}
