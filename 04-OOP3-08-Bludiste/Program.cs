using _04_OOP3_08_Bludiste;

class Program
{
    static void Main(string[] args)
    {
        Maze maze = new Maze();
        maze.LoadMaze("maze.txt");
        maze.Solve(new StackPlaceList());
    }
}