using InventoryManagmentSystem.Business.DTO.Product;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.DataAccess.UnitOfWork;
using InventoryManagmentSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GenaricResponse<IEnumerable<GetAllProductDto>> GetAll()
        {
            try
            {
                List<GetAllProductDto> products = _unitOfWork.ProductRepo.GetAll()
                      .AsNoTracking()
                      .Select(p => new GetAllProductDto
                      {
                          ProductName = p.Name,
                          Description = p.Description,
                          Price = p.Price,
                          productWarehouses = p.ProductWarehouse.Select(pw => new ProductWarehouseDto
                          {
                              WarehouseName = pw.Warehouse.Name,
                              Quantity = pw.Quantity,
                              LowStockThreshold = pw.LowStockThreshold
                          }).ToList()
                      })
                      .ToList();
                return new GenaricResponse<IEnumerable<GetAllProductDto>> { Success = true, Data = products };
            }
            catch (Exception ex)
            {
                return new GenaricResponse<IEnumerable<GetAllProductDto>> { Success = false, Message = ex.Message };

            }
        }

        public GenaricResponse<ProductDto> GetById(ProductWarehouseIDDto getProductDto)
        {
            try
            {
                Product? product = _unitOfWork.ProductRepo
                    .GetAllWithFilter(p => p.Id == getProductDto.ProductId)
                    .FirstOrDefault();

                ProductWarehouse? productWarehouse =  _unitOfWork.ProductWarehouseRepo
                    .GetAllWithFilter(pw => pw.ProductId == getProductDto.ProductId && pw.WarehouseId == getProductDto.WarehouseId)
                    .FirstOrDefault();

                if (product == null || productWarehouse == null)
                {
                    return new GenaricResponse<ProductDto> { Success = false, Message = "Product or ProductWarehouse not found." };
                }

                ProductDto productDto = new ProductDto
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Quantity = productWarehouse.Quantity,
                    LowStockThreshold = productWarehouse.LowStockThreshold,
                    WarehouseId = productWarehouse.WarehouseId,
                };
                return new GenaricResponse<ProductDto> { Success = true, Data = productDto };

            }
            catch (Exception ex)
            {
                return new GenaricResponse<ProductDto> { Success = false, Message = ex.Message };

            }


        }

        public async Task<GenaricResponse<ProductDto>> AddAsync(ProductDto productDto)
        {
            try
            {
                Product product = new Product()
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                };

                await _unitOfWork.ProductRepo.AddAsync(product);
                await _unitOfWork.SaveAsync(); // take ProductId

                ProductWarehouse productWarehouse = new ProductWarehouse()
                {
                    LowStockThreshold = productDto.LowStockThreshold,
                    Quantity = productDto.Quantity,
                    ProductId = product.Id,
                    WarehouseId = productDto.WarehouseId
                };

                await _unitOfWork.ProductWarehouseRepo.AddAsync(productWarehouse);

                await _unitOfWork.SaveAsync();

                return new GenaricResponse<ProductDto> { Success = true, Data = productDto };

            }

            catch (Exception ex)
            {
                return new GenaricResponse<ProductDto> { Success = false, Data = productDto, Message = ex.Message };

            }



        }

        public async Task<GenaricResponse<ProductDto>> UpdateAsync(int id ,ProductDto productDto)
        {
            try
            {
                Product? product = _unitOfWork.ProductRepo.GetAllWithFilter(p => p.Id == id).FirstOrDefault();
                ProductWarehouse? productWarehouse =  _unitOfWork.ProductWarehouseRepo.GetAllWithFilter(pw => pw.ProductId == id && pw.WarehouseId == productDto.WarehouseId).FirstOrDefault();

                if (product == null || productWarehouse == null)
                {
                    return new GenaricResponse<ProductDto> { Success = false,  Data = productDto, Message = "Product or ProductWarehouse not found."};
                }

                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;

                productWarehouse.LowStockThreshold = productDto.LowStockThreshold;
                productWarehouse.Quantity = productDto.Quantity;

               await _unitOfWork.ProductRepo.UpdateAsync(p => p.Id == id,product);
               await _unitOfWork.ProductWarehouseRepo.UpdateAsync(pw => pw.ProductId == id && pw.WarehouseId == productDto.WarehouseId, productWarehouse);
               await _unitOfWork.SaveAsync();

                return new GenaricResponse<ProductDto> { Success = true, Data = productDto};

            }
            catch (Exception ex)
            {
                return new GenaricResponse<ProductDto> { Success = false, Data = productDto, Message = ex.Message };

            }
        }

        public async Task<GenaricResponse<ProductWarehouseIDDto>> DeleteAsync(ProductWarehouseIDDto deleteProductDto)
        {
            try
            {
                if (await _unitOfWork.ProductWarehouseRepo
                    .DeleteAsync(pw => pw.ProductId == deleteProductDto.ProductId && pw.WarehouseId == deleteProductDto.WarehouseId))
                {
                    await _unitOfWork.SaveAsync();
                    return new GenaricResponse<ProductWarehouseIDDto> { Success = true, Data = deleteProductDto };
                }

                return new GenaricResponse<ProductWarehouseIDDto> { Success = false, Data = deleteProductDto, Message = "ProductWarehouse not found." };
            }
            catch (Exception ex)
            {
                return new GenaricResponse<ProductWarehouseIDDto> { Success = false, Data = deleteProductDto, Message = ex.Message };

            }

        }

      
    }
}
