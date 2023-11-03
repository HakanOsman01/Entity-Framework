using _02P_FootBallBetting.Data.Common;
using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext() 
        {

        }
        public FootballBettingContext(DbContextOptions options)
            :base(options) 
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConfig.ConnnectionString);
            }
           base.OnConfiguring(optionsBuilder);
             
            
           
        }
        public DbSet<PlayerStatistic> PlayerStatistic { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Town>Towns { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Country>Countries { get; set; }
        public DbSet<Team>Teams { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<User>Users { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(ps => new { ps.PlayerId, ps.GameId });
            });
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasOne(e=>e.PrimaryKitColor)
                .WithMany(e=>e.PrimaryKitTeam)
                .HasForeignKey(e=>e.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.SecondaryKitColor)
                .WithMany(e => e.SecondaryKitTeam)
                .HasForeignKey(e => e.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasOne(g => g.HomeTeam)
                .WithMany(g => g.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(g => g.AwayTeam)
                .WithMany(g => g.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.NoAction);

            });
        }
       

    }
}