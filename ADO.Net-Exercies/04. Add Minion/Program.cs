using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlTypes;

namespace _04._Add_Minion
{
    internal class Program
    {
       private static string connectionString =
               @"Server =(LocalDB)\MSSQLLocalDB; 
             Database = MinionsDB ; Trusted_Connection = True;";
        static void Main(string[] args)
        {
            string[] minionInfo = Console.ReadLine().Split(' ').ToArray();
            string nameMinion = minionInfo[1];
            int ageMinion = int.Parse(minionInfo[2]);
            string town = minionInfo[3];
            int idTown = GetTownId(town).GetAwaiter().GetResult();
            if (idTown == 0)
            {
                string townSucces = AddTown(town).GetAwaiter().GetResult();
                if (townSucces.IsNullOrEmpty())
                {
                    Console.WriteLine(townSucces);
                }
            }
            string[] villinanInfo = Console.ReadLine().Split(' ').ToArray();
            string vilanName = villinanInfo[1];
            string villanSuccess=AddVillan(vilanName).GetAwaiter().GetResult();
            if(!vilanName.IsNullOrEmpty())
            {
                Console.WriteLine(villanSuccess);
            }
            Console.WriteLine(ConnectMinionToVillan(nameMinion,ageMinion,idTown,vilanName)
                .GetAwaiter().GetResult());






        }
        private static async Task<string>AddTown(string townName)
        {
            string output = string.Empty;
           Quiryes quiryes=new Quiryes();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              await connection.OpenAsync();
                string quiry = Quiryes.TownSearch;
                string insertTownQuiry = quiryes.AddTown();
              using(SqlCommand command =new SqlCommand(quiry,connection))
              {
                    command.Parameters.AddWithValue("@townNameParam", townName);
                    output =(string)await command.ExecuteScalarAsync();
                    if (output.IsNullOrEmpty())
                    {
                        return $"Town {townName} was added to the database.";
                    }

              }
              using(SqlCommand command=new SqlCommand(insertTownQuiry, connection))
              {
                    command.Parameters.AddWithValue("@townNameParam", townName);
                    await command.ExecuteNonQueryAsync();
              }



            }
            return output;
        }
        private static async Task<string> AddVillan(string villanName)
        {
            string output = string.Empty;
            Quiryes quiryes = new Quiryes();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string searchQuiry = @"SELECT v.Name FROM Villains AS v
                WHERE v.Name=@VillanParamName";
                using (SqlCommand sqlcommand=new SqlCommand(searchQuiry,connection))
                {
                    sqlcommand.Parameters.AddWithValue("@VillanParamName", villanName);
                    output =(string) await sqlcommand.ExecuteScalarAsync();
                   
                    if (!output.IsNullOrEmpty())
                    {
                        return string.Empty;
                    }

                }
                using(SqlCommand sqlcommand=new SqlCommand(quiryes.AddVillian(),connection))
                {
                    sqlcommand.Parameters.AddWithValue("@VillanParamName", villanName);
                    await sqlcommand.ExecuteNonQueryAsync();
                    return $"{villanName} was added to the database.";

                }

            }
           
        }
        private static async Task<string> ConnectMinionToVillan
            (string minionName,int minionAge,int idTown, string villianName)
        {
            int minionId = 0;
            int villianId = 0;
            
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string minionQuiry = @"SELECT m.Id FROM Minions AS m
WHERE m.Name=@Name";
                string quiryForAddMinion = @"INSERT INTO Minions (Name,Age,TownId)
 VALUES(@NameParam,@AgeParam,@TownIdParam)";

                using (SqlCommand sqlCommand=new SqlCommand(quiryForAddMinion,connection))
                {
                    sqlCommand.Parameters.AddWithValue("@NameParam", minionName);
                    sqlCommand.Parameters.AddWithValue("@AgeParam", minionAge);
                    sqlCommand.Parameters.AddWithValue("@TownIdParam", idTown);
                    await sqlCommand.ExecuteNonQueryAsync();


                }
                using (SqlCommand sqlCommand=new SqlCommand(minionQuiry, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Name", minionName);
                    minionId =(int)await sqlCommand.ExecuteScalarAsync();

                }
                string villinaQuiry = @"SELECT v.Id FROM [Villains] AS v
WHERE v.Name=@VillanParamName";
                using (SqlCommand sqlcommand=new SqlCommand(villinaQuiry, connection))
                {
                    sqlcommand.Parameters.AddWithValue("@VillanParamName", villianName);
                    villianId =(int)await sqlcommand.ExecuteScalarAsync();
                }
                string addMinionAndVillian = @"INSERT INTO MinionsVillains (MinionId,VillainId)
VALUES (@MinionId,@VillainId)";
                using (SqlCommand sqlCommand=new SqlCommand(addMinionAndVillian,connection))
                {
                    sqlCommand.Parameters.AddWithValue("@MinionId", minionId);
                    sqlCommand.Parameters.AddWithValue("@VillainId", villianId);
                    await sqlCommand.ExecuteNonQueryAsync();
                   

                }
                return $"Successfully added {minionName} to be minion of {villianName}.";
            }
           
        }
        private static async Task<int>GetTownId(string townName)
        {
            int id=0;
            using(SqlConnection sqlConnection=new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                string quiryForTown = @"SELECT t.Id FROM Towns AS t
               WHERE t.Name=@TownParam";
                using(SqlCommand sqlCommand=new SqlCommand(quiryForTown, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@TownParam", townName);
                    if(! (await sqlCommand.ExecuteScalarAsync() is null))
                    {
                        id = (int)await sqlCommand.ExecuteScalarAsync();
                    }
                    
                }

            }
            return id;
        }
        
    }
}