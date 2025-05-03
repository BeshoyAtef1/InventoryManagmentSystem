using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO.Transaction
{
    public class TransferTransactionDto
    {
        public int ProductId { get; set; }
        public int SourceWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }



        public int Quantity { get; set; }


    }
}
