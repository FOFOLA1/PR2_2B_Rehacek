using System.Text.RegularExpressions;

namespace Projekt_prochazeni_stromu
{
    public class ListMenu : Menu
    {
        private List<Option> pickedSalesmansOption;
        private BrowserMenu browserMenu;
        protected override int OptionsCount { get => pickedSalesmansOption.Count + 4; }

        public ListMenu(BrowserMenu browser) :
            base
            (
                new List<Option>
                {
                        new Option("Založit"),
                        new Option("Načíst"),
                        new Option("Uložit"),
                        new Option("Přejít na prohlížeč")
                }
            )
        {
            browserMenu = browser;
        }

        public override void Display()
        {
            loadPickedSalesmans();
            Option selected = GetSelectedOption();
            Console.Clear();
            for (int i = 0; i < 4; i++)
            {
                Utils.WriteOption(Options[i], selected, ConsoleColor.DarkCyan);
                if (i != 3)
                    Utils.Write(" | ");
            }
            Utils.Write("\n-----------------------------------------------");
            if (_fileName == null)
            {
                Utils.Write("\nNení vytvořen/nahrán žádný seznam!");
                return;
            }

            Utils.Write($"\nSeznam: {_fileName}\n{new String('-', 7 + _fileName.Length)}\n\n");

            if (pickedSalesmansOption != null)
                for (int i = 0; i < _data.Count; i++)
                {
                    Utils.WriteOption(pickedSalesmansOption[i], selected);
                    Utils.Write("\n");
                }
        }

        public override Option GetSelectedOption()
        {
            if (SelectedOption < 4)
                return Options[SelectedOption];
            return pickedSalesmansOption[SelectedOption - 4];
        }

        public override bool Invoke()
        {
            switch (SelectedOption)
            {
                case 0: // přejít na založení
                    if (_fileName != null)
                    {
                        Utils.WriteMsg("Soubor je již vybrán");
                        break;
                    }
                    createList();
                    break;
                case 1: // přejít na nahrání
                    if (_fileName != null)
                    {
                        Utils.WriteMsg("Soubor je již vybrán");
                        break;
                    }
                    loadList(Utils.SelectFile("*.txt"));
                    break;
                case 2: // uložení
                    saveFile();
                    Utils.WriteMsg("Uloženo");
                    break;
                case 3: // přejít na prohlížeč
                    SelectedOption = 0;
                    return true;
                default:  // položky seznamu
                    browserMenu.RewriteSalesmanWithHist(GetSelectedOption().Salesman);
                    SelectedOption = 0;
                    return true;
            }
            return false;
        }

        private void createList()
        {
            string inp = null;
            do
            {
                Console.Clear();
                if (inp == "") break;
                if (inp != null) Utils.Write("Tento soubor již existuje nebo název není validní: " + inp + "\nPokud se chceš vrátit zpět a načíst již existující soubor, stiskni Enter." + "\n\n", ConsoleColor.Red);
                Utils.Write($"Zadej název souboru (bez přípony), do kterého později bude uložen seznam:\n > ");
                inp = Console.ReadLine();
            } while (File.Exists(inp + ".txt"));
            _fileName = inp + ".txt";
            _data = new List<Salesman>();
        }

        private void loadList(string fileName)
        {
            using (StreamReader fileread = new StreamReader(fileName))
            {
                string pattern = @"^(?<Name>\w+)\s(?<Surname>\w+):\sSales\s(?<Sales>\d+)$";
                string[] file = fileread.ReadToEnd().Split("\r\n");
                Console.WriteLine(file);
                if (_data == null) _data = new List<Salesman>();
                foreach (string data in file)
                {
                    Match match = Regex.Match(data, pattern);
                    if (match.Success)
                    {
                        string name = match.Groups["Name"].Value;
                        string surname = match.Groups["Surname"].Value;
                        int sales = int.Parse(match.Groups["Sales"].Value);

                        _data.Add(Salesman.FindSalesman(browserMenu.Root, name, surname, sales));
                    }
                    else
                    {
                        Console.WriteLine("No match found.");
                    }
                }
            }
            _fileName = fileName;
        }

        private void loadPickedSalesmans()
        {
            pickedSalesmansOption = new List<Option>();
            if (_data != null)
                foreach (Salesman sal in _data)
                {
                    pickedSalesmansOption.Add(new Option(sal));
                }
        }

        private void saveFile()
        {
            if (File.Exists(_fileName)) File.Delete(_fileName);
            using (StreamWriter filewrite = new StreamWriter(_fileName))
            {
                foreach (Salesman data in _data)
                {
                    filewrite.WriteLine($"{data.Name} {data.Surname}: Sales {data.Sales}");
                }
                filewrite.Close();
            }
        }
    }
}
