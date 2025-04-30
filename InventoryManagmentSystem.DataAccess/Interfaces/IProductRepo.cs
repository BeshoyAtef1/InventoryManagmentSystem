using InventoryManagmentSystem.DataAccess.Repositories;
using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.DataAccess.Interfaces
{
    public interface IProductRepo : IGenericRepository<Product>
    {
    }
}
