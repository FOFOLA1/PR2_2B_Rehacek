namespace _04_OOP3_030_Voziky
{
    internal class Program
    {
        static Random random = new Random(123456);
        public static void Main(string[] args)
        {

            int pocetVoziku = 50;
            int casStart = 0;
            int casKonec = 12 * 60;
            int maxZakaznikuZaMinutu = 3;
            int minNakup = 5;
            int maxNakup = 45;


            Obchod obchod = new Obchod(pocetVoziku);

            for (int i = casStart; i <= casKonec; i++)
            {
                int pocetZakazniku = random.Next(maxZakaznikuZaMinutu + 1);
                for (int j = 0; j < pocetZakazniku; j++)
                {
                    obchod.VezmiVozik(random.Next(minNakup, maxNakup + 1));
                }
                obchod.Nakup();

                //Console.WriteLine("\n------------------------------------");
                //Console.WriteLine($"Stav po {i}. minute:");
                //obchod.Vypis();
            }

            while (obchod.PocetZakazniku() > 0)
            {
                obchod.Nakup();

                //Console.WriteLine("\n------------------------------------");
                //obchod.Vypis();
            }

            Console.WriteLine("\n------------------------------------");
            obchod.srovnej();
            obchod.Vypis();

        }
    }
}
