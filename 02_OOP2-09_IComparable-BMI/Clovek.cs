using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_09_IComparable_BMI
{
    class Clovek : IComparable
    {
        public string Jmeno { get; private set; }
        public double Hmotnost { get; private set; }
        public double Vyska { get; private set; }

        public Clovek(string jmeno, double hmotnost, double vyska)
        {
            if (jmeno.Length < 1) throw new ArgumentException("Příliš krátké jméno");
            if (hmotnost <= 0) throw new ArgumentOutOfRangeException();
            if (vyska <= 0) throw new ArgumentOutOfRangeException();

            this.Jmeno = jmeno;
            this.Hmotnost = hmotnost;
            this.Vyska = vyska;
        }

        public double BMI()
        {
            return this.Hmotnost / ((this.Vyska/100) * (this.Vyska / 100));
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            Clovek druhy = obj as Clovek;
            //return this.BMI().CompareTo(druhy.BMI());
            return this.BMI().CompareTo(22);
        }

        public override string ToString()
        {
            return $"{Jmeno}, {Hmotnost}, {Vyska}: {BMI()}"; 
        }
    }
}
