using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO.Product
{
    public class GetAllProductDto
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public int Price { get; set; }

        public List<ProductWarehouseDto> productWarehouses { get; set; }
    }
}
