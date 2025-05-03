using InventoryManagmentSystem.Business.DTO.Account;
using InventoryManagmentSystem.Business.DTO.Product;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Services
{
    public class AccountServices : IAccoutSevices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountServices(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

        }

        public async Task<IdentityResult> Register(RegisterDto UserFromConsumer)
        {
          
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = UserFromConsumer.UserName,
                    Email = UserFromConsumer.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(user, UserFromConsumer.Password);
       
                return result;

        }

        public async Task<GenaricResponse<TokenDto>> Login(LoginDto UserFromConsumer)
        {
            ApplicationUser user= await _userManager.FindByNameAsync(UserFromConsumer.UserName);

            if (user != null)
            {

                bool found = await _userManager.CheckPasswordAsync(user, UserFromConsumer.Password);

                if (found)
                {

                    IList<string> UserRole = await _userManager.GetRolesAsync(user);




                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    string jti = Guid.NewGuid().ToString();
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, jti));
                    if(UserRole != null)
                    {
                        foreach (var role in UserRole)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));

                        }
                    }

                    SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                    SigningCredentials signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken jwtToken = new JwtSecurityToken(
                        issuer: _configuration["JWT:Iss"],
                        audience: _configuration["JWT:Aud"],
                        expires : DateTime.UtcNow.AddHours(1),
                        claims: claims,
                        signingCredentials : signingCredentials
                        );

                    TokenDto tokenDto = new TokenDto()
                    {
                        Expired = DateTime.UtcNow.AddHours(1),
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken)

                    };



                    if (user.UserName == "Admin") 
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    return new GenaricResponse<TokenDto> { Success = true, Data = tokenDto };

                }
            }
            return new GenaricResponse<TokenDto> { Success  = false , Message = "Invalid Account"} ;
        }
    }
}
