namespace Projekt_prochazeni_stromu
{
    public class BrowserMenu : Menu
    {
        public List<Option> SuboordinatesOptions { get; private set; }
        public Option EmployerOption { get; private set; }
        public Salesman SelectedSalesman { get; private set; }
        private static Option addOption = new Option("Přidat");
        private static Option removeOption = new Option("Odebrat");
        private static Option invisibleOption = new Option("");
        public bool IsSelectedOnList
        {
            get
            {
                if (Salesman.FindIndexInList(_data, SelectedSalesman) < 0)
                    return false;
                return true;
            }
        }
        public List<Salesman> History { get; private set; }
        public Salesman Root { get; private set; }



        public BrowserMenu(Salesman root) :
            base
            (
                new List<Option>
                {
                    new Option("Přejít nahoru"),
                    new Option("Přejít na seznam"),
                    invisibleOption
                }
            )
        {
            Root = root;
            RewriteSelectedSalesman(Root);

            EmployerOption = new Option(new Salesman("", "Není", 0, -1));
            History = new List<Salesman>() { EmployerOption.Salesman };
        }

        public void RewriteSelectedSalesman(Salesman newSalesman)
        {
            SelectedSalesman = newSalesman;
            SuboordinatesOptions = new List<Option>();
            for (int i = 0; i < SelectedSalesman.Subordinates.Count; i++)
            {
                SuboordinatesOptions.Add(new Option(SelectedSalesman.Subordinates[i]));
            }
            OptionsCount = 4 + SuboordinatesOptions.Count;
        }


        public override void Display()
        {
            Option selected = GetSelectedOption();
            Console.Clear();
            for (int i = 0; i < 2; i++)
            {
                Utils.WriteOption(Options[i], selected, ConsoleColor.DarkCyan);
                if (i != 1)
                    Utils.Write(" | ");
            }
            Utils.Write($"\n--------------------------------\n\nObchodník: {SelectedSalesman.Name} {SelectedSalesman.Surname}    ");
            if (_data == null) Utils.WriteOption(Options[2], selected);
            else if (IsSelectedOnList)
            {
                Options[2] = removeOption;
                if (SelectedOption == 2) Utils.WriteSelected(Options[2].DisplayText);
                else Utils.Write(Options[2].DisplayText, ConsoleColor.Red);
            }
            else
            {
                Options[2] = addOption;
                if (SelectedOption == 2) Utils.WriteSelected(Options[2].DisplayText);
                else Utils.Write(Options[2].DisplayText, ConsoleColor.Green);
            }

            Utils.Write($"\n{new string('-', SelectedSalesman.Name.Length + SelectedSalesman.Surname.Length + 12)}\nPřímé prodeje: {SelectedSalesman.Sales} $\nCelkové prodeje sítě: {Salesman.GetTotalSales(SelectedSalesman)} $\n\nNadřízený: ");
            Utils.WriteOption(EmployerOption, selected);
            Utils.Write("\n\nPodřízení: ");
            if (SuboordinatesOptions.Count == 0) Utils.Write("Nemá\n");
            else for (int i = 0; i < SuboordinatesOptions.Count; i++)
                {
                    Utils.WriteOption(SuboordinatesOptions[i], selected);
                    if (i == SuboordinatesOptions.Count - 1) Utils.Write("\n");
                    else Utils.Write("\n           ");
                }
        }

        public override Option GetSelectedOption()
        {
            switch (SelectedOption)
            {
                case 0:
                case 1:
                case 2:
                    return Options[SelectedOption];
                case 3:
                    return EmployerOption;
                default:
                    return SuboordinatesOptions[SelectedOption - 4];
            }
        }

        public override void NextOption()
        {
            SelectedOption++;
            if (SelectedOption == 2 && Options[2].DisplayText == "") SelectedOption++;
            if (SelectedOption == OptionsCount) SelectedOption = 0;

        }

        public override void PreviousOption()
        {
            SelectedOption--;
            if (SelectedOption == 2 && Options[2].DisplayText == "") SelectedOption--;
            if (SelectedOption < 0) SelectedOption = OptionsCount - 1;
        }

        public override bool Invoke()
        {
            switch (SelectedOption)
            {
                case 0: // přejít nahoru
                    goToRoot();
                    break;
                case 1: // přejít na seznam
                    SelectedOption = 0;
                    return true;
                case 2: // přidat/odebrat
                    addOrDelete();
                    break;
                case 3: // přejít na zaměstnavatele
                    if (History.Count == 1) break;
                    goToEmployer();
                    break;
                default: // přejít na suboordinate
                    goToSuboordinate();
                    break;
            }
            return false;
        }

        private void goToRoot()
        {
            RewriteSelectedSalesman(Root);

            EmployerOption = new Option(new Salesman("", "Není", 0, -1));
            History = new List<Salesman>() { EmployerOption.Salesman };
        }

        private void addOrDelete()
        {
            if (Options[2] == addOption)
            {
                _data.Add(SelectedSalesman);
            }
            else if (Options[2] == removeOption)
            {
                _data.RemoveAt(Salesman.FindIndexInList(_data, SelectedSalesman));
            }
        }

        private void goToEmployer()
        {
            RewriteSelectedSalesman(History[History.Count - 1]);
            EmployerOption.Salesman = History[History.Count - 2];
            History.RemoveAt(History.Count - 1);
            SelectedOption = 0;
        }

        private void goToSuboordinate()
        {
            History.Add(SelectedSalesman);
            EmployerOption.Salesman = SelectedSalesman;
            RewriteSelectedSalesman(SuboordinatesOptions[SelectedOption - 4].Salesman);
            SelectedOption = 0;
        }

        public void RewriteSalesmanWithHist(Salesman sal)
        {
            List<Salesman> list = Salesman.FindPathToSalesman(Root, sal.Name, sal.Surname, sal.Sales);
            if (list.Count == 1)
            {
                RewriteSelectedSalesman(Root);

                EmployerOption = new Option(new Salesman("", "Není", 0, -1));
                History = new List<Salesman>() { EmployerOption.Salesman };
            }
            else
            {
                list.Insert(0, History[0]);
                Salesman temp = list.Last();
                list.RemoveAt(list.Count - 1);
                EmployerOption.Salesman = list.Last();
                list.RemoveAt(list.Count - 1);
                RewriteSelectedSalesman(temp);
            }

        }
    }
}
