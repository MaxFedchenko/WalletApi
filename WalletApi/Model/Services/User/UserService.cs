using WalletApi.DataAccess.EF;
using WalletApi.DataAccess.Entities;

namespace WalletApi.Model.Services
{
    public class UserService : IUserService
    {
        private readonly WalletContext context;

        public UserService(WalletContext context)
        {
            this.context = context;
        }

        public async Task<int> Create(string user_name)
        {
            var user = new User
            {
                Name = user_name,
                Card = new DataAccess.Entities.Card
                {
                    Balance = 0
                }
            };

            context.Add(user);

            await context.SaveChangesAsync();

            return user.Id;
        }
    }
}
