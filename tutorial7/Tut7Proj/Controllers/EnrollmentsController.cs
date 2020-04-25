using System.Diagnostics;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Tut7Proj.DTOs.Requests;
using Tut7Proj.DTOs.Responses;
using Tut7Proj.Models;
using Microsoft.AspNetCore.Mvc;
using Tut7Proj.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Tut7Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class EnrollmentsController : ControllerBase
    {
        private IDbService _service;

         public IConfiguration Configuration { get; set; }
        public EnrollmentsController(IDbService service, IConfiguration configuration) // dependency injection
        {
            _service = service;
            Configuration = configuration;
        }
        
        [HttpPost] //, Name = "EnrollStudent"
        [ActionName("EnrollStudent")]
        [Authorize(Roles = "employee")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                var response = _service.EnrollStudent(request);
                return CreatedAtAction("EnrollStudent", response);
            }
            catch (ArgumentException)
            {
                return NotFound("Studies with given name not found");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Student with given index number already exists");
            }
        }

        [Route("promotions")]
        [HttpPost]
        [ActionName("PromoteStudents")]
        [Authorize(Roles = "employee")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            try
            {
                var response = _service.PromoteStudents(request);
                return CreatedAtAction("PromoteStudents", response);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Invalid request");
            }
            catch (SqlException)
            {
                return NotFound("Studies with given name not found");
            }
            catch (ArgumentException)
            {
                return BadRequest("No values for response found");
            }
        }
    }
}