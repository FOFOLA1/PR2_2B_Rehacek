namespace _03_OOP3_09_Randomizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9];
            Randomizer randomizer = new Randomizer(nums);

            for (int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(randomizer.Next());
            }

            Console.WriteLine();

            GenericRandomizer<int> genericRandomizer = new GenericRandomizer<int>(nums);
            for (int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(genericRandomizer.Next());
            }

            Console.WriteLine();

            String[] strs = ["1", "2", "3", "4", "5", "6", "7", "8", "9"];
            GenericRandomizer<String> genericRandomizer2 = new GenericRandomizer<String>(strs);

            for (int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine(genericRandomizer2.Next());
            }
        }
    }
}
