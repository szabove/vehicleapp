﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters.Contracts
{
    public interface IFilterFacade
    {
        IMakeFilter CreateMakeFilter();
        IModelFilter CreateModelFilter();
        ISorter CreateSorter();
        IPagination CreatePagination();
    }
}