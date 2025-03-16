namespace _02_OOP2_06_cv_Utvary
{
    internal class Trojuhelnik : IUtvar
    {
        public double StranaA { get; private set; }
        public double StranaB { get; private set; }
        public double StranaC { get; private set; }

        public string Nazev => "Trojuhelník";

        public Trojuhelnik(double stranaA, double stranaB, double stranaC)
        {
            StranaA = stranaA;
            StranaB = stranaB;
            StranaC = stranaC;
        }

        public double GetObvod() => StranaA + StranaB + StranaC;
        public double GetObsah() => 1;

        public override string? ToString() => $"Trojúhelník o stranách {StranaA}, {StranaB} a {StranaC} cm";
    }
}
