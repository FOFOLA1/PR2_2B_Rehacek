namespace _02_OOP2_04_DopravniProstredky
{
    internal class Bicykl : DopravniProstredek
    {
        public Bicykl(double maxRychlost) : base("Bicykl", TypPohonu.Manualni, maxRychlost, 1)
        {
        }

        public override void Natankuj()
        {
            Console.WriteLine("jdu na svačinu");
        }
    }
}
