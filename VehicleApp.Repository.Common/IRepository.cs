﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.Common
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<int> AddAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<int> UpdateAsync(Guid id, T entity);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteAsync(Guid id);
    }
}
