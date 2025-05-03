using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO.Product
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public int Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue)]
        public int LowStockThreshold { get; set; }

        public int WarehouseId { get; set; } 
    }
}
