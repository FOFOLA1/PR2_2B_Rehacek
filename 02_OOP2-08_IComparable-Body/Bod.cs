using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_08_IComparable_Body
{
    class Bod : IComparable
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Bod(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return string.Format("Bod [{0};{1}]", this.X, this.Y);
        }

        public double VzdalenostOdStredu()
        {
            return Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) throw new ArgumentNullException();
            Bod druhy = obj as Bod;
            return this.VzdalenostOdStredu().CompareTo(druhy.VzdalenostOdStredu());
        }
    }
}
