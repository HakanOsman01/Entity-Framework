using _02P_FootBallBetting.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Player
    {
        public Player()
        {
            this.PlayerStatistics = new HashSet<PlayerStatistic>();
        }
        [Key]
        public int PlayerId { get;set; }
        [Required]
        [MaxLength(ValidationConstans.MaxPlayerNameLenght)]
        public string Name { get; set; } = null!;
        public int SquadNumber { get; set; }
        public bool IsInjured { get; set; }
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public virtual Team? Team { get; set; }=null!;
        [ForeignKey (nameof(Position))]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; } = null!;
        public virtual ICollection<PlayerStatistic>PlayerStatistics { get; set; }

    }
}
