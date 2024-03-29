﻿using _02P_FootBallBetting.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class Position
    {
        public Position()
        {
            this.Players = new HashSet<Player>();
        }
        [Key]
        public int PositionId { get; set; }
        [Required]
        [MaxLength(ValidationConstans.MaxPositionNameLenght)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Player> Players { get; set;}
    }
}
