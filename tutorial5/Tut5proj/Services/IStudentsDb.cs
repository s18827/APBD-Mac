using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tut5proj.Models;

namespace Tut5proj.Services
{
    public interface IStudentsDb // used for StudentsController
    {
         // Always good to use abstraction
        IEnumerable<Student> GetStudents();

        Enrollment GetEnrollmentByIndNo(string indexNumber);

        bool StudentExists(string idexNumber);
        
    }
}