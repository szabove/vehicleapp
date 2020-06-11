using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.Common
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<int> AddEntityAsync(T entity);
        Task<T> GetEntityAsync(Guid id);
        Task<int> UpdateEntityAsync(Guid id, T entity);
        Task<int> DeleteEntityAsync(T entity);
        Task<int> DeleteEntityAsync(Guid id);
    }
}
