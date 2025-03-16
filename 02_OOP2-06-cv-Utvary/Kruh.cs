namespace _02_OOP2_06_cv_Utvary
{
    internal class Kruh : IUtvar
    {
        public double Polomer { get; private set; }
        public string Nazev => "Kruh";

        public Kruh(double strana)
        {
            Polomer = strana;
        }

        public double GetObvod() => 2 * Polomer * double.Pi;
        public double GetObsah() => Polomer * Polomer * double.Pi;

        public override string? ToString() => $"Kruh o poloměru {Polomer} cm";
    }
}
