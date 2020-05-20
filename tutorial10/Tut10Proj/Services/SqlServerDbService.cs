using System.Data;
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
        #endregion

        #region Helper methods
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

        public int GetIdStudiesByName(s18827Context dbContext, string studiesName)
        {
            int idStudies = 0;
            idStudies = dbContext.Studies.Where(s => s.Name == studiesName).FirstOrDefault().IdStudy;
            return idStudies;
        }

        public Enrollment GetEnrollmentByIdStudiesAndSemEqual1(s18827Context dbContext, int idStudies)
        {
            Enrollment res = null;
            res = dbContext.Enrollment.Where(e => e.Semester == 1 && e.IdStudy == idStudies).FirstOrDefault();
            return res;
        }

        public int GetMaxIdEnrollmentForIdStudies(s18827Context dbContext, int idStudies)
        {
            int maxIdEnroll = 0;
            maxIdEnroll = dbContext.Enrollment.Max(e => e.IdEnrollment);
            return maxIdEnroll;
        }

        public void CreateNewEnrollment(s18827Context dbContext, int newIdEnroll, int semester, int idStudies, DateTime startDate)
        {
            var enrollFound = FoundEnrollmentByPK(dbContext, newIdEnroll); // shouldn't have any possibility of happening - if happens sth wrong with FindMaxIdEnroll.. probably
            if (enrollFound) throw new ArgumentException("Enrollment with given id already exists in the db");

            var newEnroll = new Enrollment
            {
                IdEnrollment = newIdEnroll,
                Semester = semester,
                IdStudy = idStudies,
                StartDate = DateTime.Now
            };
            dbContext.Attach(newEnroll);
            dbContext.Entry(newEnroll).State = EntityState.Added;
        }

        public void CreateNewStudent(s18827Context dbContext, string indexNumber, string firstName, string lastName, DateTime birthDate, int idEnrollment)
        {
            var enrollFound = FoundStudentByPK(dbContext, indexNumber); // shouldn't have any possibility of happening - if student exists error should be thrown in EnrollStudent
            if (enrollFound) throw new ArgumentException("Student with given id already exists in the db");

            var newStud = new Student
            {
                IndexNumber = indexNumber,
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                IdEnrollment = idEnrollment
            };
            dbContext.Attach(newStud);
            dbContext.Entry(newStud).State = EntityState.Added;
        }

        public Enrollment GetEnrollmentByPK(s18827Context dbContext, int id)
        {
            var enrollFound = FoundEnrollmentByPK(dbContext, id); // shouldn't have any possibility of happening
            if (!enrollFound) throw new ArgumentException("Enrollment with given id not found in the db");

            Enrollment enroll = null;
            enroll = dbContext.Enrollment.Where(e => e.IdEnrollment == id).FirstOrDefault();
            return enroll;
        }
        #endregion

        #region EnrollmentsController
        public EnrollStudentResponse EnrollStudent(s18827Context dbContext, EnrollStudentRequest request)
        {
            // TODO
            // 1. Check if studies from request exist -> if not 404
            // 2. Check if enrollment that points to the specific studies that the student wants to enroll exists and semester = 1 -> INSERT, setup StartDate = CurrDate
            // 3. Create new student (if index of new student (from request) exists) -> 400, else INSERT new Student
            // 4. return Enrollment mode;
            EnrollStudentResponse response = null;

            // AD1.
            var studiesId = GetIdStudiesByName(dbContext, request.Studies);
            if (studiesId == 0) throw new ArgumentException("Studies with given name not found");

            // AD2.
            var findEnroll = GetEnrollmentByIdStudiesAndSemEqual1(dbContext, studiesId);
            var enrollId = findEnroll.IdEnrollment; // might not work ...?
            if (findEnroll == null)
            { // create new Enrollemnt for 1 sem of given studies
                var newIdEnroll = GetMaxIdEnrollmentForIdStudies(dbContext, studiesId) + 1;
                CreateNewEnrollment(dbContext, newIdEnroll, 1, studiesId, DateTime.Now);
                enrollId = newIdEnroll; // assign new enrollmentId to be used later
            }

            // AD3.
            var studentExists = FoundStudentByPK(dbContext, request.IndexNumber);
            if (studentExists) throw new InvalidOperationException("Student with given index number already exists in the db");
            CreateNewStudent(dbContext, request.IndexNumber, request.FirstName, request.LastName, request.BirthDate, enrollId);

            // AD4.
            Enrollment thisEnroll = GetEnrollmentByPK(dbContext, enrollId);

            response = new EnrollStudentResponse{
                IdEnrollment = thisEnroll.IdEnrollment,
                Semester = thisEnroll.Semester,
                IdStudy =thisEnroll.IdStudy,
                StartDate = thisEnroll.StartDate
            };
            dbContext.SaveChanges();
            return response;
        }

        // TODO
        public PromoteStudentsResponse PromoteStudents(s18827Context dbContext, PromoteStudentsRequest request)
        {
            PromoteStudentsResponse response = null;

            // ...

            dbContext.SaveChanges();
            return response;
        }
        #endregion

    }
}
