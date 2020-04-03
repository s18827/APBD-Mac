using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tut5proj.Models;

namespace Tut5proj.Services
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