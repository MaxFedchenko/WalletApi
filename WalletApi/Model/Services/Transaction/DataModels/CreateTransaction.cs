using System.ComponentModel.DataAnnotations;
using WalletApi.Core.Enums;

namespace WalletApi.Model.Services
{
    public class CreateTransaction
    {
        public string Name { get; set; }
        public TransactionType Type { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public bool IsPending { get; set; }
        public int CardId { get; set; }
        public int UserId { get; set; }
        public byte[]? Icon { get; set; }
        public string? Description { get; set; }
    }
}
