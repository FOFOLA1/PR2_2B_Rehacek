namespace _04_OOP3_030_Voziky
{
    internal class Vozik
    {
        static int NextId = 1;

        private int ID { get; set; }
        public int Opotrebeni { get; private set; }

        public Vozik()
        {
            Opotrebeni = 0;
            ID = NextId++;
        }

        public void Pouzij(int cas)
        {
            Opotrebeni += cas;
        }

        override public string ToString()
        {
            return $"Vozik {ID} s opotřebením {Opotrebeni}";
        }
    }
}
