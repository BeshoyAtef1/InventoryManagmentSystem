using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {


        public ICollection<InventoryTransaction> inventoryTransactions { get; set; }

    }
}
