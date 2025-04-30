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
        public Task AddAsync(T entity);

        public Task<bool> Delete(Expression<Func<T, bool>> Predicate);

        public IQueryable<T> GetAllWithFilter(Expression<Func<T, bool>> expression);

        public Task<bool> UpdateAsync(Expression<Func<T, bool>> Predicate, T entity);





    }
}
