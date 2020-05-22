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
        Task<IEnumerable<Student>> ListStudents();

        Task AddStudent(AddStudentRequest request);

        Task EditStudent(string indexNumber, EditStudentRequest request);

        Task RemoveStudent(string indexNumber);

        // ----------------------------------------------------

        Task<Student> ExistsStudentByPK(string indexNumber);
        Task<Enrollment> ExistsEnrollmentByPK(int id);
        Task<int> GetIdStudiesByName(string studiesName);
        Task<Enrollment> GetEnrollmentByIdStudiesAndSemesterNum(int studiesId, int semester);
        Task<int> GetMaxIdEnrollment();
        Task CreateNewEnrollment(int newIdEnroll, int semester, int idStudies, DateTime startDate);
        Task CreateNewStudent(string indexNumber, string firstName, string lastName, DateTime birthDate, int idEnrollment);
        Task<Enrollment> GetEnrollmentByPK(int id);
        Task<bool> ExistStudentsByIdEnroll(int idEnrollment);
        Task UpdateStudentsWithNewEnrollment(Enrollment oldEnrollment, int oldSemester, int studiesId, int newIdEnroll);

        // ----------------------------------------------------

        Task<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest request);

        Task<PromoteStudentsResponse> PromoteStudents(PromoteStudentsRequest request);
    }
}