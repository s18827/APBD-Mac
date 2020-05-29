using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tut10Proj.Entities;
using Tut10Proj.Services;
using System.Linq;
using Tut10Proj.DTOs.Requests;
using System.Threading.Tasks;

namespace Tut10Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private IDbService _service;
        // public IConfiguration Configuration { get; set; }

        public StudentsController(IDbService service) // dependency injection
        {
            _service = service;
            // Configuration = configuration;
        }

        [HttpGet]
        // [Authorize(Roles = "employee")]
        public async Task<IActionResult> ListStudents()
        {
            try
            {
                var listOfStudents = await _service.ListStudents();
                return Ok(listOfStudents);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ActionName("AddStudent")]
        public async Task<IActionResult> AddStudent(AddStudentRequest request)
        {
            try
            {
                var response = await _service.AddStudent(request); // make it return entity of added student to be printed in Ok(...);
                return CreatedAtAction("AddStudent", response);
                // return Ok(response);
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

        [HttpPut("{indexNumber}")]
        public async Task<IActionResult> EditStudent(string indexNumber, EditStudentRequest request)
        {
            try
            {
                await _service.EditStudent(indexNumber, request);
                // return Ok($"Student with index number {indexNumber} has been successfully edited");
                return NoContent();
            }
             catch (AggregateException ae) // AggregateException bc of asynchronous code
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("Student cannot be edited: Student with given index number not found");
                    if (e is ArgumentException) return NotFound("Student cannot be edited: Enrollment with given id not found");
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }
        

        [HttpDelete("{indexNumber}")]
        public async Task<IActionResult> RemoveStudent(string indexNumber)
        {
            try
            {
                await _service.RemoveStudent(indexNumber);
                // return Ok("Student removed successfully");
                return NoContent();
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