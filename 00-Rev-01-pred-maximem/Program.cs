namespace _00_Rev_01_pred_maximem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] cisla = vstup();
            Console.WriteLine(GetCisloPredNejvetsim(cisla));
        }

        static int[] vstup()
        {
            string input;
            int[] cisla = { };
            int i = -1;
            try
            {
                do
                {
                    input = Console.ReadLine();
                    i++;
                    cisla.Append(1);
                } while (input != "dost" || int.TryParse(input, out cisla[i]));
                return cisla;


            }
            catch (System.IndexOutOfRangeException)
            {
                return cisla;
            }
            
        }

        static string GetCisloPredNejvetsim(int[] cisla)
        {
            if (cisla.Length == 0) return "Nebylo zadáno žádné číslo";
            int i_nejvetsi = 0;
            int cislo_nejvetsi = cisla[0];

            for (int i = 1; i < cisla.Length; i++)
            {
                if (cisla[i] > cislo_nejvetsi)
                {
                    i_nejvetsi = i;
                    cislo_nejvetsi = cisla[i];
                }
            }
            if (i_nejvetsi == 0) return cisla[cisla.Length - 1].ToString();
            else return cisla[i_nejvetsi-1].ToString();
        }
    }
}
