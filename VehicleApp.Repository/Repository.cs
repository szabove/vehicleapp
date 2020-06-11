using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        IVehicleContext DatabaseContext;
        IUnitOfWork UnitOfWork;
                
        public Repository(IVehicleContext databaseContext, IUnitOfWork unitOfWork)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException();
            }
            DatabaseContext = databaseContext;
            UnitOfWork = unitOfWork;
        }

        public async Task<int> AddEntityAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            entity.SetDateCreated();

            var addAsyncResult = await UnitOfWork.AddUoWAsync<T>(entity);
            if (addAsyncResult == 0)
            {
                return 0;
            }
            return await UnitOfWork.CommitAsync();
        }

        public async Task<int> DeleteEntityAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }
            var deleteAsyncResult = await UnitOfWork.DeleteUoWAsync<T>(id);
            if (deleteAsyncResult == 0)
            {
                return 0;
            }
            return await UnitOfWork.CommitAsync();
        }

        public async Task<int> DeleteEntityAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            var deleteAsyncResult = await UnitOfWork.DeleteUoWAsync<T>(entity);
            if (deleteAsyncResult == 0)
            {
                return 0;
            }
            return await UnitOfWork.CommitAsync();
        }

        public async Task<T> GetEntityAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            try
            {
                return await DatabaseContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateEntityAsync(Guid id, T entity) 
        {
            if (id == Guid.Empty || entity == null)
            {
                throw new ArgumentNullException();
            }

            var entityFromGetAsyncResult = await GetEntityAsync(id);
            if (entityFromGetAsyncResult == null)
            {
                return 0;
            }

            entity.DateUpdated = DateTime.Now;
            DatabaseContext.Entry(entityFromGetAsyncResult).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(entityFromGetAsyncResult).Property(x => x.DateCreated).IsModified = false;

            var updateAsyncResult = await UnitOfWork.UpdateUoWAsync<T>(entityFromGetAsyncResult);
            if (updateAsyncResult == 0)
            {
                return 0;
            }

            return await UnitOfWork.CommitAsync();
        }
    }
}
