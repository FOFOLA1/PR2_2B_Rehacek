namespace _02_OOP2_02_BankovniUcet
{
    internal class SporiciUcet : Ucet
    {
        public double UrokovaMira { get; set; }

        public void Urokuj()
        {
            Stav *= UrokovaMira / 100 + 1;
        }

    }
}
