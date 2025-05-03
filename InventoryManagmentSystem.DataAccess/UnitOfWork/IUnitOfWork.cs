using InventoryManagmentSystem.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IArchiveTransactionRepo ArchiveTransactionRepo {  get; }

        public IInventoryTransactionRepo InventoryTransactionRepo { get; }


        public IProductRepo ProductRepo { get; }

        public IProductWarehouseRepo ProductWarehouseRepo { get; }

        public IUserRepo UserRepo { get; }

        public IWarehouseRepo WarehouseRepo { get; }
        public Task SaveAsync();


    }
}
