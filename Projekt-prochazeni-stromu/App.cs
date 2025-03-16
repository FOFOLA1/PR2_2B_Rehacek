namespace Projekt_prochazeni_stromu
{
    public class App
    {
        private BrowserMenu browserMenu;
        private ListMenu listMenu;
        public Menu DisplayedMenu;
        private ConsoleKeyInfo keyInfo;
        private Salesman root;

        public App(string path)
        {
            root = Salesman.DeserializeTree(File.ReadAllText(path));
            browserMenu = new BrowserMenu(root);
            listMenu = new ListMenu(browserMenu);

            DisplayedMenu = browserMenu;
            browserMenu.Display();

            do
            {

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.DownArrow:
                        DisplayedMenu.NextOption();
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.UpArrow:
                        DisplayedMenu.PreviousOption();
                        break;
                    case ConsoleKey.Spacebar:
                        if (DisplayedMenu.Invoke())
                        {
                            if (DisplayedMenu.GetType().IsInstanceOfType(browserMenu)) DisplayedMenu = listMenu;
                            else DisplayedMenu = browserMenu;
                        }
                        break;
                }
                DisplayedMenu.Display();
            }
            while (keyInfo.Key != ConsoleKey.X);
        }
    }
}
