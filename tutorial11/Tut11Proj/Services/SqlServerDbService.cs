using System.Data;
using System.Collections.Generic;
using Tut11Proj.Entities;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Tut11Proj.Services
{
    public class SqlServerDbService : IDbService
    {
        private readonly s18827DbContext _context;

        public SqlServerDbService(s18827DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> ListDoctors()
        {
            var doctorsList = await _context.Doctors.ToListAsync();
            return doctorsList;
        }

    }
}
