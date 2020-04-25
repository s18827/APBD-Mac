using System.Collections.Generic;
using Tut7Proj.DTOs.Requests;
using Tut7Proj.DTOs.Responses;
using Tut7Proj.Models;

namespace Tut7Proj.Services
{
    public interface IDbService
    {
        IEnumerable<Student> GetStudents(int? idStudy);
        void Login(LoginRequest request);

        // ----------------------------------------------------
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);


    }
}