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

        public UnitOfWork(IVehicleContext databaseContext,
                            IVehicleMakeRepository _makes,
                            IVehicleModelRepository _models
                            )
        {
            DatabaseContext = databaseContext;
            Makes = _makes;
            Models = _models;
        }

        public IVehicleMakeRepository Makes { get; set; }
        public IVehicleModelRepository Models { get; set; }
              
        
        public async Task<int> CommitAsync()
        {
            try
            {
                return await DatabaseContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void Dispose()
        {
            DatabaseContext.Dispose();
        }
    }
}
