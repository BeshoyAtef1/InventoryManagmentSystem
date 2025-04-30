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
    public class WarehouseRepo : GenericRepository<Warehouse>  , IWarehouseRepo
    {
        private readonly AppDbContext appDbContext;

        public WarehouseRepo(AppDbContext appDbContext): base(appDbContext) {
            
            this.appDbContext = appDbContext;       
        } 
    
    }
}
