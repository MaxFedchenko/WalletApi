using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApi.DataAccess.EF;
using WalletApi.DataAccess.Entities;
using WalletApi.Model.DTOs;
using WalletApi.Model.Services;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService cardService;
        private readonly ICardPointsService cardPointsService;
        private readonly decimal cardLimit;

       public CardController(IConfiguration configuration, ICardService cardService, ICardPointsService cardPointsService)
        {
            this.cardService = cardService;
            this.cardPointsService = cardPointsService;
            cardLimit = configuration.GetValue<decimal>("CardLimit");
        }

        [HttpGet]
        public async Task<IActionResult> GetInfo([Required] int user_id) 
        {
            var card = await cardService.GetByUserId(user_id);
            if (card == null) return NotFound();

            int points = cardPointsService.GetCurrentPoints();

            return Ok(new CardInfoDTO
            {
                CardId = card.Id,
                Balance = card.Balance,
                Available = cardLimit - card.Balance,
                DailyPoints = points >= 1000 ? Math.Round(points / 1000.0).ToString("0K") : points.ToString(),
                Month = DateTime.Now.ToString("MMMM")
            });
        }
    }
}
