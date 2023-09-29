using WalletApi.Core.Enums;

namespace WalletApi.Model.Services
{
    public class TransactionInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionType Type { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public bool IsPending { get; set; }
        public string User { get; set; }
        public byte[]? Icon { get; set; }
    }
}
