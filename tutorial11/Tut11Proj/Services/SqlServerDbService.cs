using System.Data;
using System.Collections.Generic;
using Tut11Proj.Entities;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Tut11Proj.DTOs.Responses;
using Tut11Proj.DTOs.Requests;

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

        public Task<Doctor> GetDoctor(int idDoctor)
        {
            return _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == idDoctor);
        }

        public async Task<Doctor> GetDocWithEmail(string email)
        {
            var doc = await _context.Doctors.Where(d => d.Email == email).FirstOrDefaultAsync();
            return doc;
        }

        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            // var doc = await GetDoctor(doctor.IdDoctor);
            if (doctor != null) throw new ArgumentNullException("Doctor with given id already exists");

            var emailCheck = doctor.Email;
            if (emailCheck != null) throw new ArgumentException("This email is already taken");

            // var newDoctor = new Doctor
            // {
            //     IdDoctor = doctor.IdDoctor,
            //     FirstName = doctor.FirstName,
            //     LastName = doctor.LastName,
            //     Email = doctor.Email
            // };
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task ModifyDoctor(Doctor doctor)
        {
            if (doctor == null) throw new ArgumentNullException("Doctor with given id not found");
            var emailCheck = GetDocWithEmail(doctor.Email);
            if (emailCheck != null) throw new ArgumentException("This email is already taken");
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctor(int idDoctor)
        {
            var doctor = await GetDoctor(idDoctor);
            if (doctor == null) throw new ArgumentNullException("Doctor with given id not found");
            _context.Entry(doctor).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
