using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tut11Proj.Entities;

namespace Tut11Proj.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Doctor>> ListDoctors();

    }
}