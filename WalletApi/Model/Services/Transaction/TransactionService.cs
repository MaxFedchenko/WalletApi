using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalletApi.Core.Enums;
using WalletApi.DataAccess.EF;

namespace WalletApi.Model.Services
{
    public class TransactionService : ITransactionService
    {
        public readonly WalletContext context;
        public readonly IMapper mapper;
        private readonly decimal cardLimit;

        public TransactionService(WalletContext context, IConfiguration configuration, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            cardLimit = configuration.GetValue<decimal>("CardLimit");
        }

        public async Task<TransactionDetails?> GetDetails(int transaction_id, int user_id)
        {
            return await context.Transactions
                .Include(t => t.Card)
                .ThenInclude(c => c.User)
                .Where(t => t.Id == transaction_id)
                .Where(t => t.Card.UserId == user_id)
                .Select(t => mapper.Map<TransactionDetails>(t))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TransactionInfo>> GetRange(int offset, int amount, int user_id)
        {
            return await context.Transactions
                .Include(t => t.Card)
                .ThenInclude(c => c.User)
                .Where(t => t.Card.UserId == user_id)
                .OrderByDescending(t => t.Date)
                .Skip(offset).Take(amount)
                .Select(t => mapper.Map<TransactionInfo>(t))
                .ToListAsync();
        }

        public async Task<int> Create(CreateTransaction transaction, int user_id)
        {
            var card = await context.Cards.FirstOrDefaultAsync(c => c.UserId == user_id);
            if (card == null) throw new ArgumentException(nameof(user_id));

            // Update Card Balance
            card.Balance = card.Balance + transaction.Type switch
            {
                TransactionType.Payment => transaction.Sum,
                TransactionType.Credit => -1 * transaction.Sum,
                _ => throw new NotSupportedException()
            };
            if (card.Balance < 0 || card.Balance > cardLimit)
                throw new ArgumentOutOfRangeException(nameof(transaction.Sum));

            // Add New Transaction
            var tran_entity = mapper.Map<DataAccess.Entities.Transaction>(transaction);
            tran_entity.CardId = card.Id;
            context.Transactions.Add(tran_entity);

            await context.SaveChangesAsync();

            return tran_entity.Id;
        }
    }
}
