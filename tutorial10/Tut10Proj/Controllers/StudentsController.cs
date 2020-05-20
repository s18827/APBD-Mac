using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tut10Proj.Models;
using Tut10Proj.Services;
using System.Linq;
using Tut10Proj.Models.DTOs.Requests;

namespace Tut10Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly s18827Context _context;

        private IDbService _service;
        // public IConfiguration Configuration { get; set; }

        public StudentsController(IDbService service, s18827Context context) // dependency injection
        {
            _service = service;
            // Configuration = configuration;
            _context = context;
        }

        [HttpGet]
        // [Authorize(Roles = "employee")]
        public IActionResult ListStudents()
        {
            try
            {
                var listOfStudents = _service.ListStudents(_context);
                return Ok(listOfStudents);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentRequest request)
        {
            try
            {
                _service.AddStudent(_context, request).Wait();
                return Ok("Student added successfully");
            }
            catch (AggregateException ae) // AggregateException bc of asynchronous code
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return BadRequest("Student cannot be added: Student with given index number already exists in the db");
                    if (e is ArgumentException) return NotFound("Student cannot be added: Enrollment with given id not found");
                    else return BadRequest("add: OTHER ERROR\n" + e.StackTrace);
                }
                return null;
            }
        }

        [HttpPost("{indexNumber}")]
        public IActionResult EditStudent(string indexNumber, EditStudentRequest request)
        {
            try
            {
                _service.EditStudent(_context, indexNumber, request).Wait();
                return Ok($"Student with index number {indexNumber} has been successfully edited");
            }
             catch (AggregateException ae) // AggregateException bc of asynchronous code
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("Student cannot be edited: Student with given index number not found");
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }
        

        [HttpDelete("{indexNumber}")]
        public IActionResult RemoveStudent(string indexNumber)
        {
            try
            {
                _service.RemoveStudent(_context, indexNumber).Wait();
                return Ok("Student removed successfully");
            }
            catch (AggregateException ae) // AggregateException bc of asynchronous code
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("Student cannot be removed: Student with given index number not found");
                    // else return BadRequest("OTHER ERROR - probably costraints violated (happens when some other table is dependent on Student table)");
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }

    }
}