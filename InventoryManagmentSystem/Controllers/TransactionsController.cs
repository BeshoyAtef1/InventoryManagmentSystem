using InventoryManagmentSystem.Business.DTO.Product;
using InventoryManagmentSystem.Business.DTO.Transaction;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionServices _TransactionServices;

        public TransactionsController(ITransactionServices TransactionServices)
        {
            _TransactionServices= TransactionServices;
        }
        [HttpPost("AddStock")]
        public async Task<IActionResult> AddStock(AddTransactionDto addDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GenaricResponse<string> AddResponse = await _TransactionServices.AddStockAsync(addDto);

            if (AddResponse.Success)
            {
                return Ok(AddResponse);
            }
            return BadRequest(AddResponse);
        }
        [HttpPost("RemoveStock")]
        public async Task<IActionResult> RemoveStock(DeleteTransactionDto RemoveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GenaricResponse<string> RemoveResponse = await _TransactionServices.RemoveStockAsync(RemoveDto);

            if (RemoveResponse.Success)
            {
                return Ok(RemoveResponse);
            }
            return BadRequest(RemoveResponse);
        }
        [HttpPost("TransferStock")]
        public async Task<IActionResult> TransferStock(TransferTransactionDto TransferDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GenaricResponse<string> TransferResponse = await _TransactionServices.TransferStockAsync(TransferDto);

            if (TransferResponse.Success)
            {
                return Ok(TransferResponse);
            }
            return BadRequest(TransferResponse);
        }
    }
}
