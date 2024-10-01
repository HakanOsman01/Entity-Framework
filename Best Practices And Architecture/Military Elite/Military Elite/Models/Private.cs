using Military_Elite.Contracts;
using System.Text;

namespace Military_Elite.Models
{
    public class Private : Soldier, IPrivate
    {
        public Private(int id, string fistName, string lastName,decimal salary) 
            : base(id, fistName, lastName)
        {
            this.Salary = salary;
        }

        
        public decimal Salary { get;private set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine (base.ToString()+" "+ $"Salary: {Salary:f2}");
            return stringBuilder.ToString().Trim();
        }

    }
}
