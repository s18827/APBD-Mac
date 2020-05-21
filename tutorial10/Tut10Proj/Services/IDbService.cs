using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tut10Proj.Models;
using Tut10Proj.Models.DTOs.Requests;
using Tut10Proj.Models.DTOs.Responses;

namespace Tut10Proj.Services
{
    public interface IDbService
    {
        IEnumerable<Student> ListStudents(s18827Context dbContext);

        Task AddStudent(s18827Context dbContext, AddStudentRequest request);

        Task EditStudent(s18827Context dbContext, string indexNumber, EditStudentRequest request);

        Task RemoveStudent(s18827Context dbContext, string indexNumber);

        // ----------------------------------------------------

        Task<bool> ExistsStudentByPK(s18827Context dbContext, string indexNumber);
        Task<bool> ExistsEnrollmentByPK(s18827Context dbContext, int id);
        int GetIdStudiesByName(s18827Context dbContext, string studiesName);
        Enrollment GetEnrollmentByIdStudiesAndSemesterNum(s18827Context dbContext, int studiesId, int semester);
        int GetMaxIdEnrollment(s18827Context dbContext);
        void CreateNewEnrollment(s18827Context dbContext, int newIdEnroll, int semester, int idStudies, DateTime startDate);
        void CreateNewStudent(s18827Context dbContext, string indexNumber, string firstName, string lastName, DateTime birthDate, int idEnrollment);
        Enrollment GetEnrollmentByPK(s18827Context dbContext, int id);
        bool ExistStudentsByIdEnroll(s18827Context dbContext, int idEnrollment);
        void UpdateStudentsWithNewEnrollment(s18827Context dbContext, int oldSemester, int studiesId, int newIdEnroll);

        // ----------------------------------------------------

        EnrollStudentResponse EnrollStudent(s18827Context dbContext, EnrollStudentRequest request);

        PromoteStudentsResponse PromoteStudents(s18827Context dbContext, PromoteStudentsRequest request);
    }
}