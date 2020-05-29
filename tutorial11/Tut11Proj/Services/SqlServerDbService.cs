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

        // public async Task<string> GetDoctorEmail(int idDoctor)
        // {
        //     var doc = await _context.Doctors.Where(d => d.IdDoctor == idDoctor).FirstOrDefaultAsync();
        //     return doc.Email;
        // }

        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            // var doc = await GetDoctor(doctor.IdDoctor);
            if (doctor != null) throw new ArgumentNullException("Doctor with given id already exists");

            var emailCheck = doctor.Email;
            if (emailCheck != null) throw new ArgumentException("This email is already taken");

            var newDoctor = new Doctor
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return newDoctor;
        }

        public async Task ModifyDoctor(Doctor doctor)
        {
            // var stud = await GetDoctor(doctor.IdDoctor);
            if (doctor == null) throw new ArgumentNullException("Student with given index number not found");

            var emailCheck = doctor.Email;
            if (emailCheck != null) throw new ArgumentException("This email is already taken");

            var modifDoc = doctor;
            student.IndexNumber = indexNumber;
            if (request.FirstName != null) student.FirstName = request.FirstName;
            if (request.LastName != null) student.LastName = request.LastName;
            if (request.BirthDate != null) student.BirthDate = (DateTime)request.BirthDate;
            if (request.IdEnrollment != null) student.IdEnrollment = (int)request.IdEnrollment;

            // dbContext.Student.Attach(student); // not needed since operating on one instance of Student
            // bc var student = stud; <- and stud was "invoked to the moemory" prior to that
            dbContext.Entry(student).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveStudent(string indexNumber)
        {
            var stud = await GetStudentByPK(indexNumber);
            if (stud == null) throw new ArgumentNullException("Student with given index number not found");
            // var student = dbContext.Student.Find(indexNumber);
            var student = stud;
            // dbContext.Student.Attach(student);// not needed since var student = stud <- stud was invoked to be checked prior to like a focus is set to it
            // this way I don't have to find/download from db agian the object I want to delete before actually deleting it
            // dbContext.Remove(student); // below line is instead of this line
            dbContext.Entry(student).State = EntityState.Deleted; // now we can use ChangeTracker to track all changes of the state of this object when Debugging
            await dbContext.SaveChangesAsync();
        }

    }
}
