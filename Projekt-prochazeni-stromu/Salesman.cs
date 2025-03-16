using System.Text.Json;

public class Salesman
{
    static int NextId = 1;

    private int ID { get; set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public int Sales { get; private set; }
    public List<Salesman> Subordinates { get; private set; }

    public Salesman(string surname, string name, int sales, int id = 0)
    {
        Name = name;
        Surname = surname;
        Sales = sales;
        Subordinates = new List<Salesman>();

        if (id != 0)
        {
            ID = id;
            if (id > NextId)
                NextId = id + 1;
        }
        else
        {
            ID = NextId++;
        }
    }

    public void AddSubordinate(Salesman subordinate)
    {
        Subordinates.Add(subordinate);
    }

    //kód níže slouží jen pro naèítání ze souboru

    public static Salesman DeserializeTree(string jsonString)
    {
        List<SalesmanData> deserializedData = JsonSerializer.Deserialize<List<SalesmanData>>(jsonString);

        Dictionary<int, Salesman> treeData = new Dictionary<int, Salesman>();
        Salesman root = null;

        foreach (var item in deserializedData)
        {
            Salesman salesman = item.ToSalesman();
            treeData[salesman.ID] = salesman;

            if (item.ParentId != 0)
                treeData[item.ParentId].AddSubordinate(salesman);
            else
                root = salesman;
        }

        return root;
    }

    private class SalesmanData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Sales { get; set; }
        public int ParentId { get; set; }

        public SalesmanData() { }

        public SalesmanData(Salesman salesman, int parentId = 0)
        {
            ID = salesman.ID;
            Name = salesman.Name;
            Surname = salesman.Surname;
            Sales = salesman.Sales;
            ParentId = parentId;
        }

        public Salesman ToSalesman() => new Salesman(Surname, Name, Sales, ID);

    }

    public static int GetTotalSales(Salesman parentNode)
    {
        int total = parentNode.Sales;

        foreach (var subordinate in parentNode.Subordinates)
        {
            total += GetTotalSales(subordinate);
        }

        return total;
    }

    public static int FindIndexInList(List<Salesman> list, Salesman salesman)
    {
        Salesman sal;
        for (int i = 0; i < list.Count; i++)
        {
            sal = list[i];
            if (sal.Name == salesman.Name && sal.Surname == salesman.Surname && sal.Sales == salesman.Sales)
                return i;
        }
        return -1;
    }

    public static Salesman FindSalesman(Salesman root, string name, string surname, int sales)
    {
        if (root.Name == name && root.Surname == surname && root.Sales == sales)
            return root;
        Salesman temp;
        foreach (Salesman suboordinate in root.Subordinates)
        {
            temp = FindSalesman(suboordinate, name, surname, sales);
            if (temp != null) return temp;
        }
        return null;
    }

    public static List<Salesman> FindPathToSalesman(Salesman root, string targetName, string targetSurname, int targetSales)
    {
        if (root == null)
            return null;

        List<Salesman> path = new List<Salesman>();

        if (FindPathRecursive(root, targetName, targetSurname, targetSales, path))
            return path;

        return null;
    }

    private static bool FindPathRecursive(Salesman current, string targetName, string targetSurname, int targetSales,
                                                List<Salesman> path)
    {
        path.Add(current);

        if (current.Name == targetName && current.Surname == targetSurname && current.Sales == targetSales)
            return true;

        foreach (var subordinate in current.Subordinates)
        {
            if (FindPathRecursive(subordinate, targetName, targetSurname, targetSales, path))
                return true;
        }

        path.RemoveAt(path.Count - 1);

        return false;
    }

    public override string ToString()
    {
        return $"{Name} {Surname}";
    }
}