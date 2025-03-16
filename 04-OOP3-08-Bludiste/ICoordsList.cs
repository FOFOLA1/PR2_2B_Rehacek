namespace _04_OOP3_08_Bludiste
{
    internal interface ICoordsList
    {
        int Count { get; }
        void Add(Coords coords);
        Coords NextPlace();
    }
}
