namespace WalletApi.Model.Services
{
    public interface ICardService
    {
        Task<Card?> GetByUserId(int user_id);
    }
}