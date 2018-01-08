using System;
using System.Data.Entity;
using System.Linq;
namespace GOL
{
    public class DBContext : DbContext
    {
        public virtual DbSet<GameRound> GameRounds { get; set; }
        public virtual DbSet<SaveGame> SaveGames { get; set; }         
        
        public DBContext()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContext, Migrations.Configuration>("DefaultConnection"));
        }
    }
}