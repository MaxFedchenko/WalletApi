namespace WalletApi.Model.Services
{
    public interface ITransactionService
    {
        Task<int> Create(CreateTransaction transaction);
        Task<TransactionDetails?> GetDetails(int transactionId, int userId);
        Task<IEnumerable<TransactionInfo>> GetRange(int offset, int amount, int userId);
    }
}