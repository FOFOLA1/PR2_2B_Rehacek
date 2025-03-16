namespace _02_OOP2_03_Zamestnanci
{
    internal class StalyZamestnanec : Zamestnanec
    {
        private int _mzda;


        public StalyZamestnanec(string jmeno, string prijmeni, int mzda) : base(jmeno, prijmeni)
        {
            _mzda = mzda;
        }

        public override int Mzda()
        {
            return _mzda;
        }
    }
}
