namespace _02_OOP2_06_cv_Utvary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ctverec ctverec = new Ctverec(3);
            Trojuhelnik trojuhelnik = new Trojuhelnik(2, 3, 4);
            Kruh kruh = new Kruh(3);
            Ctverec ctverec1 = new Ctverec(1);

            IUtvar[] utvaty = [ctverec, trojuhelnik, kruh, ctverec];

            foreach (IUtvar utvar in utvaty)
            {
                Console.WriteLine(utvar.Nazev);
                Console.WriteLine(utvar);
            }


        }
    }
}
