using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tut4Proj.Models;

namespace Tut4Proj.Services
{
    public class SqlServerDb : IStudentsDb
    {
        // we could have some repository or sth patterns to reuse our code
        // Repository pattern
        // or Unit of work + ORM
        // we'll focus on that later

        public IActionResult GetStudents()
        {
            throw new System.NotImplementedException();
        }

        public IActionResult GetEnrollmentByIndNo(int indexNumber)
        {
            throw new System.NotImplementedException();
        }

    }
}