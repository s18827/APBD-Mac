using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tut4Proj.Models;

namespace Tut4Proj.Services
{
    public interface IStudentsDb
    {
         // Always good to use abstraction
        IEnumerable<Student> GetStudents();

        Enrollment GetEnrollmentByIndNo(string indexNumber);

        bool StudentExists(string idexNumber);
        
        // public IActionResult AddStudent(Student student);

        // public IActionResult EditStudentById(int id, string newFname, string newLname, string newIndexNum);

        // public IActionResult RemoveStudentById(int id);

        // public bool IdExists(int id);
    }
}