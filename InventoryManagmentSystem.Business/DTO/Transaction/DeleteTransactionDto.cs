using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO.Transaction
{
    public class DeleteTransactionDto
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }


    }
}
