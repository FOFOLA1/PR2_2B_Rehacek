namespace _02_OOP2_10_Obchodnici
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename = "smalltree.json";
            //string filename = "largetree.json";
            Salesman boss = Salesman.DeserializeTree(File.ReadAllText(filename));

            //DisplaySalesmenTree(boss);
            //FindSalesmanStack(boss, "Brown");
            //FindSalesmanQueue(boss, "Brown");
            //GetTotalSalesQueue(boss);
            //Console.WriteLine(GetTotalSalesRecursive(boss));
            Console.WriteLine(GetSalesmanStack(boss, "Jones"));

        }
        static void DisplaySalesmenTree(Salesman node, string indent = "")
        {
            Console.WriteLine($"{indent}{node.Name} {node.Surname} - Sales: {node.Sales}");

            foreach (var subordinate in node.Subordinates)
            {
                DisplaySalesmenTree(subordinate, indent + "    ");
            }
        }

        static void FindSalesmanRecursive(Salesman parentNode, string surename)
        {
            if (parentNode.Surname == surename)
            {
                Console.WriteLine($"{parentNode.Name} {parentNode.Surname} - Sales: {parentNode.Sales}");
            }

            foreach (var subordinate in parentNode.Subordinates)
            {
                FindSalesmanRecursive(subordinate, surename);
            }
        }
        static void FindSalesmanStack(Salesman parentNode, string surename)
        {
            Stack<Salesman> toBeVisited = new Stack<Salesman>();
            toBeVisited.Push(parentNode);
            while (toBeVisited.Count > 0)
            {
                Salesman current = toBeVisited.Pop();
                if (current.Surname == surename)
                {
                    Console.WriteLine($"{current.Name} {current.Surname} - Sales: {current.Sales}");

                }
                foreach (var subordinate in current.Subordinates)
                {
                    toBeVisited.Push(subordinate);
                }
            }
        }

        static void FindSalesmanQueue(Salesman parentNode, string surename)
        {
            Queue<Salesman> toBeVisited = new Queue<Salesman>();
            toBeVisited.Enqueue(parentNode);
            while (toBeVisited.Count > 0)
            {
                Salesman current = toBeVisited.Dequeue();
                if (current.Surname == surename)
                {
                    Console.WriteLine($"{current.Name} {current.Surname} - Sales: {current.Sales}");
                }
                foreach (var subordinate in current.Subordinates)
                {
                    toBeVisited.Enqueue(subordinate);
                }
            }
        }

        static void GetTotalSalesQueue(Salesman parentNode)
        {
            Queue<Salesman> toBeVisited = new Queue<Salesman>();
            toBeVisited.Enqueue(parentNode);
            int totalSales = 0;
            while (toBeVisited.Count > 0)
            {
                Salesman current = toBeVisited.Dequeue();
                totalSales += current.Sales;
                foreach (var subordinate in current.Subordinates)
                {
                    toBeVisited.Enqueue(subordinate);
                }
            }
            Console.WriteLine($"Total sales: {totalSales}");
        }

        static int GetTotalSalesRecursive(Salesman parentNode)
        {
            int total = parentNode.Sales;
            foreach (var subordinate in parentNode.Subordinates)
            {
                total += GetTotalSalesRecursive(subordinate);
            }
            return total;
        }

        static Salesman[] GetSalesmanStack(Salesman parentNode, string surename)
        {
            List<Salesman> found = new List<Salesman>();
            Stack<Salesman> toBeVisited = new Stack<Salesman>();
            toBeVisited.Push(parentNode);

            while (toBeVisited.Count > 0)
            {
                Salesman current = toBeVisited.Pop();
                if (current.Surname == surename)
                {
                    found.Add(current);
                }
                foreach (Salesman subordinate in current.Subordinates)
                {
                    toBeVisited.Push(subordinate);
                }
            }
            return found.ToArray();
        }

    }
}
