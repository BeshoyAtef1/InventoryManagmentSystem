using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Models
{
    public class ProductWarehouse
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }


        [Key, Column(Order = 1)]
        public int WarehouseId { get; set; }


        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }


        [Range(0, int.MaxValue)]
        public int LowStockThreshold { get; set; }


        [ForeignKey(nameof(WarehouseId))]
        public Product Product { get; set; }


        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }
    }
}
