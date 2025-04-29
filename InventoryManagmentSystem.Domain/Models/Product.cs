using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public int Price { get; set; }

        public ICollection<ProductWarehouse> ProductHouses { get; set; }
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; }
        public ICollection<NotificationLog> NotificationLogs { get; set; }

    }
}
