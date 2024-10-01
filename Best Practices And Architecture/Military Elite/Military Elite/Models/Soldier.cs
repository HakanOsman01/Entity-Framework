using Military_Elite.Contracts;
using System.Text;

namespace Military_Elite.Models
{
    public class Soldier : ISoldier
    {
        public Soldier(int id,string fistName,string lastName)
        {
            this.Id = id;
            this.FirstName = fistName;
            this.LastName = lastName;

        }
        public int Id { get;private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }
        public override string ToString()
        {
            
           
            return $"Name: {FirstName} {LastName} Id: {Id}";
        }
    }
}
