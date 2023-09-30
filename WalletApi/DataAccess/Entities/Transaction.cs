using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalletApi.Core.Enums;

namespace WalletApi.DataAccess.Entities
{
    public class Transaction : Entity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool IsPending { get; set; }
        public byte[]? Icon { get; set; }

        public int AuthorizedUserId { get; set; }
        public User AuthorizedUser { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
