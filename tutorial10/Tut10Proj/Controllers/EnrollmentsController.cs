using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Tut10Proj.DTOs.Requests;
using Tut10Proj.DTOs.Responses;
using Tut10Proj.Entities;
using Microsoft.AspNetCore.Mvc;
using Tut10Proj.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Tut10Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class EnrollmentsController : ControllerBase
    {
        private IDbService _service;

        // public IConfiguration Configuration { get; set; }
        public EnrollmentsController(IDbService service) // dependency injection
        {
            _service = service;
            // Configuration = configuration;
   
        }

        [HttpPost("enrollStudent")]
        [ActionName("EnrollStudent")]
        // [Authorize(Roles = "employee")]
        public async Task<IActionResult> EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                var response = await _service.EnrollStudent(request);
                return CreatedAtAction("EnrollStudent", response);
            }
             catch (AggregateException ae) // AggregateException bc of asynchronous code
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("Studies with given name not found");
                    if (e is ArgumentException) BadRequest("Student with given index number already exists");
                    // else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }

        [Route("promotions")]
        [HttpPost]
        [ActionName("PromoteStudents")]
        // [Authorize(Roles = "employee")]
        public async Task<IActionResult> PromoteStudents(PromoteStudentsRequest request)
        {
            try
            {
                var response = await _service.PromoteStudents(request);
                return CreatedAtAction("PromoteStudents", response);
            }
             catch (AggregateException ae) // AggregateException bc of asynchronous code
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("Studies with given name not found");
                    if (e is ArgumentException) return BadRequest("There are no students on this semester for given studies");
                    // else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }
    }
}