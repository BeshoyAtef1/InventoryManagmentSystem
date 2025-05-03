using InventoryManagmentSystem.Business.DTO.Report;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Interfaces
{
    public interface IReportServices
    {
        public GenaricResponse<IEnumerable<LowStockReportDto>> LowStockReport(int Page = 1, int PageSize = 10);
        public GenaricResponse<IEnumerable<TransactionHistoryDto>> TransactionHistoryWithFilter(Expression<Func<InventoryTransaction, bool>> filter, int Page = 1, int PageSize = 10);


    }
}
