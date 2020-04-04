using Tut5proj.DTOs.Requests;
using Tut5proj.DTOs.Responses;

namespace Tut5proj.Services
{
    public interface IStudentsServiceDb
    {
        // better to pass separeate bussiness model instead of request
         EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

         PromoteStudentsResponse PromoteStudets(PromoteStudentsRequest request);
    }
}