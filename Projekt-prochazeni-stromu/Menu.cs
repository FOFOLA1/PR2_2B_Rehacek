namespace Projekt_prochazeni_stromu
{
    public abstract class Menu
    {
        protected virtual int OptionsCount { get; set; }
        protected int SelectedOption { get; set; }
        protected List<Option> Options { get; set; }
        protected static List<Salesman> _data;
        protected static string _fileName;
        protected Menu(List<Option> options)
        {
            SelectedOption = 0;
            OptionsCount = options.Count;
            Options = options;
        }

        public abstract void Display();
        public virtual void NextOption()
        {
            SelectedOption++;
            if (SelectedOption == OptionsCount) SelectedOption = 0;
        }

        public virtual void PreviousOption()
        {
            SelectedOption--;
            if (SelectedOption < 0) SelectedOption = OptionsCount - 1;
        }
        public abstract bool Invoke();
        public abstract Option GetSelectedOption();
    }
}
