using InventoryManagmentSystem.DataAccess.Data;
using InventoryManagmentSystem.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _appDBContext;

        public GenericRepository(AppDbContext appDBContext)
        {
            _appDBContext=appDBContext; 
        }

        public async Task AddAsync(TEntity entity)
        {
            await _appDBContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> Predicate)
        {
            TEntity? result = await _appDBContext.Set<TEntity>().FirstOrDefaultAsync(Predicate);
            if (result is not null)
            {
                _appDBContext.Set<TEntity>().Remove(result);
                return true;
            }
            return false;
        }

        public IQueryable<TEntity> GetAllWithFilter(Expression<Func<TEntity, bool>> expression)
        {
            return _appDBContext.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _appDBContext.Set<TEntity>();
        }


        public async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> Predicate, TEntity entity) 
        {
            TEntity? existedItem = await _appDBContext.Set<TEntity>().FirstOrDefaultAsync(Predicate);
            if (existedItem is not null)
            {
                _appDBContext.Entry(existedItem).CurrentValues.SetValues(entity);
                return true;
            }
            return false;
        }



    }
}
