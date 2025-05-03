using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<ProductWarehouse> ProductWarehouses { get; set; }
        public ICollection<InventoryTransaction> SourceTransactions { get; set; }
        public ICollection<InventoryTransaction> DestinationTransactions { get; set; }
        public ICollection<NotificationLog> NotificationLogs { get; set; }



    }
}
