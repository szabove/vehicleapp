using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VehicleApp.DAL;
using VehicleApp.Repository.Common;
using System.Data.Entity;

namespace VehicleApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        IVehicleContext DatabaseContext;

        public UnitOfWork(IVehicleContext databaseContext)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException();
            }
            DatabaseContext = databaseContext;
        }

        public Task<int> AddUoWAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DatabaseContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    DatabaseContext.Set<T>().Add(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<int> DeleteUoWAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DatabaseContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    DatabaseContext.Set<T>().Attach(entity);
                    DatabaseContext.Set<T>().Remove(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> DeleteUoWAsync<T>(Guid id) where T : class
        {
            var entity = DatabaseContext.Set<T>().Find(id);
            if (entity == null)
            {
                return Task.FromResult(0);
            }
            return DeleteUoWAsync<T>(entity);
        }

        public Task<int> UpdateUoWAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DatabaseContext.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    DatabaseContext.Set<T>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await DatabaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            DatabaseContext.Dispose();
        }

    }
}
