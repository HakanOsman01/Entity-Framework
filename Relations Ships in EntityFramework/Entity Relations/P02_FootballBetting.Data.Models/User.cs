using _02P_FootBallBetting.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
        }
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(ValidationConstans.MaxUserNameLenght)]
        public string UserName { get; set; } = null!;
        [Required]
        [MaxLength(ValidationConstans.UserPasswordLenght)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstans.UserMaxEmailLenght)]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(ValidationConstans.MaxUserNameLenght)]
        public string Name { get; set; } = null!;
        public decimal Balance { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
