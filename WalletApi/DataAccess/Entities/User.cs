using System.ComponentModel.DataAnnotations;

namespace WalletApi.DataAccess.Entities
{
    public class User : Entity<int>
    {
        public string Name { get; set; } = "";

        public Card? Card { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
