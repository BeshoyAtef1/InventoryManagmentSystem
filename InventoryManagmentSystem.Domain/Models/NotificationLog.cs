using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Models
{
    public class NotificationLog
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; } 

        [ForeignKey("Warehouse")]
        public int? WarehouseId { get; set; } 

        public string Message { get; set; }

        public DateTime Date { get; set; } 

        // Navigation properties
        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
