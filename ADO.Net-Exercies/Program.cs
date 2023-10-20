using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace _2._Villain_Names
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server =(LocalDB)\MSSQLLocalDB; 
             Database = MinionsDB; Trusted_Connection = True;";
            string getViliansName = GetVillansNames(connectionString)
                . GetAwaiter().GetResult();
            Console.WriteLine(getViliansName);

        }

        private static async Task<string> GetVillansNames(string connectionString)
        {
            string output = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT v.Name AS [VilianName],COUNT(m.Id) AS [CountMinions] \r\nFROM Villains " +
                    "AS v INNER JOIN [MinionsVillains] AS mv\r\nON v.Id=mv.VillainId INNER JOIN " +
                    "[Minions] AS m\r\nON mv.MinionId=m.Id\r\nGROUP BY v.Id,v.Name\r\nHAVING COUNT(m.Id)>3";
                using (SqlCommand command=new SqlCommand(query, connection))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                   
                    while(await reader.ReadAsync())
                    {
                        output = $"{reader["VilianName"]} – {reader["CountMinions"]}";

                    }
                }

            }
            return output;
        }
    }
}