﻿using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        public virtual Player Player { get; set; } = null!;
        [ForeignKey(nameof(Player))]

        public int PlayerId { get; set; }
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public virtual Game Game { get; set; } = null!;
        public byte ScoredGoals { get; set; }
        public byte Assists { get; set; }
        public byte MinutesPlayed { get; set; }


    }
}
