using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Models
{
   
    public class archiveTransactionRepo
    {
        public enum TransactionType
        {
            Add,
            Remove,
            Transfer

        }
        public int Id { get; set; }
        public TransactionType transactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
        public int SourceWarehouseId { get; set; }
        public int? DestinationWarehouseId { get; set; }

    

    }
}
