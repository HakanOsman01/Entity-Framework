using Military_Elite.Contracts;
using Military_Elite.Enums;
using System.Text;

namespace Military_Elite.Models
{
    public class Enginner : Private,IEngineer
    {
        //•	Engineer: "Engineer <id> <firstName> <lastName> <salary> <corps> <repair1Part> 
        //    <repair1Hours> … <repairNPart> <repairNHours>
        public Enginner(int id, string fistName, string lastName, decimal salary,SpecialisedSoldier specialisedSoldier) 
            : base(id, fistName, lastName, salary)
        {
            this.Repairs = new List<IRepair>();
            this.SpecialisedSoldier = specialisedSoldier;
        }

        public List<IRepair> Repairs { get; set; }

        public SpecialisedSoldier SpecialisedSoldier { get;private set; }
        public override string ToString()
        {
           StringBuilder stringBuilder= new StringBuilder();
            stringBuilder.AppendLine(base.ToString());
            stringBuilder.AppendLine($"Corps: {SpecialisedSoldier.ToString()}");
            stringBuilder.AppendLine("Repairs:");
            foreach (var repair in this.Repairs)
            {
                stringBuilder.AppendLine(repair.ToString());
            }
            return stringBuilder.ToString().Trim();
        }
    }
}
