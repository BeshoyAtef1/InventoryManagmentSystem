using InventoryManagmentSystem.Business.DTO.Product;
using InventoryManagmentSystem.Business.DTO.Transaction;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "User")]
        [HttpPost("AddStock")]
        public async Task<IActionResult> AddStock(AddTransactionDto addDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GenaricResponse<string> AddResponse = await _TransactionServices.AddStockAsync(addDto,userId);

            if (AddResponse.Success)
            {
                return Ok(AddResponse);
            }
            return BadRequest(AddResponse);
        }

        [Authorize(Roles = "User")]
        [HttpPost("RemoveStock")]
        public async Task<IActionResult> RemoveStock(DeleteTransactionDto RemoveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            GenaricResponse<string> RemoveResponse = await _TransactionServices.RemoveStockAsync(RemoveDto, userId);

            if (RemoveResponse.Success)
            {
                return Ok(RemoveResponse);
            }
            return BadRequest(RemoveResponse);
        }

       [Authorize(Roles = "User")]
        [HttpPost("TransferStock")]
        public async Task<IActionResult> TransferStock(TransferTransactionDto TransferDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GenaricResponse<string> TransferResponse = await _TransactionServices.TransferStockAsync(TransferDto , userId);

            if (TransferResponse.Success)
            {
                return Ok(TransferResponse);
            }
            return BadRequest(TransferResponse);
        }
    }
}
