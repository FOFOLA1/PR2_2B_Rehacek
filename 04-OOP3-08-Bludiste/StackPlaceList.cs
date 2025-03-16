namespace _04_OOP3_08_Bludiste
{
    internal class StackPlaceList : ICoordsList
    {
        Stack<Coords> _places = new Stack<Coords>();

        public int Count => _places.Count;

        public void Add(Coords coord)
        { _places.Push(coord); }

        public Coords NextPlace()
        {
            return _places.Pop();
        }
    }
}
