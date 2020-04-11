using System;
using Tut6Proj.Models;
using Tut6Proj.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Tut6Proj.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IDbService _service;
    
        public StudentsController(IDbService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_service.GetStudents());
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetEnrollment(string indexNumber)
        {
            // if (!_service.GetStudentByIndex(indexNumber)) return NotFound("Student with given index number hasn't been found.");
            // else // we have now custom exception handling
            return Ok(_service.GetEnrollOfStudByIndNo(indexNumber));
        }

    }
}
