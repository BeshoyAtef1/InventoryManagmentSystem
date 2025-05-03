using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.DataAccess.Interfaces
{
    public interface IGenericRepository<T>
    {
         Task AddAsync(T entity);

         Task<bool> DeleteAsync(Expression<Func<T, bool>> Predicate);

         IQueryable<T> GetAllWithFilter(Expression<Func<T, bool>> expression);


        IQueryable<T> GetAll();

        Task<bool> UpdateAsync(Expression<Func<T, bool>> Predicate, T entity);





    }
}
