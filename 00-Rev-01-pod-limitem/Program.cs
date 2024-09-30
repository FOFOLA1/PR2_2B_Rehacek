namespace _00_Rev_01_pod_limitem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] cisla = { 1.3, 2.2, -1.5, 1.4, -2.7 };
            Console.WriteLine(PrumerPodLimitem(cisla, 1.1));
            Console.WriteLine(PrumerPodLimitem(cisla, -2));
        }

        static double PrumerPodLimitem(double[] cisla, double limit)
        {
            double soucet = 0;
            int pocet = 0;
            foreach (double cislo in cisla)
            {
                if (cislo < limit)
                {
                    soucet += cislo;
                    pocet++;
                }
            }
            if (soucet/pocet == double.NaN) return 0;
            else return soucet/pocet;
            
        }
    }
}
