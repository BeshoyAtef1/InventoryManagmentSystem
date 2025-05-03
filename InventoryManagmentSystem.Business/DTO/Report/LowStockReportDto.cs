using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.DTO.Report
{
    public class LowStockReportDto
    {
        public int Quantity {  get; set; }
        public int LowStockThreshold { get; set; }

        public string ProductName { get; set; }
        public string WarehouseName { get; set; }


    }
}
