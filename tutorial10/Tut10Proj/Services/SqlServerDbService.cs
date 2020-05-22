using System.Data;
using System.Collections.Generic;
using Tut10Proj.Models;
using System.Linq;
using Tut10Proj.Models.DTOs.Requests;
using System;
using Tut10Proj.Models.DTOs.Responses;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Tut10Proj.Services
{
    public class SqlServerDbService : IDbService
    {

        private s18827Context dbContext;

        public SqlServerDbService(s18827Context context)
        {
            dbContext = context;
        }

        #region StudentsController
        public async Task<IEnumerable<Student>> ListStudents()
        {
            var studentsList = await dbContext.Student.ToListAsync();
            return studentsList;
        }

        public async Task<AddStudentResponse> AddStudent(AddStudentRequest request)
        {
            AddStudentResponse response = null;
            var stud = await GetStudentByPK(request.IndexNumber);
            if (stud != null) throw new ArgumentNullException("Student with given index number already exists");

            var enroll = await GetEnrollmentByPK(request.IdEnrollment);
            if (enroll == null) throw new ArgumentException("Enrollment with given id not found");

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

            response = new AddStudentResponse {
                IndexNumber = student.IndexNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                IdEnrollment = student.IdEnrollment
            };
            return response;
        }

        // async ERROR
        public async Task EditStudent(string indexNumber, EditStudentRequest request)
        {
            var stud = await GetStudentByPK(indexNumber);
            if (stud == null) throw new ArgumentNullException("Student with given index number not found");

            var enroll = await GetEnrollmentByPK((int)request.IdEnrollment);
            if (enroll == null) throw new ArgumentException("Enrollment with given id not found");

            var student = stud;
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

        // async ERROR
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
        #endregion

        #region Helper methods
        public Task<Student> GetStudentByPK(string indexNumber)
        {
            return dbContext.Student.FirstOrDefaultAsync(e => e.IndexNumber == indexNumber);
        }

        public Task<Enrollment> GetEnrollmentByPK(int id)
        {
            return dbContext.Enrollment.FirstOrDefaultAsync(e => e.IdEnrollment == id);
        }

        public async Task<int> GetIdStudiesByName(string studiesName)
        {
            int idStudies = 0;
            var stud = await dbContext.Studies.Where(s => s.Name == studiesName).FirstOrDefaultAsync();
            if (stud == null) return idStudies;
            return stud.IdStudy;
        }

        public Task<Enrollment> GetEnrollmentByIdStudiesAndSemesterNum(int idStudies, int semester)
        {
            return dbContext.Enrollment.Where(e => e.Semester == semester && e.IdStudy == idStudies).FirstOrDefaultAsync();
        }

        public Task<int> GetMaxIdEnrollment()
        {
            return dbContext.Enrollment.MaxAsync(e => e.IdEnrollment);
        }

        public async Task CreateNewEnrollment(int newIdEnroll, int semester, int idStudies, DateTime startDate)
        {
            var enroll = await GetEnrollmentByPK(newIdEnroll);
            if (enroll != null) throw new ArgumentException("Enrollment with given id already exists");

            var newEnroll = new Enrollment
            {
                IdEnrollment = newIdEnroll,
                Semester = semester,
                IdStudy = idStudies,
                StartDate = DateTime.Now,
            };

            dbContext.Enrollment.Attach(newEnroll);
            dbContext.Entry(newEnroll).State = EntityState.Added;
        }

        public async Task CreateNewStudent(string indexNumber, string firstName, string lastName, DateTime birthDate, int idEnrollment)
        {
            var student = await GetStudentByPK(indexNumber);
            if (student != null) throw new ArgumentException("Student with given index number already exists");
            // var enroll = await ExistsEnrollmentByPK(idEnrollment); // shouldn't have any possibility of happening - if student exists error should be thrown in EnrollStudent
            // if (enroll != null) throw new ArgumentNullException("Enrollment with this id not found");

            var newStud = new Student
            {
                IndexNumber = indexNumber,
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                IdEnrollment = idEnrollment
            };
            dbContext.Student.Attach(newStud);
            dbContext.Entry(newStud).State = EntityState.Added;
        }

        public Task<bool> ExistStudentsByIdEnroll(int idEnrollment)
        {
            return dbContext.Student.AnyAsync(s => s.IdEnrollment == idEnrollment);
        }

        public async Task UpdateStudentsWithNewEnrollment(Enrollment oldEnrollment, int oldSemester, int studiesId, int newIdEnroll)
        {
            // var oldIdEnroll = await dbContext.Enrollment.Where(e => e.Semester == oldSemester && e.IdStudy == studiesId).FirstOrDefaultAsync();
            // if (oldIdEnroll == null) throw new ArgumentException("USWNE: There are no students on this semester for given studies");

            // var enroll = await ExistsEnrollmentByPK(newIdEnroll); // shouldn't have any possibility of happening
            // if (enroll == null) throw new ArgumentException("Enrollment with given id not found");

            // bool existStudentsForIdEnroll = true; // not needded since above
            // existStudentsForIdEnroll = await ExistStudentsByIdEnroll(oldIdEnroll.IdEnrollment);
            // if (!existStudentsForIdEnroll) throw new ArgumentException("There are no students on this semester for given studies");

            var studentList = dbContext.Student.Where(s => s.IdEnrollment == oldEnrollment.IdEnrollment);

            await studentList.ForEachAsync(s => s.IdEnrollment = newIdEnroll);

            foreach (var student in studentList)
            {
                //dbContext.Student.Attach(student); // once again not neede since we operate on already focused on instances of Student
                dbContext.Entry(student).Property("IdEnrollment").IsModified = true;
            }
            await dbContext.SaveChangesAsync();
        }
        #endregion

        #region EnrollmentsController
        public async Task<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest request)
        {
            // TODO
            // 1. Check if Studies from request exist -> if not 404
            // 2. Check if Enrollment that points to the specific studies that the student wants to enroll exists and semester = 1 -> INSERT, setup StartDate = CurrDate
            // 3. Create new Student (if index of new student (from request) exists) -> 400, else INSERT new Student
            // 4. Create response

            EnrollStudentResponse response = null;

            // AD1.
            var studiesId = await GetIdStudiesByName(request.Studies);
            if (studiesId == 0) throw new ArgumentNullException("Studies with given name not found");

            // AD2.
            var findEnroll = await GetEnrollmentByIdStudiesAndSemesterNum(studiesId, 1);
            var enrollId = findEnroll.IdEnrollment;
            if (findEnroll == null)
            { // create new Enrollemnt for first sem of given studies
                var newIdEnroll = await GetMaxIdEnrollment() + 1;
                await CreateNewEnrollment(newIdEnroll, 1, studiesId, DateTime.Now);
                enrollId = newIdEnroll; // assign new enrollmentId to be used later
            }

            // AD3.
            await CreateNewStudent(request.IndexNumber, request.FirstName, request.LastName, request.BirthDate, enrollId);

            // AD4.
            var thisEnroll = await GetEnrollmentByPK(enrollId);
            if (thisEnroll == null) throw new ArgumentNullException("Something went wrong: created Enrollment not found");

            response = new EnrollStudentResponse
            {
                IdEnrollment = thisEnroll.IdEnrollment,
                Semester = thisEnroll.Semester,
                IdStudy = thisEnroll.IdStudy,
                StartDate = thisEnroll.StartDate
            };
            await dbContext.SaveChangesAsync();
            return response;
        }

        public async Task<PromoteStudentsResponse> PromoteStudents(PromoteStudentsRequest request)
        {
            // TODO
            // 1. Check Studies
            // 2. Find Enrollment (if doesn't exist -> create new by getting maxIdEnroll)
            // 3. Update all Students with values from request
            // 4. Create response
            PromoteStudentsResponse response = null;

            // AD1.
            var studiesId = await GetIdStudiesByName(request.Studies);
            if (studiesId == 0) throw new ArgumentNullException("Studies with given name not found");
            var oldEnroll = await GetEnrollmentByIdStudiesAndSemesterNum(studiesId, request.Semester);
            if (oldEnroll == null) throw new ArgumentException("There are no Students on this semester for given Studies");

            // AD2.
            Enrollment findEnroll = await GetEnrollmentByIdStudiesAndSemesterNum(studiesId, request.Semester + 1);
            int enrollId = 0;
            if (findEnroll == null)
            { // create new Enrollemnt for semester = sem from request + 1
                var newIdEnroll = await GetMaxIdEnrollment() + 1;
                await CreateNewEnrollment(newIdEnroll, request.Semester + 1, studiesId, DateTime.Now);
                enrollId = newIdEnroll; // assign new enrollmentId to be used later
            }
            else { enrollId = findEnroll.IdEnrollment; }

            // AD3.
            await UpdateStudentsWithNewEnrollment(oldEnroll, request.Semester, studiesId, enrollId);

            // AD4.
            Enrollment newEnroll = await GetEnrollmentByPK(enrollId);

            response = new PromoteStudentsResponse
            { // change values
                IdEnrollment = newEnroll.IdEnrollment,
                Semester = newEnroll.Semester,
                IdStudy = newEnroll.IdStudy,
                StartDate = newEnroll.StartDate
            };
            await dbContext.SaveChangesAsync();
            return response;
        }
        #endregion

    }
}
