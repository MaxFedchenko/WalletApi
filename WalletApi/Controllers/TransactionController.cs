using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Text;
using WalletApi.Model.DTOs;
using WalletApi.Model.Services;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TransactionController(IConfiguration configuration, ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _configuration = configuration;
            _mapper = mapper;
        }

        private void IfNoIconAddDefault(TransactionDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Icon))
                dto.Icon = _configuration.GetValue<string>("DefaultTransactionIcon")!;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [Required] int userId) 
        {
            var transaction = await _transactionService.GetDetails(id, userId);
            if (transaction == null) return NotFound();

            var dto = _mapper.Map<TransactionDetailsDTO>(transaction);
            IfNoIconAddDefault(dto);
            return Ok(dto); 
        }

        [HttpGet]
        public async Task<IActionResult> GetRange([Required] int userId, int offset = 0, int amount = 10) 
        {
            var transactions = await _transactionService.GetRange(offset, amount, userId);

            return Ok(transactions.Select(t => 
            {
                var dto = _mapper.Map<TransactionDTO>(t);
                IfNoIconAddDefault(dto);
                return dto;
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDTO dto) 
        {
            int tran_id;
            try
            {
                var tran = _mapper.Map<CreateTransaction>(dto);
                tran.Date = DateTime.Now;
                tran_id = await _transactionService.Create(tran);
            }
            catch (AutoMapperMappingException ex) when (ex.MemberMap.ToString() == nameof(dto.Type))
            {
                return BadRequest(new { message = "Invalid transaction type. Valid types are 'payment' and 'credit'" });
            }
            catch (ArgumentException ex) when (ex.ParamName == nameof(dto.CardId))
            {
                return BadRequest(new { message = "No such card exists" });
            }
            catch (ArgumentException ex) when (ex.ParamName == nameof(dto.Sum))
            {
                return BadRequest(new { message = "The transaction sum is invalid" });
            }
            catch (DbUpdateException) 
            {
                return BadRequest(new { message = "Invalid transaction" });
            }
            
            return StatusCode(201, tran_id);
        }
    }
}
