using Microsoft.EntityFrameworkCore;
using WalletApi.DataAccess.EF;

namespace WalletApi.Model.Services
{
    public class CardService : ICardService
    {
        public readonly WalletContext context;

        public CardService(WalletContext context)
        {
            this.context = context;
        }

        public async Task<Card?> GetByUserId(int user_id)
        {
            return await context.Cards.Where(c => c.UserId == user_id)
                .Select(c => new Card { Id = c.Id, Balance = c.Balance })
                .FirstOrDefaultAsync();
        }
    }
}
