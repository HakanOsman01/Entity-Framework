using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace _03._Minion_Names
{
     
    internal class Program
    {
       private static  string connectionString = @"Server =(LocalDB)\MSSQLLocalDB; 
             Database = MinionsDB; Trusted_Connection = True;";
        static void Main(string[] args)
        {
            int id=int.Parse(Console.ReadLine());
            string getIdVilllan = GetVillansId(id).GetAwaiter().GetResult();
            Console.WriteLine(getIdVilllan);
            string minionsSetForVillan= 
                GetAllMinionsForVillan(id).GetAwaiter().GetResult();

            if (minionsSetForVillan.Length == 0)
            {
                Console.WriteLine("(no minions)");
            }
            else
            {
                Console.WriteLine(minionsSetForVillan.ToString().Trim());
            }
          


        }

        private static  async Task<string>GetAllMinionsForVillan(int id)
        {
            string getMinionsQuery = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) 
                                        AS RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @IdParam
                                ORDER BY m.Name";
            StringBuilder stringBuilder = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               await connection.OpenAsync();
                using(SqlCommand command = new SqlCommand
                    (getMinionsQuery,connection))
                {
                    command.Parameters.AddWithValue("@IdParam", id);
                   using SqlDataReader reader =  await command.ExecuteReaderAsync();
                    while ( await reader.ReadAsync())
                    {
                        stringBuilder.AppendLine
                            ($"{reader["RowNum"].ToString()}. " +
                            $"{reader["Name"].ToString()} " +
                            $"{reader["Age"].ToString()}");
                    }

                }
            }
            return stringBuilder.ToString().Trim();
        }

        private static async Task<string> GetVillansId(int id)
        {
            Query query = new Query();
            
            string nameVillan = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using(SqlCommand command =new SqlCommand(query.CreateQuiry(id),connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    nameVillan=(string)await command.ExecuteScalarAsync();
                    if (nameVillan.IsNullOrEmpty())
                    {
                        return $"No villain with ID {id} exists in the database.";
                    }
                  
                    
                }

            }
            return $"Villain: {nameVillan}";
        }
    }
}