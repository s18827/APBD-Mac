using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Tut10Proj.Models;
using System.Linq;
using Tut10Proj.Models.DTOs.Requests;
using System;
using Tut10Proj.Models.DTOs.Responses;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tut10Proj.Services
{
    public class SqlServerDbService : IDbService
    {

        #region StudentsController
        public IEnumerable<Student> ListStudents(s18827Context dbContext)
        {
            var studentsList = dbContext.Student.ToList();
            return studentsList;
        }

        public async Task AddStudent(s18827Context dbContext, AddStudentRequest request)
        {
            var studFound = FoundStudentByPK(dbContext, request.IndexNumber);
            if (studFound) throw new ArgumentNullException("Student with given index number already exists in the db");

            var enrollFound = FoundEnrollmentByPK(dbContext, request.IdEnrollment);
            if (!enrollFound) throw new ArgumentException("Enrollment with given id not found");

            var student = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = request.IdEnrollment
            };
            dbContext.Student.Add(student);
            await dbContext.SaveChangesAsync();
        }

        // Attach is somehow in conflict with index number checking
        public async Task EditStudent(s18827Context dbContext, string indexNumber, EditStudentRequest request)
        {
            // var studFound = FoundStudentByPK(dbContext, indexNumber);
            // if (!studFound) throw new ArgumentNullException("Student with given index number not found");
            var student = new Student();
            student.IndexNumber = indexNumber;
            if (request.FirstName != null) student.FirstName = request.FirstName;
            if (request.LastName != null) student.LastName = request.LastName;
            if (request.BirthDate != null) student.BirthDate = (DateTime)request.BirthDate;
            if (request.IdEnrollment != null) student.IdEnrollment = (int)request.IdEnrollment;

            dbContext.Attach(student); // somehow in conflict with index number checking
            dbContext.Entry(student).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
        }

        // Attach is somehow in conflict with index number checking
        public async Task RemoveStudent(s18827Context dbContext, string indexNumber)
        {
            // var studFound = FoundStudentByPK(dbContext, indexNumber);
            // if (!studFound) throw new ArgumentNullException("Student with given index number not found");
            // var student = dbContext.Student.Find(indexNumber);
            var student = new Student { IndexNumber = indexNumber }; // somehow in conflict with index number checking
            dbContext.Attach(student); // this way I don't have to find/download from db agian the object I want to delete before actually deleting it
            // dbContext.Remove(student); // below insted of this
            dbContext.Entry(student).State = EntityState.Deleted; // now we can use ChangeTracker to track all changes of the state of this object when Debugging
            await dbContext.SaveChangesAsync();
        }

        public bool FoundStudentByPK(s18827Context dbContext, string indexNumber)
        {
            var res = dbContext.Student.Find(indexNumber);
            if (res != null)
            {
                return true;
            }
            return false;
        }

        public bool FoundEnrollmentByPK(s18827Context dbContext, int id)
        {
            var res = dbContext.Enrollment.Find(id);
            if (res != null)
            {
                return true;
            }
            return false;
        }
        #endregion


        #region EnrollmentsController
        // public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        // {
        //     throw new System.NotImplementedException();
        // }

        // public PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request)
        // {
        //     throw new System.NotImplementedException();
        // }
        #endregion

    }
}
