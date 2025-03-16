namespace _01_OOP_01_Role
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Role role = new Role("cervena", 350);
            Console.WriteLine(role);
        }

        class Role
        {
            private string barva;
            private int delka;

            public string Barva { get => barva; set => barva = value; } // Zkratka prop, propfull
            public int Delka
            {
                get => delka;
                set
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    delka = value;
                }
            }

            public Role(string barva, int delka)
            {
                Barva = barva;
                Delka = delka;
            }

            public override string ToString()
            {
                return $"Role papíru, barva {Barva}, zbývá {Delka} cm";
            }


        }
    }
}
