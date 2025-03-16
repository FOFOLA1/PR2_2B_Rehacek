namespace Projekt_prochazeni_stromu
{
    public class Option
    {
        private static int lastID = 0;
        public Salesman Salesman { get; set; }
        public int ID { get; private set; }
        private string _name;
        public string DisplayText
        {
            get
            {
                if (Salesman == null) return _name;
                else return Salesman.Name + " " + Salesman.Surname;
            }
            set { _name = value; }
        }

        public Option(string displayText)
        {
            ID = ++lastID;
            _name = displayText;
        }

        public Option(Salesman salesman)
        {
            ID = ++lastID;
            Salesman = salesman;
        }
    }
}
