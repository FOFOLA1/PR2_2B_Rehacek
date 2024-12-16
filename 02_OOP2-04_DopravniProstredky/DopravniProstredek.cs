using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _02_OOP2_04_DopravniProstredky
{
    enum TypPohonu { Manualni, SpalovaciMotor, Elektromotor }

    internal abstract class DopravniProstredek
    {
        public string Nazev {  get; set; }
        public TypPohonu Pohon { get; set; }
        public double MaxRychlost { get; set; }
        public int PocetMist {  get; set; }

        protected DopravniProstredek(string nazev, TypPohonu pohon, double maxRychlost, int pocetMist)
        {
            Nazev = nazev;
            Pohon = pohon;
            MaxRychlost = maxRychlost;
            PocetMist = pocetMist;
        }

        public abstract void Natankuj();

        public override string ToString()
        {
            return $"{Nazev} s pohonem na {Pohon} s maximální rychlostí {MaxRychlost} má {PocetMist}.";
        }
    }
}
