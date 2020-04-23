using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Services.Common
{
    public interface IPaginationService<T> where T : class
    {
        ICollection<T> PaginatedResult(ICollection<T> data, IPagination pagination); //Task<ICollection<PaginatedResponse>>

    }
}
