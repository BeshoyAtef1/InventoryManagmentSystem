using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO.Report
{
    public class TransactionHistoryDto
    {
        public string transactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }

        public string? SourceWarehouse { get; set; }
        public string? DestinationWarehouse { get; set; }


    }
}
