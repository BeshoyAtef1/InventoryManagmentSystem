using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Models
{
    public class User
    {
       public string id { get; set; }

        public string Name { get; set; }

        public ICollection<InventoryTransaction> inventoryTransactions { get; set; }
        public ICollection<ArchiveTransaction> ArchivedTransactions { get; set; }

    }
}
