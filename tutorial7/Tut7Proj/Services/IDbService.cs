using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Tut7Proj.DTOs.Requests;
using Tut7Proj.DTOs.Responses;
using Tut7Proj.Models;

namespace Tut7Proj.Services
{
    public interface IDbService
    {
        void SavRequestDataToFile(IEnumerable<string> data);
        void SaveLoginDataToFile(LoginModel loginModel); // for checking purposes
        void SaveLoginDataToDb(LoginModel loginModel);


        // ----------------------------------------------------
        Log_inResponse Login(Log_inRequest request, IConfiguration Configuration);
        // Log_inResponse Login(Log_inRequest request);

        Log_inResponse RefreshToken(string requestToken, IConfiguration Configuration);

        // ----------------------------------------------------
        IEnumerable<Student> GetStudents(int? idStudy);
        
        // ----------------------------------------------------
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);

    }
}