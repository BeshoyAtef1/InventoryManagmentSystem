using InventoryManagmentSystem.Business.DTO.Transaction;
using InventoryManagmentSystem.Business.ErrorCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Interfaces
{
    public interface ITransactionServices
    {
        public Task<GenaricResponse<string>> AddStockAsync(AddTransactionDto addDto);
        public Task<GenaricResponse<string>> RemoveStockAsync(DeleteTransactionDto DeleteDto);
        public Task<GenaricResponse<string>> TransferStockAsync(TransferTransactionDto TransferDto);


    }
}
