using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_04_DopravniProstredky
{
    internal sealed class Flotila
    {
        List<DopravniProstredek> dopravniProstredeks;

        public Flotila()
        {
            this.dopravniProstredeks = new List<DopravniProstredek> ();
        }

        public int Velikost { get => dopravniProstredeks.Count; }
        public int Kapacita
        {
            get
            {
                int count = 0;
                foreach (DopravniProstredek dp in dopravniProstredeks)
                {
                    count += dp.PocetMist;
                }
                return count;
            }
        }

        public List<TypPohonu> Pohony
        {
            get
            {
                List<TypPohonu> typPohonus = new List<TypPohonu>();
                foreach (DopravniProstredek dp in dopravniProstredeks)
                {
                    typPohonus.Append(dp.Pohon);
                }
                return typPohonus.Distinct().ToList();
            }
        }

        public void Natankuj()
        {
            foreach (DopravniProstredek dp in dopravniProstredeks)
            {
                dp.Natankuj();
            }
        }

        public void OdvezRychle(int pocetLidi) { }

        public void PridejVozidlo(DopravniProstredek d)
        {
            dopravniProstredeks.Add(d);
        }
        public void OdeberVozidlo(DopravniProstredek d)
        {
            dopravniProstredeks.Remove(d);
        }
    }
}
