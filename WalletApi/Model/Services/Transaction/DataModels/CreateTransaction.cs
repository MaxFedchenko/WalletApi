using WalletApi.Core.Enums;

namespace WalletApi.Model.Services
{
    public class CreateTransaction
    {
        public required string Name { get; set; }
        public required TransactionType Type { get; set; }
        public required decimal Sum { get; set; }
        public required DateTime Date { get; set; }
        public required bool IsPending { get; set; }
        public required int CardId { get; set; }
        public required int UserId { get; set; }
        public byte[]? Icon { get; set; }
        public string? Description { get; set; }
    }
}
