using _06_db;

namespace _06_DB_02_EF_basics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new MyDbContext();
            var cars = dbContext.Cars.ToList();

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Brand} {car.Model} {car.RegPlate}");
            }

            var newCar = new Car() { Brand = "Toyota", Model = "Cellica", Purchased = DateTime.Now, RegPlate = "123456", Id = 5 };

            dbContext.Cars.Add(newCar);
            dbContext.SaveChanges();
        }
    }
}
