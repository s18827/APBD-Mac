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

        private readonly s18827Context dbContext;

        public SqlServerDbService(s18827Context context)
        {
            dbContext = context;
        }

        #region StudentsController
        public IEnumerable<Student> ListStudents()
        {
            var studentsList = dbContext.Student.ToList();
            return studentsList;
        }

        public async Task AddStudent(AddStudentRequest request)
        {
            var studExists = ExistsStudentByPK(request.IndexNumber).Result; // .Result if Task<bool>
            if (studExists) throw new ArgumentNullException("Student with given index number already exists in the db");

            var enrollExists = ExistsEnrollmentByPK(request.IdEnrollment).Result;
            if (!enrollExists) throw new ArgumentException("Enrollment with given id not Exists");

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

        // async ERROR
        public async Task EditStudent(string indexNumber, EditStudentRequest request)
        {
            var studExists = await ExistsStudentByPK(indexNumber);
            if (!studExists) throw new ArgumentNullException("Student with given index number does not exists");

            var enrollExists = await ExistsEnrollmentByPK((int)request.IdEnrollment);
            if (!enrollExists) throw new ArgumentException("Enrollment with given id does not exists");

            var student = new Student();
            student.IndexNumber = indexNumber;
            if (request.FirstName != null) student.FirstName = request.FirstName;
            if (request.LastName != null) student.LastName = request.LastName;
            if (request.BirthDate != null) student.BirthDate = (DateTime)request.BirthDate;
            if (request.IdEnrollment != null) student.IdEnrollment = (int)request.IdEnrollment;

            dbContext.Attach(student); // somehow in conflict with checks
            dbContext.Entry(student).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
        }

        // anync ERROR
        public async Task RemoveStudent(string indexNumber)
        {
            var studExists = ExistsStudentByPK(indexNumber).Result;
            if (!studExists) throw new ArgumentNullException("Student with given index number not Exists");
            // var student = dbContext.Student.Find(indexNumber);
            var student = new Student { IndexNumber = indexNumber };
            dbContext.Attach(student); // somehow in conflict with checks
            // this way I don't have to find/download from db agian the object I want to delete before actually deleting it
            // dbContext.Remove(student); // below line is instead of this line
            dbContext.Entry(student).State = EntityState.Deleted; // now we can use ChangeTracker to track all changes of the state of this object when Debugging
            await dbContext.SaveChangesAsync();
        }
        #endregion

        #region Helper methods
        public async Task<bool> ExistsStudentByPK(string indexNumber)
        {
            var res = dbContext.Student.Find(indexNumber);
            if (res != null)
            {
                // await Task.FromResult(true);
                return true;
            }
            // await Task.FromResult(false);
            return false;
        }

        public async Task<bool> ExistsEnrollmentByPK(int id)
        {
            var res = dbContext.Enrollment.Find(id);
            if (res != null)
            {
                // var res = await true;
                return true;
                // return true;
            }
            // await Task.FromResult(false);
            return false;
        }

        public int GetIdStudiesByName(string studiesName)
        {
            int idStudies = 0;
            var stud = dbContext.Studies.Where(s => s.Name == studiesName).FirstOrDefault();
            if (stud == null) return idStudies;
            return stud.IdStudy;
        }

        public Enrollment GetEnrollmentByIdStudiesAndSemesterNum(int idStudies, int semester)
        {
            Enrollment res = null;
            res = dbContext.Enrollment.Where(e => e.Semester == semester && e.IdStudy == idStudies).FirstOrDefault();
            return res;
        }

        public int GetMaxIdEnrollment()
        {
            int maxIdEnroll = 0;
            maxIdEnroll = dbContext.Enrollment.Max(e => e.IdEnrollment);
            return maxIdEnroll;
        }

        public void CreateNewEnrollment(int newIdEnroll, int semester, int idStudies, DateTime startDate)
        {
            var enrollExists = ExistsEnrollmentByPK(newIdEnroll).Result; // shouldn't have any possibility of happening - if happens sth wrong with FindMaxIdEnroll.. probably
            if (enrollExists) throw new ArgumentException("Enrollment with given id already exists in the db");

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

        public void CreateNewStudent(string indexNumber, string firstName, string lastName, DateTime birthDate, int idEnrollment)
        {
            var enrollExists = ExistsStudentByPK(indexNumber).Result; // shouldn't have any possibility of happening - if student exists error should be thrown in EnrollStudent
            if (enrollExists) throw new ArgumentException("Student with given id already exists in the db");

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

        public Enrollment GetEnrollmentByPK(int id)
        {
            var enrollExists = ExistsEnrollmentByPK(id).Result; // shouldn't have any possibility of happening
            if (!enrollExists) throw new ArgumentException("Enrollment with given id not found in the db");

            Enrollment enroll = null;
            enroll = dbContext.Enrollment.Where(e => e.IdEnrollment == id).FirstOrDefault();
            return enroll;
        }

        public bool ExistStudentsByIdEnroll(int idEnrollment)
        {
            var res = dbContext.Student.Any(s => s.IdEnrollment == idEnrollment);
            if (res)
            {
                return true;
            }
            return false;
        }
        public void UpdateStudentsWithNewEnrollment(int oldSemester, int studiesId, int newIdEnroll)
        {
            var oldIdEnroll = dbContext.Enrollment.Where(e => e.Semester == oldSemester && e.IdStudy == studiesId).FirstOrDefault();
            if (oldIdEnroll == null) throw new ArgumentException("There are no students on this semester for given studies");

            var enrollExists = ExistsEnrollmentByPK(newIdEnroll).Result; // shouldn't have any possibility of happening
            if (!enrollExists) throw new ArgumentException("Enrollment with given id not found in the db");

            bool existStudentsForIdEnroll = true;
            existStudentsForIdEnroll = ExistStudentsByIdEnroll(oldIdEnroll.IdEnrollment);
            if (!existStudentsForIdEnroll) throw new ArgumentException("There are no students on this semester for given studies");

            var studentList = dbContext.Student.Where(s => s.IdEnrollment == oldIdEnroll.IdEnrollment).Select(s => new Student
            {
                IndexNumber = s.IndexNumber,
                IdEnrollment = newIdEnroll
            }).ToArray();

            foreach (var student in studentList)
            {
                dbContext.Attach(student);
                dbContext.Entry(student).Property("IdEnrollment").IsModified = true;
            }
            dbContext.SaveChanges();
        }
        #endregion

        #region EnrollmentsController
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            // TODO
            // 1. Check if Studies from request exist -> if not 404
            // 2. Check if Enrollment that points to the specific studies that the student wants to enroll exists and semester = 1 -> INSERT, setup StartDate = CurrDate
            // 3. Create new Student (if index of new student (from request) exists) -> 400, else INSERT new Student
            // 4. Create response

            EnrollStudentResponse response = null;

            // AD1.
            var studiesId = GetIdStudiesByName(request.Studies);
            if (studiesId == 0) throw new ArgumentNullException("Studies with given name not Exists");

            // AD2.
            var findEnroll = GetEnrollmentByIdStudiesAndSemesterNum(studiesId, 1);
            var enrollId = findEnroll.IdEnrollment;
            if (findEnroll == null)
            { // create new Enrollemnt for first sem of given studies
                var newIdEnroll = GetMaxIdEnrollment() + 1;
                CreateNewEnrollment(newIdEnroll, 1, studiesId, DateTime.Now);
                enrollId = newIdEnroll; // assign new enrollmentId to be used later
            }

            // AD3.
            var studentExists = ExistsStudentByPK(request.IndexNumber).Result;
            if (studentExists) throw new ArgumentException("Student with given index number already exists in the db");
            CreateNewStudent(request.IndexNumber, request.FirstName, request.LastName, request.BirthDate, enrollId);

            // AD4.
            Enrollment thisEnroll = GetEnrollmentByPK(enrollId);

            response = new EnrollStudentResponse
            {
                IdEnrollment = thisEnroll.IdEnrollment,
                Semester = thisEnroll.Semester,
                IdStudy = thisEnroll.IdStudy,
                StartDate = thisEnroll.StartDate
            };
            dbContext.SaveChanges();
            return response;
        }

        public PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request)
        {
            // TODO
            // 1. Check Studies
            // 2. Find Enrollment (if doesn't exist -> create new by getting maxIdEnroll)
            // 3. Update all Students with values from request
            // 4. Create response
            PromoteStudentsResponse response = null;

            // AD1.
            var studiesId = GetIdStudiesByName(request.Studies);
            if (studiesId == 0) throw new ArgumentNullException("Studies with given name not found");

            // AD2.
            Enrollment findEnroll = GetEnrollmentByIdStudiesAndSemesterNum(studiesId, request.Semester + 1);
            int enrollId = 0;
            if (findEnroll == null)
            { // create new Enrollemnt for semester = sem from request + 1
                var newIdEnroll = GetMaxIdEnrollment() + 1;
                CreateNewEnrollment(newIdEnroll, request.Semester + 1, studiesId, DateTime.Now);
                enrollId = newIdEnroll; // assign new enrollmentId to be used later
            }
            else { enrollId = findEnroll.IdEnrollment; }

            // AD3.
            try
            {
                UpdateStudentsWithNewEnrollment(request.Semester, studiesId, enrollId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("There are no students on this semester for given studies");
            }

            // AD4.
            Enrollment newEnroll = GetEnrollmentByPK(enrollId);

            response = new PromoteStudentsResponse
            { // change values
                IdEnrollment = newEnroll.IdEnrollment,
                Semester = newEnroll.Semester,
                IdStudy = newEnroll.IdStudy,
                StartDate = newEnroll.StartDate
            };
            dbContext.SaveChanges();
            return response;
        }
        #endregion

    }
}
