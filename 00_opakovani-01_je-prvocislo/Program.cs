

using System.Runtime.InteropServices;

namespace _00_opakovani_01_je_prvocislo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                int num = ReadInput();
                bool prime = IsPrimeNum(num);
                PrintResult(num, prime);
            }
        }

        private static void PrintResult(int num, bool prime)
        {
            if (prime) Console.WriteLine($"Číslo {num} je prvočíslo.\n");
            else Console.WriteLine($"Číslo {num} není prvočíslem.\n");
        }

        private static bool IsPrimeNum(int num)
        {
            if (num == 1) return false;
            int halfnum = num / 2;
            for (int i = 2; i <= halfnum; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static int ReadInput()
        {
            int num = 0;
            do
            {
                Console.Write("Zadej číslo: ");
            } while (!int.TryParse(Console.ReadLine(), out num));
            return num;
        }
    }
}
