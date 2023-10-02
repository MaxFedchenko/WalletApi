using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WalletApi.Core.Enums;
using WalletApi.DataAccess.EF;

namespace WalletApi.Model.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly WalletContext _context;
        private readonly IMapper _mapper;
        private readonly decimal _cardLimit;

        public TransactionService(WalletContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _cardLimit = configuration.GetValue<decimal>("CardLimit");
        }

        public async Task<TransactionDetails?> GetDetails(int transactionId, int userId)
        {
            return await _context.Transactions
                .Include(t => t.Card)
                .Include(t => t.AuthorizedUser)
                .Where(t => t.Id == transactionId)
                .Where(t => t.Card!.UserId == userId)
                .Select(t => _mapper.Map<TransactionDetails>(t))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TransactionInfo>> GetRange(int offset, int amount, int userId)
        {
            return await _context.Transactions
                .Include(t => t.Card)
                .Include(t => t.AuthorizedUser)
                .Where(t => t.Card!.UserId == userId)
                .OrderByDescending(t => t.Date)
                .Skip(offset).Take(amount)
                .Select(t => _mapper.Map<TransactionInfo>(t))
                .ToListAsync();
        }

        public async Task<int> Create(CreateTransaction transaction)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == transaction.CardId);
            if (card == null) throw new ArgumentException(null, nameof(transaction.CardId));

            // Update Card Balance
            card.Balance = card.Balance + transaction.Type switch
            {
                TransactionType.Payment => transaction.Sum,
                TransactionType.Credit => -1 * transaction.Sum,
                _ => throw new NotSupportedException()
            };
            if (card.Balance < 0 || card.Balance > _cardLimit)
                throw new ArgumentOutOfRangeException(nameof(transaction.Sum));

            // Add New Transaction
            var tran_entity = _mapper.Map<DataAccess.Entities.Transaction>(transaction);
            tran_entity.CardId = card.Id;
            _context.Transactions.Add(tran_entity);

            await _context.SaveChangesAsync();

            return tran_entity.Id;
        }
    }
}
