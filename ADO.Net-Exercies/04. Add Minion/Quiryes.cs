using _04._Add_Minion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04._Add_Minion
{
    public class Quiryes : IAddInDB
    {
        public const string TownSearch= @"SELECT t.Name FROM [Towns] AS t 
            WHERE t.Name=@townNameParam";

        public string AddMinion()
        {
            throw new NotImplementedException();
        }

        public string AddTown()
        {
            string command = @"INSERT INTO Towns(Name) 
     VALUES (@townNameParam)";
            return command;
        }

        public string AddVillian()
        {
            string command = @"INSERT INTO [Villains] (Name,EvilnessFactorId)
VALUES (@VillianParamName,4)";
            return command;
        }
    }
}
