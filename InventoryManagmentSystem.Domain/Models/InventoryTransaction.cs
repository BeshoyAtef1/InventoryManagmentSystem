using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Models
{
   
    public class InventoryTransaction
    {
        public enum TransactionType
        {
            Add,
            Remove,
            Transfer,
            Adjustment

        }
        public int Id { get; set; }
        public TransactionType transactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
        public int? SourceWarehouseId { get; set; }
        public int? DestinationWarehouseId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(SourceWarehouseId))]
        public Warehouse SourceWarehouse { get; set; }


        [ForeignKey(nameof(DestinationWarehouseId))]
        public Warehouse DestinationWarehouse { get; set; }


        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }


    }
}
