using System.Data.Entity;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker.EntityFramework
{
    public class HandHistoryContext : DbContext
    {
        public HandHistoryContext() : base("HandHistoryContext")
        {
            Database.SetInitializer(new HandHistoryInitializer());
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerHistory> PlayerHistories { get; set; }
        public DbSet<HandAction> HandActions { get; set; }
    }
}
