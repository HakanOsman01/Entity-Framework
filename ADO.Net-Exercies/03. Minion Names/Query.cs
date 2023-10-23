using _03._Minion_Names.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03._Minion_Names
{
    public class Query : IQuiry
    {
        public string CreateQuiry(int id)
        {
            string theQuiry = @"SELECT Name FROM Villains WHERE Id = @Id";
            return theQuiry;
        }
    }
      
}
