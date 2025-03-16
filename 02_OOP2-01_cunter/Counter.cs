namespace _02_OOP2_01_cunter
{
    internal class Counter
    {
        public int Count { get; protected set; }

        public virtual void Next()
        {
            Count++;
        }

        public virtual void Reset()
        {
            Count = 0;
        }
    }
}
