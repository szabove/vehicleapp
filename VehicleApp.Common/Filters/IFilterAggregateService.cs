﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters
{
    public interface IFilterAggregateService
    {
        IMakeFilter MakeFilter { get;}
        IModelFilter ModelFilter { get; }
        IPagination Pagination { get; }
        ISorter Sorter { get; }
    }
}
