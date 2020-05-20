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

        bool FoundStudentByPK(s18827Context dbContext, string indexNumber);
        bool FoundEnrollmentByPK(s18827Context dbContext, int id);

        // ----------------------------------------------------

        // EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        // PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);
    }
}