using Azure;
using InventoryManagmentSystem.Business.DTO.Product;
using InventoryManagmentSystem.Business.DTO.Report;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.DataAccess.UnitOfWork;
using InventoryManagmentSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Services
{
    public class ReportServices : IReportServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _MemoryCache;


        public ReportServices(IUnitOfWork unitOfWork , IMemoryCache MemoryCache)
        {
            _unitOfWork = unitOfWork;
            _MemoryCache = MemoryCache;
        }

        public GenaricResponse<IEnumerable<LowStockReportDto>> LowStockReport(int Page = 1, int PageSize = 10)
        {
            try
            {
                int skipCount = (Page - 1) * PageSize;

                List<LowStockReportDto> ProductWarehouses;

                _MemoryCache.TryGetValue("LowStockReport", out ProductWarehouses);

                if (ProductWarehouses == null)
                {
                    ProductWarehouses = _unitOfWork.ProductWarehouseRepo
                   .GetAll()
                   .Where(pw => pw.Quantity < pw.LowStockThreshold)
                   .Skip(skipCount)
                   .Take(PageSize)
                   .Select(pw => new LowStockReportDto
                   {
                       Quantity = pw.Quantity,
                       LowStockThreshold = pw.LowStockThreshold,
                       ProductName = pw.Product.Name,
                       WarehouseName = pw.Warehouse.Name
                   }).ToList();

                    _MemoryCache.Set("LowStockReport", ProductWarehouses,TimeSpan.FromHours(1) );

                }

                return new GenaricResponse<IEnumerable<LowStockReportDto>> { Success = true, Data = ProductWarehouses };

            }
            catch (Exception ex)
            {
                return new GenaricResponse<IEnumerable<LowStockReportDto>> { Success = false, Message=ex.Message };

            }

        }

        public GenaricResponse<IEnumerable<TransactionHistoryDto>> TransactionHistoryWithFilter
            (Expression<Func<InventoryTransaction, bool>> filter  ,int Page = 1 , int PageSize = 10)
        {

            try
            {
                int skipCount = (Page - 1) * PageSize;

                List<TransactionHistoryDto> transactionHistories = _unitOfWork.InventoryTransactionRepo
                    .GetAll()
                    .Where(filter)
                    .Skip(skipCount)
                    .Take(PageSize)
                    .Select(it => new TransactionHistoryDto
                    {

                        Quantity=it.Quantity,
                        Date = it.Date,
                        DestinationWarehouse=it.DestinationWarehouse.Name,
                        ProductName=it.Product.Name,
                        SourceWarehouse=it.SourceWarehouse.Name,    
                        transactionType=it.transactionType.ToString(),
                        UserName=it.User.UserName,  

                    }).ToList();


                return new GenaricResponse<IEnumerable<TransactionHistoryDto>> { Success = true, Data = transactionHistories };

            }
            catch (Exception ex)
            {
                return new GenaricResponse<IEnumerable<TransactionHistoryDto>> { Success = false, Message = ex.Message };

            }

        }
    }
}
