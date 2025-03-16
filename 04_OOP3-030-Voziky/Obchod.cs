﻿namespace _04_OOP3_030_Voziky
{
    internal class Obchod
    {
        private List<Vozik> StojanVoziku;
        private List<Zakaznik> Zakaznici;


        public Obchod(int pocetVoziku)
        {
            StojanVoziku = new List<Vozik>();
            Zakaznici = new List<Zakaznik>();
            for (int i = 0; i < pocetVoziku; i++)
            {
                StojanVoziku.Add(new Vozik());
            }
        }

        public void VezmiVozik(int cas)
        {
            int posledni = StojanVoziku.Count - 1;
            if (posledni == -1)
            {
                Console.WriteLine("\n\n\nVe stojanu nejsou žádné vozíky.\n\n\n");
                return;
            }
            StojanVoziku[posledni].Pouzij(cas);
            Zakaznik zakaznik = new Zakaznik(cas, StojanVoziku[posledni]);
            Zakaznici.Add(zakaznik);
            StojanVoziku.RemoveAt(posledni);
        }

        public void Nakup()
        {
            for (int i = 0; i < Zakaznici.Count; i++)
            {
                Zakaznici[i].Nakup();
                if (Zakaznici[i].ZbyvajiciCas == 0)
                {
                    StojanVoziku.Add(Zakaznici[i].Vozik);
                    Zakaznici.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Vypis()
        {
            Console.WriteLine("Vozíky ve stojanu:");
            foreach (Vozik vozik in StojanVoziku)
            {
                Console.WriteLine(vozik);
            }
            Console.WriteLine("\nZákazníci:");
            foreach (Zakaznik zakaznik in Zakaznici)
            {
                Console.WriteLine(zakaznik.Vozik);
            }
        }

        public int PocetVolnychVoziku()
        {
            return StojanVoziku.Count;
        }

        public int PocetZakazniku()
        {
            return Zakaznici.Count;
        }

        public void srovnej()
        {
            StojanVoziku = StojanVoziku.OrderBy(x => x.Opotrebeni).ToList();
        }
    }
}
