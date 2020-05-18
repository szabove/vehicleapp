using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Repository.Common
{
    public interface IRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<int> UpdateAsync(Guid id, T entity);
        Task<int> DeleteAsync(Guid id);
        Task<IEnumerable<T>> WhereQueryAsync(Expression<Func<T, bool>> expression);
    }
}
