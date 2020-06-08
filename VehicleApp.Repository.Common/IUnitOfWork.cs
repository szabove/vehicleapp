using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> AddUoWAsync<T>(T entity) where T : class;
        Task<int> UpdateUoWAsync<T>(T entity) where T : class;
        Task<int> DeleteUoWAsync<T>(T entity) where T : class;
        Task<int> DeleteUoWAsync<T>(Guid id) where T : class;
        Task<int> CommitAsync();
    }
}
