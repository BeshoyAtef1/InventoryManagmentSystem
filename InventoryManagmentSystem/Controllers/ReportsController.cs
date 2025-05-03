using InventoryManagmentSystem.Business.DTO.Product;
using InventoryManagmentSystem.Business.DTO.Report;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportServices _reportService;

        public ReportsController(IReportServices reportService)
        {
            _reportService = reportService;
        }

        [Authorize]
        [HttpGet("LowStockReport/{Page:int}")]
        public IActionResult LowStockReport(int Page)
        {
            GenaricResponse<IEnumerable<LowStockReportDto>> LowStockReport = _reportService.LowStockReport(Page);

            if (LowStockReport.Success)
            {
                return Ok(LowStockReport);
            }
            return BadRequest(LowStockReport);
        }

        [HttpGet("TransactionHistoryByProductId/{id:int}/{Page:int}")]
        public IActionResult TransactionHistoryByProductId(int id ,int Page)
        {
            GenaricResponse<IEnumerable<TransactionHistoryDto>> TransactionHistoryDto =
                _reportService.TransactionHistory(x=>x.ProductId==id ,Page);

            if (TransactionHistoryDto.Success)
            {
                return Ok(TransactionHistoryDto);
            }
            return BadRequest(TransactionHistoryDto);
        }

        [HttpGet("TransactionHistoryByDate/{Page:int}")]
        public IActionResult TransactionHistoryByDate(TransactionHistoryByDateDto Date, int Page)
        {
            GenaricResponse<IEnumerable<TransactionHistoryDto>> TransactionHistoryDto = 
                _reportService.TransactionHistory(x => x.Date >= Date.FromDate && x.Date<=Date.ToDate, Page);

            if (TransactionHistoryDto.Success)
            {
                return Ok(TransactionHistoryDto);
            }
            return BadRequest(TransactionHistoryDto);
        }
    }
}
