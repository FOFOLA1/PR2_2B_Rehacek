namespace _02_OOP2_03_Zamestnanci
{
    internal class Firma
    {


        private List<Zamestnanec> _personal = new List<Zamestnanec>();

        public void Zamestnej(Zamestnanec z)
        {
            if (!_personal.Contains(z))
                _personal.Add(z);
        }

        public void Propust(Zamestnanec z)
        {
            _personal.Remove(z);
        }

        public void Vyplata()
        {
            int celkem = 0;
            foreach (Zamestnanec z in _personal)
            {
                int mzda = z.Mzda();
                celkem += mzda;
                Console.WriteLine($"{z.Prijmeni}, {z.Jmeno}: {mzda} Kč");
            }
            Console.WriteLine(new string('-', 15));
            Console.WriteLine($"Celkem: {celkem} Kč");
        }
    }
}
