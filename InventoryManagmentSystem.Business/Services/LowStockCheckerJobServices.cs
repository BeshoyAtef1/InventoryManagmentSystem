using InventoryManagmentSystem.Business.DTO.Report;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.DataAccess.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Services
{
    public class LowStockCheckerJobServices 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LowStockCheckerJobServices> _logger;   

        public LowStockCheckerJobServices(IUnitOfWork unitOfWork, ILogger<LowStockCheckerJobServices> logger  )
        {
            _unitOfWork = unitOfWork;   
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
           List<LowStockReportDto> LowStockReport =  _unitOfWork.ProductWarehouseRepo
                .GetAllWithFilter(pw => pw.Quantity < pw.LowStockThreshold)
                .Select(pw => new LowStockReportDto
                {
                    Quantity=pw.Quantity,
                    ProductName=pw.Product.Name,
                    LowStockThreshold=pw.LowStockThreshold,
                    WarehouseName=pw.Warehouse.Name
                }).ToList();
                

            if( LowStockReport.Count > 0 )
            {

                foreach( var lowStockReport in LowStockReport)
                {
                    _logger.LogWarning($"Product{lowStockReport.ProductName}has quantity {lowStockReport.Quantity}" +
                        $" below the minimum threshold {lowStockReport.LowStockThreshold} in {lowStockReport.WarehouseName}");
                } 
                    
            }
            else
            {
                _logger.LogInformation("No products are below the minimum threshold");
            }

        }
    }
}
