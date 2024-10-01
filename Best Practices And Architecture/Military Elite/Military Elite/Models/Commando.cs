using Military_Elite.Contracts;
using Military_Elite.Enums;
using System.Text;

namespace Military_Elite.Models
{
    public class Commando : Private, ICommando
    {
        public Commando(int id, string fistName, string lastName, decimal salary
            , SpecialisedSoldier specialisedSoldier)
            : base(id, fistName, lastName, salary)
        {
            this.SpecialisedSoldier = specialisedSoldier;
            this.Missions = new List<IMission>();
        }

        public List<IMission> Missions { get;set; }

        public SpecialisedSoldier SpecialisedSoldier { get;private set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($"Corps: {SpecialisedSoldier.ToString()}");
            sb.AppendLine("Missions:");
            foreach (var mission in this.Missions)
            {
                sb.AppendLine(mission.ToString());
            }
            return sb.ToString().Trim();
        }
    }
}
