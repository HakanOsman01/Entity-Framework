using Military_Elite.Contracts;
using System.Text;

namespace Military_Elite.Models
{
    public class LeutenantGeneral : Soldier, ILieutenantGeneral
    {
        public LeutenantGeneral(int id, string fistName, string lastName,decimal salary) 
            : base(id, fistName, lastName)
        {

            this.Salary = salary;
            this.Privates = new List<IPrivate>();
        }

        public List<IPrivate> Privates { get; set; }

        public decimal Salary { get; private set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(base.ToString()+" "+$"Salary: {Salary:f2}");
            stringBuilder.AppendLine($"Privates:");
            foreach (var currentPrivate in Privates)
            {
                stringBuilder.AppendLine(currentPrivate.ToString());
            }
            return stringBuilder.ToString().Trim();
        }
    }
}
