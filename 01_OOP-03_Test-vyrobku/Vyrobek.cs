namespace _01_OOP_03_Test_vyrobku
{
    internal class Vyrobek
    {
        private double rozmer;

        public double Rozmer
        {
            get { return rozmer; }
            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException("value"); }
                rozmer = value;
            }
        }

        public Vyrobek(double rozmer)
        {
            Rozmer = rozmer;
        }

    }
}
