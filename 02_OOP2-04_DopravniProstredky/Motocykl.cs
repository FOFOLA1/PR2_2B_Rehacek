namespace _02_OOP2_04_DopravniProstredky
{
    internal sealed class Motocykl : DopravniProstredek
    {
        private double _maxRychlost;
        public Motocykl(double MaxRychlost) : base("Motocykl", TypPohonu.SpalovaciMotor, MaxRychlost, 2)
        {
            _maxRychlost = MaxRychlost;
        }

        public override void Natankuj()
        {
            Console.WriteLine("Plním nádrž benzínem");
        }
    }
}
