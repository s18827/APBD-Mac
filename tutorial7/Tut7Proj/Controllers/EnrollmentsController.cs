using System.Diagnostics;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Tut7Proj.DTOs.Requests;
using Tut7Proj.DTOs.Responses;
using Tut7Proj.Models;
using Microsoft.AspNetCore.Mvc;
using Tut7Proj.Services;

namespace Tut7Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class EnrollmentsController : ControllerBase
    {

        private IDbService _service;

        public EnrollmentsController(IDbService service) // dependency injection
        {
            _service = service;
        }
        [HttpPost] //, Name = "EnrollStudent"
        [ActionName("EnrollStudent")]
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