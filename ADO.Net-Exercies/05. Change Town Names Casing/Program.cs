using Microsoft.Data.SqlClient;
using System.Text;

namespace _05._Change_Town_Names_Casing
{
    internal class Program
    {
        private static string connectionString = @"Server =(LocalDB)\MSSQLLocalDB; 
             Database = MinionsDB; Trusted_Connection = True;";
        static void Main(string[] args)
        {
            string contryName=Console.ReadLine();
            int contryCode = GetContryCode(contryName)
                .GetAwaiter()
                .GetResult();
            int countTownsEffected = GetCountOfEffectedTowns(contryCode)
                .GetAwaiter()
                .GetResult();
            if (countTownsEffected == 0)
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                Console.WriteLine($"{countTownsEffected} town names were affected.");
                string allTownsName = GetEffectedTownsNames(contryCode)
                    .GetAwaiter()
                    .GetResult();
                ChangeTownsNameToUpperCase(contryCode)
                    .GetAwaiter()
                    .GetResult();
                Console.WriteLine($"{allTownsName}");
            }

        }

        private static async Task  ChangeTownsNameToUpperCase(int contryCode)
        {
           using(SqlConnection sqlConnection=new SqlConnection(connectionString))
           {
                await sqlConnection.OpenAsync();
                string quiry = @"UPDATE Towns
SET Name=UPPER(Name)
WHERE CountryCode=@ContryCodeParam";
                using(SqlCommand sqlCommand=new SqlCommand(quiry,sqlConnection)) 
                {
                    sqlCommand.Parameters.AddWithValue("@ContryCodeParam", contryCode);
                    await sqlCommand.ExecuteNonQueryAsync();
                }

           }
        }

        private static async Task<int> GetCountOfEffectedTowns(int contryCode)
        {
            string quiry = @"SELECT COUNT(*) 
FROM Countries AS c INNER JOIN [Towns] AS t
ON c.Id=t.CountryCode
GROUP BY t.CountryCode
HAVING t.CountryCode=@ContyIdParam";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(quiry, connection))
                {
                    command.Parameters.AddWithValue("@ContyIdParam", contryCode);

                    int count = 0;
                    if (await command.ExecuteScalarAsync() == null)
                    {
                        return count;
                    }
                    else
                    {
                        count = (int)await command.ExecuteScalarAsync();
                    }
                    return count;
                    
                    
                }
            }
        }

        private static async Task<int> GetContryCode(string contryName)
        {
            string townQuiry = @"SELECT c.Id FROM Countries AS c
WHERE c.Name=@ContryNameParam";
            using (SqlConnection sqlConnection=new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                using(SqlCommand sqlCommand=new SqlCommand(townQuiry,sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ContryNameParam", contryName);
                    int townId=(int)await sqlCommand.ExecuteScalarAsync();
                    return townId;

                }

            }
        }
        private static async Task<string> GetEffectedTownsNames(int contryCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            using(SqlConnection sqlConnection=new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                string quiry = @"SELECT t.Name AS [TownName] FROM Towns AS 
                  t WHERE t.CountryCode=@ContryCodeParam";
                using (SqlCommand sqlCommand=new SqlCommand(quiry, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ContryCodeParam", contryCode);
                    using SqlDataReader reader= await sqlCommand.ExecuteReaderAsync();
                    while(reader.Read())
                    {
                        
                        stringBuilder.Append(reader["TownName"].ToString().ToUpper());
                       
                        stringBuilder.Append(", ");

                    }
                    stringBuilder.Remove(stringBuilder.Length - 2,2);
                    stringBuilder.Append("]");
                }
            }
            return stringBuilder.ToString();
        }
    }
}