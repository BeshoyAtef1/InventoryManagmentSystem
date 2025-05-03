using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO.Report
{
    public class TransactionHistoryByDateDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
