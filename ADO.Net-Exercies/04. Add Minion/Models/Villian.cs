using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04._Add_Minion.Models
{
    public class Villian
    {
        public Villian(string name)
        {
            Name = name;
            
        }

        public string Name { get; private set; }
    }
}
