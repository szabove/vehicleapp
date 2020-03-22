using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly VehicleContext context;

        public VehicleModelRepository(VehicleContext _context)
        {
            context = _context;
        }

        public void Add(VehicleModel vehicleMake)
        {
            context.VehicleModel.Add(vehicleMake);
        }

        public void Delete(VehicleModel vehicleMake)
        {
            context.VehicleModel.Remove(vehicleMake);
        }

        public VehicleModel Get(Guid id)
        {
            return context.VehicleModel.Find(id);
        }

        public IEnumerable<VehicleModel> GetAll()
        {
            return context.VehicleModel.ToArray();
        }

        public void Update(VehicleModel vehicleMake)
        {
            context.VehicleModel.AddOrUpdate(vehicleMake);
        }

    }
}
