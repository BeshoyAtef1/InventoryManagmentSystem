using InventoryManagmentSystem.DataAccess.Data;
using InventoryManagmentSystem.DataAccess.Interfaces;
using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.DataAccess.Repositories
{
    public class ArchiveTransactionRepo : GenericRepository<archiveTransactionRepo>, IArchiveTransactionRepo
    {
        private readonly AppDbContext appDbContext;
        public ArchiveTransactionRepo(AppDbContext appDbContext) : base(appDbContext) { 
            this.appDbContext = appDbContext;
        }
        
    }
}
