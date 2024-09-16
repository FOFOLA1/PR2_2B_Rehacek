namespace _01_OOP_02_Tachometr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tachometr tachometr = new Tachometr();
                Console.WriteLine(tachometr.Stav);
                tachometr.Ujed(10);
                Console.WriteLine(tachometr.Stav);
                tachometr.Ujed(-20);
                Console.WriteLine(tachometr.Stav);
            } catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("chyba");
            }
            
        }
    }
}
