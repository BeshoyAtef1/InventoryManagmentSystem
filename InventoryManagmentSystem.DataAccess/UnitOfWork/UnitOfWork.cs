using InventoryManagmentSystem.DataAccess.Data;
using InventoryManagmentSystem.DataAccess.Interfaces;
using InventoryManagmentSystem.DataAccess.Repositories;

using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public IArchiveTransactionRepo ArchiveTransactionRepo { get; }
        public IInventoryTransactionRepo InventoryTransactionRepo { get; }
        public INotificationRepo NotificationRepo { get; }
        public IProductRepo ProductRepo { get; }
        public IProductWarehouseRepo ProductWarehouseRepo { get; }
        public IUserRepo UserRepo { get; }
        public IWarehouseRepo WarehouseRepo { get; }

        public UnitOfWork(AppDbContext appDbContext) {
            _appDbContext = appDbContext;

            ArchiveTransactionRepo = new ArchiveTransactionRepo(_appDbContext);
            InventoryTransactionRepo = new InventoryTransactionRepo(_appDbContext);
            NotificationRepo = new NotificationRepo(_appDbContext);
            ProductRepo = new ProductRepo(_appDbContext);
            ProductWarehouseRepo = new ProductWarehouseRepo(_appDbContext);
            UserRepo = new UserRepo(_appDbContext);
            WarehouseRepo = new WarehouseRepo(_appDbContext);



        }

        public async void Dispose()
        {
            await _appDbContext.DisposeAsync();
        }
        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
