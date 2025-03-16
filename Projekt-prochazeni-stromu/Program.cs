namespace Projekt_prochazeni_stromu
{
    class Program
    {
        static void Main(string[] args)
        {
            new App(Utils.SelectFile("*.json", "Vyber soubor s daty:"));
        }
    }
}
