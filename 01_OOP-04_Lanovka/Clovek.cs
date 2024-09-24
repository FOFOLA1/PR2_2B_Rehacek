using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_04_Lanovka
{
    internal class Clovek
    {
        private string jmeno;
        private double hmotnost;

        public string Jmeno
        {
            get { return jmeno; }
            private set { jmeno = value; }
        }

        public double Hmotnost
        {
            get { return hmotnost; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                hmotnost = value;
            }
        }

        public Clovek(string jmeno, double hmotnost)
        {
            Jmeno = jmeno;
            Hmotnost = hmotnost;
        }

    }
}
