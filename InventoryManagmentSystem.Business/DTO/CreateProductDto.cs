using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public int Price { get; set; }

    }
}
