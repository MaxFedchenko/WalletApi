using Microsoft.EntityFrameworkCore;
using WalletApi.DataAccess.Entities;

namespace WalletApi.DataAccess.EF
{
    public class WalletContext : DbContext
    {
        private readonly string conn_string;

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public WalletContext(IConfiguration app_config)
        {
            conn_string = app_config.GetConnectionString("DefaultConnection")!;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(conn_string);
        }
    }
}
