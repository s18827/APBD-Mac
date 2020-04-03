using Tut5proj.DTOs.Requests;

namespace Tut5proj.Services
{
    public interface IStudentsServiceDb
    {
        // better to pass separeate bussiness model instead of request
         void EnrollStudent(EnrollStudentRequest request);

         void PromoteStudets(int semester, string studies);
    }
}