namespace WalletApi.Model.Services
{
    public interface ITransactionService
    {
        Task<int> Create(CreateTransaction transaction, int user_id);
        Task<TransactionDetails?> GetDetails(int transaction_id, int user_id);
        Task<IEnumerable<TransactionInfo>> GetRange(int offset, int amount, int user_id);
    }
}