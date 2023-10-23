using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04._Add_Minion.Models
{
    public class Minion
    {
        public Minion(string name,int age,string town) 
        {
            Name = name;
            Age = age;
            Town = town;

        }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Town { get; private set; }
    }
}
