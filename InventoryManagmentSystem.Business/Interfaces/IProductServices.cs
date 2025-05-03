using InventoryManagmentSystem.Business.DTO.Product;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Interfaces
{
    public interface IProductServices
    {
        public GenaricResponse<IEnumerable<GetAllProductDto>> GetAll();
        public GenaricResponse<ProductDto> GetById(ProductWarehouseIDDto getProductDto);
        public Task<GenaricResponse<ProductDto>> AddAsync(ProductDto productDto);
        public Task<GenaricResponse<ProductDto>> UpdateAsync(int id, ProductDto productDto);
        public Task<GenaricResponse<ProductWarehouseIDDto>> DeleteAsync(ProductWarehouseIDDto deleteProductDto);




    }
}
