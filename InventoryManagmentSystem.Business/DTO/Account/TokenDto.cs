using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO.Account
{
    public class TokenDto
    {
        public DateTime Expired { get; set; }
        public string Token { get; set; }
    }
}
