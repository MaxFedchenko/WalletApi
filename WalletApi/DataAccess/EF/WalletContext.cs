using Microsoft.EntityFrameworkCore;
using WalletApi.DataAccess.Entities;

namespace WalletApi.DataAccess.EF
{
    public class WalletContext : DbContext
    {
        private readonly string _connString;

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public WalletContext(IConfiguration appConfig)
        {
            _connString = appConfig.GetConnectionString("DefaultConnection")!;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connString);
        }
    }
}
