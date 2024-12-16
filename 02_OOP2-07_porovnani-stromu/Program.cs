namespace _02_OOP2_07_porovnani_stromu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Strom strom = new Str
        }


        class Strom : IComparable
        {
            public string Druh { get; set; }
            public double Vyska { get; set; }

            public Strom(string druh, double vyska)
            {
                Druh = druh;
                Vyska = vyska;
            }

            public int CompareTo(object? obj)
            {
                Strom druhyStrom = obj as Strom;

                if (druhyStrom == null) throw new ArgumentException();
                //else if (druhyStrom.Vyska > this.Vyska) return -1;
                //else return 1;

                return this.Vyska.CompareTo(druhyStrom.Vyska);

            }
        }
    }
}
