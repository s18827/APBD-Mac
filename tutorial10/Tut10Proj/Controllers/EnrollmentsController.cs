using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Tut10Proj.Models.DTOs.Requests;
using Tut10Proj.Models.DTOs.Responses;
using Tut10Proj.Models;
using Microsoft.AspNetCore.Mvc;
using Tut10Proj.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Tut10Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class EnrollmentsController : ControllerBase
    {
        private readonly s18827Context _context;

        private IDbService _service;

        // public IConfiguration Configuration { get; set; }
        public EnrollmentsController(IDbService service, s18827Context context) // dependency injection
        {
            _service = service;
            // Configuration = configuration;
            _context = context;
        }

        [HttpPost("enrollStudent")]
        [ActionName("EnrollStudent")]
        // [Authorize(Roles = "employee")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                var response = _service.EnrollStudent(_context, request);
                return CreatedAtAction("EnrollStudent", response);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Studies with given name not found");
            }
            catch (ArgumentException)
            {
                return BadRequest("Student with given index number already exists in the db");
            }
        }

        [Route("promotions")]
        [HttpPost]
        [ActionName("PromoteStudents")]
        // [Authorize(Roles = "employee")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            try
            {
                var response = _service.PromoteStudents(_context, request);
                return CreatedAtAction("PromoteStudents", response);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Studies with given name not found");
            }
            catch (ArgumentException)
            {
                return BadRequest("There are no students on this semester for given studies");
            }
        }
    }
}