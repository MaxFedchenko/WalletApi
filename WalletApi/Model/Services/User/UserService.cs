using WalletApi.DataAccess.EF;
using WalletApi.DataAccess.Entities;

namespace WalletApi.Model.Services
{
    public class UserService : IUserService
    {
        private readonly WalletContext _context;

        public UserService(WalletContext context)
        {
            _context = context;
        }

        public async Task<int> Create(string userName)
        {
            var user = new User
            {
                Name = userName,
                Card = new DataAccess.Entities.Card
                {
                    Balance = 0
                }
            };

            _context.Add(user);

            await _context.SaveChangesAsync();

            return user.Id;
        }
    }
}
