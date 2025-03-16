using Microsoft.Data.SqlClient;

namespace _06_db
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PR2B;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                insert(connection, "3565fds", "Ford", "Fiesta", DateTime.Now);
                selectCars(connection);
                updateCar(connection, 5, "3565fds", "Ford", "Fiesta", DateTime.Now);
            }


        }

        private static void insert(SqlConnection connection,
            string regPlate,
            string brand,
            string model,
            DateTime purchased
            )
        {
            string insQuery = "INSERT INTO Cars(RegPlate, Model, Purchased) VALUES(@RegPlate, @Model, @Purchased";

            SqlCommand insCommand = new SqlCommand(insQuery, connection);
            //insCommand.Parameters.AddWithValue("@Id", id);
            insCommand.Parameters.AddWithValue("@RegPlate", regPlate);
            insCommand.Parameters.AddWithValue("@Brand", brand);
            insCommand.Parameters.AddWithValue("@Model", model);
            insCommand.Parameters.AddWithValue("@Purchased", purchased);
        }

        private static void updateCar(SqlConnection connection,
            int id,
            string regPlate,
            string brand,
            string model,
            DateTime purchased
            )
        {
            string insQuery = "UPDATE Cars SET RegPlate=@RegPlate, Model=@Model, Purchased=@Purchased, Brand=@Brand WHERE Id=@Id";
            SqlCommand insCommand = new SqlCommand(insQuery, connection);
            insCommand.Parameters.AddWithValue("@Id", id);
            insCommand.Parameters.AddWithValue("@RegPlate", regPlate);
            insCommand.Parameters.AddWithValue("@Brand", brand);
            insCommand.Parameters.AddWithValue("@Model", model);
            insCommand.Parameters.AddWithValue("@Purchased", purchased);
        }

        private static void selectCars(SqlConnection connection)
        {
            string query = "SELECT * FROM Cars LEFT JOIN Drivers ON (Cars.Id = Drivers.CarId)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string RegPlate = reader.GetString(1);
                        string Brand = (string)reader["Brand"];
                        string Model = (string)reader["Model"];
                        string Name = (reader["Name"] is null) ? "" : (string)reader["Name"];
                        string Surname = (reader["Surname"] is null) ? "" : (string)reader["Surname"];
                        DateTime Purchased = reader.GetDateTime(4);

                        Console.WriteLine($"id {id}, regplate {RegPlate}, brand {Brand}, model {Model}, purchased {Purchased}, name {Name}, surname {Surname}");

                    }
                }
            }
        }

        private static void deleteCar(SqlConnection connection, int id)
        {
            string insQuery = "DELETE FROM Cars WHERE Id=@Id";

            SqlCommand insCommand = new SqlCommand(insQuery, connection);
            insCommand.Parameters.AddWithValue("@Id", id);
        }

    }
}
