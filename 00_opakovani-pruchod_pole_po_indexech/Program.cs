namespace _00_opakovani_pruchod_pole_po_indexech
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //int[] array = { -5, 3, 2, -3, 7, 0, 12, -1 };
            //int[] array = {1, 3, 2, 1, 7, 0, 12, -1};
            //int[] array = {-1};
            int[] array = {};

            int sumS = 0,  sumL = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i%2 == 0)
                {
                    sumS += array[i];
                } else
                {
                    sumL += array[i];
                }
            }
            Console.WriteLine(sumS);
            Console.WriteLine(sumL);
        }
    }
}
