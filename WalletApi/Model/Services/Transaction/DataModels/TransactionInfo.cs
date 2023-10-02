using WalletApi.Core.Enums;

namespace WalletApi.Model.Services
{
    public class TransactionInfo
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required TransactionType Type { get; set; }
        public required decimal Sum { get; set; }
        public required DateTime Date { get; set; }
        public required bool IsPending { get; set; }
        public required string User { get; set; }
        public byte[]? Icon { get; set; }
    }
}
