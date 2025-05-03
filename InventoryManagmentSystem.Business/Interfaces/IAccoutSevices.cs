using InventoryManagmentSystem.Business.DTO.Account;
using InventoryManagmentSystem.Business.ErrorCode;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Interfaces
{
    public interface IAccoutSevices
    {
        public Task<IdentityResult> Register(RegisterDto UserFromConsumer);

        public Task<GenaricResponse<TokenDto>> Login(LoginDto UserFromConsumer);


    }
}
