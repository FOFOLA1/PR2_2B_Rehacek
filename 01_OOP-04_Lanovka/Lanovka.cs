namespace _01_OOP_04_Lanovka
{
    internal class Lanovka
    {
        private int delka;
        private double nosnost;
        public double Zatizeni;
        public bool JeVolnoDole;
        public bool JeVolnoNahore;
        private Clovek[] lanovka;

        public Lanovka(int Delka, double Nostnost)
        {
            JeVolnoDole = true;
            JeVolnoNahore = true;
            nosnost = Nostnost;
            Zatizeni = 0;
            lanovka = new Clovek[Delka];
        }

        public bool Nastup(Clovek clovek)
        {
            if (lanovka[0] != null || nosnost - Zatizeni - clovek.Hmotnost < 0) return false;
            lanovka[0] = clovek;
            Zatizeni += clovek.Hmotnost;
            return true;
        }

        public Clovek Vystup()
        {
            Clovek vystup = lanovka[lanovka.Length - 1];
            lanovka[lanovka.Length - 1] = null;
            return vystup;
        }

        public void Jed()
        {
            if (lanovka[lanovka.Length - 1] != null) throw new ArgumentOutOfRangeException();
            for (int i = lanovka.Length - 1; i > 0; i--)
            {
                lanovka[i] = lanovka[i - 1];
            }
            lanovka[0] = null;
        }



    }

}
