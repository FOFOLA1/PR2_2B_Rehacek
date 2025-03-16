namespace _02_OOP2_03_Zamestnanci
{
    internal abstract class Zamestnanec
    {
        public string Jmeno { get; private set; }
        public string Prijmeni { get; private set; }

        protected Zamestnanec(string jmeno, string prijmeni)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
        }

        public abstract int Mzda();
    }
}
