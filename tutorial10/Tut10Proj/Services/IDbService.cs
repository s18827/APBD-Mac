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
        bool FoundStudentByPK(s18827Context dbContext, string indexNumber);
        bool FoundEnrollmentByPK(s18827Context dbContext, int id);
        int GetIdStudiesByName(s18827Context dbContext, string studiesName);
        Enrollment GetEnrollmentByIdStudiesAndSemEqual1(s18827Context dbContext, int studiesId);
        int GetMaxIdEnrollmentForIdStudies(s18827Context dbContext, int studiesId);
        void CreateNewEnrollment(s18827Context dbContext, int newIdEnroll, int semester, int idStudies, DateTime startDate);
        void CreateNewStudent(s18827Context dbContext, string indexNumber, string firstName, string lastName, DateTime birthDate, int idEnrollment);
        Enrollment GetEnrollmentByPK(s18827Context dbContext, int id);
        // ----------------------------------------------------

        EnrollStudentResponse EnrollStudent(s18827Context dbContext, EnrollStudentRequest request);

        PromoteStudentsResponse PromoteStudents(s18827Context dbContext, PromoteStudentsRequest request);
    }
}