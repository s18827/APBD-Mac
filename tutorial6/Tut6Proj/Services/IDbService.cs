using System.Collections.Generic;
using Tut6Proj.DTOs.Requests;
using Tut6Proj.DTOs.Responses;
using Tut6Proj.Models;

namespace Tut6Proj.Services
{
    public interface IDbService
    {
        IEnumerable<Student> GetStudents();

        Enrollment GetEnrollOfStudByIndNo(string indexNumber);

        Student GetStudentByIndex(string idexNumber);

        // -----------------------------------

        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

        PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);
    }
}