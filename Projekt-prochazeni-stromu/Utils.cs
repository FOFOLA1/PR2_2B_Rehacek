namespace Projekt_prochazeni_stromu
{
    public class Utils
    {
        public static void Write(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(message);
        }

        public static void WriteSelected(string message)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(message);
        }

        public static void WriteOption(Option option, Option SelectedOption, ConsoleColor color = ConsoleColor.White)
        {
            if (option.ID == SelectedOption.ID)
                WriteSelected(option.DisplayText);
            else
                Write(option.DisplayText, color);

        }

        public static void WriteMsg(string msg)
        {
            Console.Clear();
            Write(msg);
            Console.ReadKey();
        }

        public static string ListSubmenu(string[] list, string header = null)
        {
            int sel = 0;
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                if (header != null) Write(header);
                for (int i = 0; i < list.Length; i++)
                {
                    if (i == sel) Write("\n > ");
                    else Write("\n  ");
                    Write(list[i].ToString());
                }
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.RightArrow:
                        sel++;
                        if (sel == list.Length) sel = 0;
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.LeftArrow:
                        sel--;
                        if (sel < 0) sel = list.Length - 1;
                        break;
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        return list[sel];
                    case ConsoleKey.Escape:
                    case ConsoleKey.X:
                        Environment.Exit(0);
                        break;
                }
            } while (key.Key != ConsoleKey.X || key.Key != ConsoleKey.Escape);
            return "";
        }

        public static string SelectFile(string filterFormat, string header = null)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), filterFormat, SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                Console.Clear();
                Write("Nic k načtení. Stiskni klávesu pro vrácení.");
                Console.ReadKey();
                return null;
            }
            files = files.Select(x => x.Split("\\").Last()).ToArray();
            return ListSubmenu(files, header);
        }
    }
}
