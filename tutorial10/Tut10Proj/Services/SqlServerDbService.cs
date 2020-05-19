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
            if (studFound) throw new ArgumentNullException("Student with given index number is already in the db");

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

        public EditStudentResponse EditStudent(s18827Context context, string indexNumber, EditStudentRequest request)
        {
            EditStudentResponse response = null;
            // if(request.FirstName != null) FirstName = request.FirstName;

            // EditStudentResponse response = null;
            // var student = new Student {
            //     IndexNumber = indexNumber,
            //     FirstName = request.FirstName,
            //     LastName = request.LastName,
            //     // BirthDate = request.BirthDate,
            //     IdEnrollment = request.IdEnrollment
                
            //     };
            response.EditMessage = $"Student with index number = {indexNumber} has been successfully edited.";
            response.EditedFields.ToString();
            return response;
        } 

        public async Task RemoveStudent(s18827Context dbContext, string indexNumber)
        {
            var studFound = FoundStudentByPK(dbContext, indexNumber);
            if (!studFound) throw new ArgumentNullException("Student with given index number not found");
            // var studToRm = dbContext.Student.Find(indexNumber);
            var studToRm = new Student { IndexNumber = indexNumber };
            dbContext.Attach(studToRm); // this way I don't have to find/download from db agian the object I want to delete
            // dbContext.Remove(studToRm); // below insted of this
            dbContext.Entry(studToRm).State = EntityState.Deleted; // now we can se ChangeDetector and see all changes when Debugging
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
