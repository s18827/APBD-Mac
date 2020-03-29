using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tut4Proj.Models;

namespace Tut4Proj.Services
{
    public interface IStudentsDb
    {
         // Always good to use abstraction
        public IActionResult GetStudents();

        public IActionResult GetEnrollmentByIndNo(int indexNumber);
        
        // public IActionResult AddStudent(Student student);

        // public IActionResult EditStudentById(int id, string newFname, string newLname, string newIndexNum);

        // public IActionResult RemoveStudentById(int id);

        // public bool IdExists(int id);
    }
}