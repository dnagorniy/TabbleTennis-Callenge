using System.Data.Entity;
using TTC_DB.Entities;

namespace TTC_DB.Context
{
    public class TtcContext : DbContext
    {
        public TtcContext() : base("name=Ttc_ConnectionString") { }

        public virtual DbSet<GameResult> GameResults { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e => e.GameResults)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.GameResults)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.Winner)
                .HasForeignKey(e => e.WinnerId)
                .WillCascadeOnDelete(false);
        }

    }
}