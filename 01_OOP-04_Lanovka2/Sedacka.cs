﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_04_Lanovka2
{
    internal class Sedacka
    {
        public Clovek Pasazer { get; set; }
        public Sedacka Predchozi { get; set; }
        //public Sedacka Dalsi { get; set; }

        public Sedacka(Sedacka predchozi)
        {
            Predchozi = predchozi;
        }
    }
}
