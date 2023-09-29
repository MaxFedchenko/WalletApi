using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletApi.Model.DTOs;
using WalletApi.Model.Services;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService usersService;

        public UserController(IUserService usersService) 
        {
            this.usersService = usersService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserDTO user) 
        {
            var user_id = await usersService.Create(user.Name);

            return StatusCode(201, user_id);
        }
    }
}
