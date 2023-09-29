namespace WalletApi.Model.Services
{
    public interface IUserService
    {
        Task<int> Create(string user_name);
    }
}