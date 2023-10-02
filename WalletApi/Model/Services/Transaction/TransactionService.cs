using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalletApi.Core.Enums;
using WalletApi.DataAccess.EF;

namespace WalletApi.Model.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly WalletContext context;
        private readonly IMapper mapper;
        private readonly decimal cardLimit;

        public TransactionService(WalletContext context, IConfiguration configuration, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            cardLimit = configuration.GetValue<decimal>("CardLimit");
        }

        public async Task<TransactionDetails?> GetDetails(int transactionId, int userId)
        {
            return await context.Transactions
                .Include(t => t.Card)
                .Include(t => t.AuthorizedUser)
                .Where(t => t.Id == transactionId)
                .Where(t => t.Card!.UserId == userId)
                .Select(t => mapper.Map<TransactionDetails>(t))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TransactionInfo>> GetRange(int offset, int amount, int userId)
        {
            return await context.Transactions
                .Include(t => t.Card)
                .Include(t => t.AuthorizedUser)
                .Where(t => t.Card!.UserId == userId)
                .OrderByDescending(t => t.Date)
                .Skip(offset).Take(amount)
                .Select(t => mapper.Map<TransactionInfo>(t))
                .ToListAsync();
        }

        public async Task<int> Create(CreateTransaction transaction)
        {
            var card = await context.Cards.FirstOrDefaultAsync(c => c.Id == transaction.CardId);
            if (card == null) throw new ArgumentException(null, nameof(transaction.CardId));

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
