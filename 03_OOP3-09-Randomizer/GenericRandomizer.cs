namespace _03_OOP3_09_Randomizer
{
    internal class GenericRandomizer<T>
    {
        private T[] _nums;
        private int _position;
        private Random _random = new Random();

        public GenericRandomizer(T[] nums)
        {
            this._nums = nums;
            Randomize();
            _position = 0;
        }

        private void Randomize()
        {
            for (int i = _nums.Length - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                T tmp = _nums[i];
                _nums[i] = _nums[j];
                _nums[j] = tmp;
            }
        }
        public T Next()
        {
            return _nums[_position++];
        }
    }
}
