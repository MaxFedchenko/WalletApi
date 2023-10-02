using System.ComponentModel.DataAnnotations.Schema;
using WalletApi.Core.Enums;

namespace WalletApi.DataAccess.Entities
{
    public class Transaction : Entity<int>
    {
        public string Name { get; set; } = "";
        public TransactionType Type { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "money")]
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public bool IsPending { get; set; }
        public byte[]? Icon { get; set; }

        public int AuthorizedUserId { get; set; }
        public User? AuthorizedUser { get; set; }

        public int CardId { get; set; }
        public Card? Card { get; set; }
    }
}
