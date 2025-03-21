﻿namespace _02_OOP2_03_Zamestnanci
{
    internal class Brigadnik : Zamestnanec
    {

        public int Odpracovano { get; set; }
        public int HodinovaMzda { get; set; }

        public override int Mzda()
        {
            return Odpracovano * HodinovaMzda;
        }

        public Brigadnik(string jmeno, string prijmeni) : base(jmeno, prijmeni)
        {
        }
    }
}
