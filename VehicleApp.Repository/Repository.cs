using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        IVehicleContext DatabaseContext;
                
        public Repository(IVehicleContext context)
        {
            DatabaseContext = context;
        }

        public async Task<int> AddAsync(T entity) 
        {
            try
            {
                DatabaseContext.Set<T>().Add(entity);
                return await Task.FromResult(1);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                T entity = await GetAsync(id);
                DatabaseContext.Set<T>().Remove(entity);

                return await Task.FromResult(1);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        public async Task<T> GetAsync(Guid id)
        {
            return await DatabaseContext.Set<T>().FindAsync(id);
        }

        public async Task<int> UpdateAsync(Guid id, T entity) 
        {
            try
            {
                T _entity = await GetAsync(id);
                DatabaseContext.Entry(_entity).CurrentValues.SetValues(entity);
                return await Task.FromResult(1);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<T>> WhereQueryAsync(Expression<Func<T, bool>> expression) 
        {
            return await DatabaseContext.Set<T>().Where(expression).ToListAsync();
        }

    }
}
