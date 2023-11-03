using _02P_FootBallBetting.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Color
    {
        public Color()
        {
            this.PrimaryKitTeam = new HashSet<Team>();
            this.SecondaryKitTeam= new HashSet<Team>(); 
        }
        [Key]
        public int ColorId { get; set; }
        [Required]

        [MaxLength(ValidationConstans.MaxColorName)]
        public string Name { get; set; } = null!;
        [InverseProperty (nameof(Team.PrimaryKitColor))]
        public virtual ICollection<Team>  PrimaryKitTeam{ get; set; }

        [InverseProperty (nameof(Team.SecondaryKitColor))]
        public virtual ICollection<Team> SecondaryKitTeam { get; set; }
    }

}
