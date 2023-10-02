namespace WalletApi.Model.Services
{
    public interface IUserService
    {
        Task<int> Create(string userName);
    }
}