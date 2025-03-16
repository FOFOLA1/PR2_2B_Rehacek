namespace _02_OOP2_01_cunter
{
    internal class StepCounter : Counter
    {
        public int Step { get; private set; }

        public StepCounter(int step)
        {
            Step = step;
        }

        public sealed override void Next()
        {
            Count += Step;
        }
    }
}
