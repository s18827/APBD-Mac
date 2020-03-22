using System.Collections.Generic;
using Tut3Proj.Models;

namespace Tut3Proj.Services
{
    public interface IDbService
    {
         // Always good to use abstraction
        public IEnumerable<Student> GetStudents();

        public string GetStudentById(int id);
        
        public string AddStudent(Student student);

        public string EditStudentById(int id, string newFname, string newLname, string newIndexNum);

        public string RemoveStudentById(int id);

        public bool IdExists(int id);
    }
}