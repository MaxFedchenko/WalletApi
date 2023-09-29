using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalletApi.Core.Enums;
using WalletApi.Model.DTOs;
using WalletApi.Model.Services;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public TransactionController(IConfiguration configuration, ITransactionService transactionService, IMapper mapper)
        {
            this.transactionService = transactionService;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        private void IfNoIconAddDefault(TransactionDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Icon))
                dto.Icon = configuration.GetValue<string>("DefaultTransactionIcon")!;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [Required] int user_id) 
        {
            var transaction = await transactionService.GetDetails(id, user_id);
            if (transaction == null) return NotFound();

            var dto = mapper.Map<TransactionDetailsDTO>(transaction);
            IfNoIconAddDefault(dto);
            return Ok(dto); 
        }

        [HttpGet]
        public async Task<IActionResult> GetRange([Required] int user_id, int offset = 0, int amount = 10) 
        {
            var transactions = await transactionService.GetRange(offset, amount, user_id);

            return Ok(transactions.Select(t => 
            {
                var dto = mapper.Map<TransactionDTO>(t);
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
                var tran = mapper.Map<CreateTransaction>(dto);
                tran.Date = DateTime.Now;
                tran_id = await transactionService.Create(tran, dto.UserId);
            }
            catch (ArgumentException ex) 
            {
                if (ex.ParamName == "user_id")
                    return BadRequest(new { message = "No such user exists" });
                else if (ex.ParamName == "Sum")
                    return BadRequest(new { message = "The transaction is invalid as it falls outside the acceptable range" });
                else throw ex;
            }

            return StatusCode(201, tran_id);
        }
    }
}
