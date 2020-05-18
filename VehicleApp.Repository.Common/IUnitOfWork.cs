using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleMakeRepository Makes { get; set; }
        IVehicleModelRepository Models { get; set; }
        Task<int> CommitAsync();
    }
}
