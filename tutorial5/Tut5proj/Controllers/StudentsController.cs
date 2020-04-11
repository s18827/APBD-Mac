using System;
using Tut5proj.Models;
using Tut5proj.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Tut5proj.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentsDb _db;
    
        public StudentsController(IStudentsDb dbService)
        {
            this._db = dbService;
        }

        [HttpGet] // pasing param by QuerryString
        public IActionResult GetStudents()
        {
            return Ok(_db.GetStudents());
        }

        [HttpGet("{indexNumber}")] // pasing param by URL segment
        public IActionResult GetEnrollment(string indexNumber)
        {
            if (!_db.StudentExists(indexNumber)) return NotFound("Student with given index number hasn't been found.");
            else return Ok(_db.GetEnrollmentByIndNo(indexNumber));
        }

    }
}
