using InventoryManagmentSystem.Business.DTO.Product;
using InventoryManagmentSystem.Business.DTO.Transaction;
using InventoryManagmentSystem.Business.ErrorCode;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.DataAccess.UnitOfWork;
using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenaricResponse<string>> AddStockAsync(AddTransactionDto addDto)
        {
            try
            {
                ProductWarehouse productWarehouse = _unitOfWork.ProductWarehouseRepo
               .GetAllWithFilter(PW => PW.ProductId == addDto.ProductId && PW.WarehouseId == addDto.WarehouseId)
               .FirstOrDefault();

                if (productWarehouse == null)
                {
                    return new GenaricResponse<string> { Success = false, Data = "Product not found in this warehouse" };
                }

                productWarehouse.Quantity += addDto.Quantity;

                InventoryTransaction transaction = new InventoryTransaction()
                {
                    Quantity = addDto.Quantity,
                    Date = DateTime.Now,
                    SourceWarehouseId = addDto.WarehouseId,
                    ProductId = addDto.ProductId,
                    UserId = addDto.UserId,
                    transactionType = TransactionType.Add,
                };

                await _unitOfWork.InventoryTransactionRepo.AddAsync(transaction);
                await _unitOfWork.SaveAsync();

                return new GenaricResponse<string> { Success = true, Data = "Stock added successfully" };

            }

            catch (Exception ex)
            {
                return new GenaricResponse<string> { Success = false, Message = ex.Message };

            }

        }


        public async Task<GenaricResponse<string>> RemoveStockAsync(DeleteTransactionDto DeleteDto)
        {
            try
            {
                ProductWarehouse productWarehouse = _unitOfWork.ProductWarehouseRepo
                    .GetAllWithFilter(PW => PW.ProductId == DeleteDto.ProductId && PW.WarehouseId == DeleteDto.WarehouseId)
                    .FirstOrDefault();

                if (productWarehouse == null)
                {
                    return new GenaricResponse<string> { Success = false, Data = "Product not found in this warehouse" };
                }

                if (productWarehouse.Quantity < DeleteDto.Quantity)
                {
                    return new GenaricResponse<string> { Success = false, Data = "Not enough to remove" };
                }

                productWarehouse.Quantity -= DeleteDto.Quantity;

                InventoryTransaction transaction = new InventoryTransaction()
                {
                    Quantity = DeleteDto.Quantity,
                    Date = DateTime.Now,
                    SourceWarehouseId = DeleteDto.WarehouseId,
                    ProductId = DeleteDto.ProductId,
                    UserId = DeleteDto.UserId,
                    transactionType = TransactionType.Remove,
                };

                await _unitOfWork.InventoryTransactionRepo.AddAsync(transaction);
                await _unitOfWork.SaveAsync();

                return new GenaricResponse<string> { Success = true, Data = "Stock remove successfully" };
            }
            catch (Exception ex)
            {
                return new GenaricResponse<string>{ Success = false ,Message = ex.Message};
            }

        }


        public async Task<GenaricResponse<string>> TransferStockAsync(TransferTransactionDto TransferDto)
        {
            try
            {
                if(TransferDto.SourceWarehouseId== TransferDto.DestinationWarehouseId)
                {
                    return new GenaricResponse<string> { Success = false, Message = "You can't transfer items from a warehouse to itself" };

                }

                ProductWarehouse productSourceWarehouse = _unitOfWork.ProductWarehouseRepo
                    .GetAllWithFilter(PW => PW.ProductId == TransferDto.ProductId && PW.WarehouseId == TransferDto.SourceWarehouseId)
                    .FirstOrDefault();

                ProductWarehouse productDestinationWarehouse = _unitOfWork.ProductWarehouseRepo
                 .GetAllWithFilter(PW => PW.ProductId == TransferDto.ProductId && PW.WarehouseId == TransferDto.DestinationWarehouseId)
                 .FirstOrDefault();

                if (productSourceWarehouse == null  || productDestinationWarehouse ==null)
                {
                    return new GenaricResponse<string> { Success = false, Data = "Product not found in this warehouse" };
                }

                if (productSourceWarehouse.Quantity < TransferDto.Quantity)
                {
                    return new GenaricResponse<string> { Success = false, Data = "Not enough to remove in product Source Warehouse" };
                }

                productSourceWarehouse.Quantity -= TransferDto.Quantity;
                productDestinationWarehouse.Quantity += TransferDto.Quantity;

                

                InventoryTransaction transaction = new InventoryTransaction()
                {
                    Quantity = TransferDto.Quantity,
                    Date = DateTime.Now,
                    SourceWarehouseId = TransferDto.SourceWarehouseId,
                    DestinationWarehouseId = TransferDto.DestinationWarehouseId,
                    ProductId = TransferDto.ProductId,
                    UserId = TransferDto.UserId,
                    transactionType = TransactionType.Transfer,
                };

                await _unitOfWork.InventoryTransactionRepo.AddAsync(transaction);
                await _unitOfWork.SaveAsync();

                return new GenaricResponse<string> { Success = true, Data = "Stock Transfer successfully" };
            }
            catch (Exception ex)
            {
                return new GenaricResponse<string> { Success = false, Message = ex.Message };
            }

        }
    }
}
