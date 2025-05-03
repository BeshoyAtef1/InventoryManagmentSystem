using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.Business.Services;
using System.Threading.Tasks;
using InventoryManagmentSystem.Domain.Models;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.DTO.Product;
using Microsoft.AspNetCore.Authorization;


namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto ProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GenaricResponse<ProductDto> AddResponse = await _productServices.AddAsync(ProductDto);

            if (AddResponse.Success)
            {
                return Ok(AddResponse);
            }
            return BadRequest(AddResponse);
            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GenaricResponse<ProductDto> EditResponse = await _productServices.UpdateAsync(id, productDto);

            if (EditResponse.Success)
            {
                return Ok(EditResponse);
            }
            return BadRequest(EditResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{ProductId:int}/{WarehouseId:int}")]
        public async Task<IActionResult> DeleteById([FromRoute]ProductWarehouseIDDto deleteProductDto)
        {
            GenaricResponse<ProductWarehouseIDDto> DeleteResponse = await _productServices.DeleteAsync(deleteProductDto);

            if (DeleteResponse.Success)
                return Ok(DeleteResponse);
            return BadRequest(DeleteResponse);
        }

        [HttpGet("{ProductId:int}/{WarehouseId:int}")]
        public IActionResult GetById([FromRoute] ProductWarehouseIDDto GetProductDto)
        {
            GenaricResponse<ProductDto> GetProductWithWarehouse =  _productServices.GetById(GetProductDto);

            if (GetProductWithWarehouse.Success)
                return Ok(GetProductWithWarehouse);

            return BadRequest(GetProductWithWarehouse);
        }

        [HttpGet]
        public IActionResult Get()
        {
            GenaricResponse<IEnumerable<GetAllProductDto>> products = _productServices.GetAll();
            if (products.Success)
                return Ok(products);

            return BadRequest(products);
        }
    }
}
