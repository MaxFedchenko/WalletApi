using Microsoft.EntityFrameworkCore;
using WalletApi.DataAccess.EF;

namespace WalletApi.Model.Services
{
    public class CardService : ICardService
    {
        private readonly WalletContext _context;

        public CardService(WalletContext context)
        {
            _context = context;
        }

        public async Task<Card?> GetByUserId(int userId)
        {
            return await _context.Cards.Where(c => c.UserId == userId)
                .Select(c => new Card { Id = c.Id, Balance = c.Balance })
                .FirstOrDefaultAsync();
        }
    }
}
