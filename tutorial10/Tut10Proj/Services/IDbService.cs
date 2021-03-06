using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tut10Proj.Entities;
using Tut10Proj.DTOs.Requests;
using Tut10Proj.DTOs.Responses;

namespace Tut10Proj.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Student>> ListStudents();

        Task<AddStudentResponse> AddStudent(AddStudentRequest request);

        Task EditStudent(string indexNumber, EditStudentRequest request);

        Task RemoveStudent(string indexNumber);

        // ----------------------------------------------------

        Task<Student> GetStudentByPK(string indexNumber);
        Task<Enrollment> GetEnrollmentByPK(int id);
        Task<int> GetIdStudiesByName(string studiesName);
        Task<Enrollment> GetEnrollmentByIdStudiesAndSemesterNum(int studiesId, int semester);
        Task<int> GetMaxIdEnrollment();
        Task CreateNewEnrollment(int newIdEnroll, int semester, int idStudies, DateTime startDate);
        Task CreateNewStudent(string indexNumber, string firstName, string lastName, DateTime birthDate, int idEnrollment);
        // Task<Enrollment> GetEnrollmentByPK(int id);
        Task<bool> ExistStudentsByIdEnroll(int idEnrollment);
        Task UpdateStudentsWithNewEnrollment(Enrollment oldEnrollment, int oldSemester, int studiesId, int newIdEnroll);

        // ----------------------------------------------------

        Task<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest request);

        Task<PromoteStudentsResponse> PromoteStudents(PromoteStudentsRequest request);
    }
}