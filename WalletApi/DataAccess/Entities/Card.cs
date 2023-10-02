using System.ComponentModel.DataAnnotations.Schema;

namespace WalletApi.DataAccess.Entities
{
    public class Card : Entity<int>
    {
        [Column(TypeName="money")]
        public decimal Balance { get; set; }

        public List<Transaction>? Transactions { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
