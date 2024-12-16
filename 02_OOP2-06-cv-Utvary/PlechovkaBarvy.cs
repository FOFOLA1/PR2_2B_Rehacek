using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_06_cv_Utvary
{
    internal class PlechovkaBarvy : IUtvar
    {
        public double Objem { get; private set; }
        public double Vydatnost { get; private set; }

        public PlechovkaBarvy(double objem, double vydatnost)
        {
            Objem = objem;
            Vydatnost = vydatnost;
        }

        public bool Obarvi(IUtvar utvar)
        {
            double spotreba = utvar.GetObsah() * Vydatnost;

            if (spotreba <= Objem)
            {
                Objem -= spotreba;
                return true;
            } else return false;
        }

        public override string ToString() => $"Plechovka barvy, zbývá ještě {Objem} litrů barvy.";
    }
}
