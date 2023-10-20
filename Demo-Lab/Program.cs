
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace FirstExerciese
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                @"Server =(LocalDB)\MSSQLLocalDB; 
             Database = SoftUni; Trusted_Connection = True;";
             GetAllEmployeesLastName(connectionString).GetAwaiter().GetResult();


        }
        private static async Task GetAllEmployeesLastName(string connectionString)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                string query = @"SELECT FirstName" + ",LastName" + ",Salary FROM Employees " +
                    "WHERE Salary > @salaryParam";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@salaryParam", 50000);

                    SqlDataReader sqlDataReader =  await sqlCommand.ExecuteReaderAsync();
                    while ( await sqlDataReader.ReadAsync())
                    {

                        string firstName = sqlDataReader[0].ToString();
                        string lastName = sqlDataReader[1].ToString();
                        decimal salary = decimal.Parse(sqlDataReader[2].ToString());
                        Console.WriteLine($"{firstName} - {lastName} with salary {salary:f2}");
                    }

                }


            }

        }

    }
}