using System;
using System.Data.Entity;
using System.Linq;
namespace GOL
{
    public class DBContext : DbContext
    {
        public virtual DbSet<GameRound> Rounds { get; set; }
        public virtual DbSet<Game> Games { get; set; }         
        
        public DBContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContext, Migrations.Configuration>("DefaultConnection"));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameRound>()
                .HasOptional<Game>(s => s.Game)
                .WithMany()
                .WillCascadeOnDelete(true);
        }
    }
}