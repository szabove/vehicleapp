﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;
using System.Linq.Expressions;
using LinqKit;

namespace VehicleApp.Common
{
    public class MakeSorter : ISorter<IVehicleMake>
    {
        public MakeSorter()
        {
            sortBy = "name";
            sortDirection = "asc";
        }

        public string sortBy { get; set; }
        public string sortDirection { get; set; }

        public ICollection<IVehicleMake> SortData(ICollection<IVehicleMake> dataToSort, Expression<Func<IVehicleMake, dynamic>> sortQuery )
        {
            sortDirection.ToLower();

            switch (sortDirection)
            {
                case "asc":
                    return dataToSort.AsQueryable().OrderBy(sortQuery).ToList();
                case "desc":
                    return dataToSort.AsQueryable().OrderByDescending(sortQuery).ToList();
                default:
                    return dataToSort.AsQueryable().OrderBy(sortQuery).ToList();
            }
        }

        public Expression<Func<IVehicleMake, dynamic>> GetSortQuery()
        {
            sortBy.ToLower();
            switch (sortBy)
            {
                case "name":
                    Expression<Func<IVehicleMake, dynamic>> _sortByName = x => x.Name;
                    return _sortByName;
                case "abrv":
                    Expression<Func<IVehicleMake, dynamic>> _sortByAbrv = x => x.Abrv;
                    return _sortByAbrv;
                default:
                    return null;
            }
        }

    }
}