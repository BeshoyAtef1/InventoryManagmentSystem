using InventoryManagmentSystem.Business.DTO.Account;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccoutSevices _accountServices;
        public AccountController(IAccoutSevices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto UserFromConsumer)
        {
            if (ModelState.IsValid)
            {
                IdentityResult ResultResponse = await _accountServices.Register(UserFromConsumer);
                
                if(ResultResponse.Succeeded)
                {
                    return Ok("Account created");
                }
                foreach (var item in ResultResponse.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }

            return BadRequest(ModelState);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginDto UserFromConsumer)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }

            GenaricResponse<TokenDto> ResultResponse = await _accountServices.Login(UserFromConsumer);
            if(ResultResponse.Success)
            {
                return Ok(ResultResponse);
            }
            return BadRequest(ResultResponse);


        }

    }
}
