namespace _03_OOP3_10_Unit_tests
{
    public class Tools
    {
        public static int FindMin(int[] nums)
        {
            if (nums.Length == 0) throw new ArgumentException("Empty array cannot have minimum: " + nameof(nums));


            int min = int.MaxValue;
            foreach (int x in nums)
            {
                if (x < min)
                {
                    min = x;
                }
            }
            return min;
        }

    }
}
