using System.Data;
using System.Collections.Generic;
using Tut11Proj.Entities;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
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

        public async Task<Doctor> GetDoctor(int idDoctor)
        {
            var doc = await GetDoctorWhereId(idDoctor);
            if (doc == null) throw new ArgumentNullException("Doctor with given id not found");
            return doc;
        }

        public Task<Doctor> GetDoctorWhereId(int idDoctor)
        {
            var res = _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == idDoctor);
            return res;
        }

        public Task<Doctor> GetDocWhereEmail(string email)
        {
            var res = _context.Doctors.FirstOrDefaultAsync(d => d.Email == email);
            return res;
        }

        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            var doc = await GetDoctorWhereId(doctor.IdDoctor);
            if (doc != null) throw new ArgumentNullException("Doctor with given id already exists");

            var emailCheck = await GetDocWhereEmail(doctor.Email);
            if (emailCheck != null) throw new ArgumentException("This email is already taken");

            // var newDoctor = new Doctor
            // {
            //     IdDoctor = doctor.IdDoctor,
            //     FirstName = doctor.FirstName,
            //     LastName = doctor.LastName,
            //     Email = doctor.Email
            // };
            // _context.Entry(doctor).State= EntityState.Unchanged;
            _context.Entry(doctor).State = EntityState.Added;
            // _context.Entry(newDoctor.Precriptions).State= EntityState.Unchanged;
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task ModifyDoctor(int idDoctor, EditDoctorRequest request)
        {
            var doc = await GetDoctorWhereId(idDoctor);
            if (doc == null) throw new ArgumentNullException("Doctor with given id not found");
            var emailCheck = await GetDocWhereEmail(request.Email);
            if (emailCheck != null) throw new ArgumentException("This email is already taken");

            var doctor = doc;
            doctor.IdDoctor = idDoctor;
            if (request.FirstName != null) doctor.FirstName = request.FirstName;
            if (request.LastName != null) doctor.LastName = request.LastName;
            if (request.Email != null) doc.Email = request.Email;
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctor(int idDoctor)
        {
            var doctor = await GetDoctorWhereId(idDoctor);
            if (doctor == null) throw new ArgumentNullException("Doctor with given id not found");
            _context.Entry(doctor).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
