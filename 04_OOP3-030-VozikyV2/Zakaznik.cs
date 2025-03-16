namespace _04_OOP3_030_Voziky
{
    internal class Zakaznik
    {
        public int ZbyvajiciCas { get; private set; }
        public Vozik Vozik { get; private set; }

        public Zakaznik(int casNakupu, Vozik vozik)
        {
            ZbyvajiciCas = casNakupu;
            Vozik = vozik;
        }

        public void Nakup()
        {
            ZbyvajiciCas--;
        }
    }
}
