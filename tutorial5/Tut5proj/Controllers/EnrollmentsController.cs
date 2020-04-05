using System.Diagnostics;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Tut5proj.DTOs.Requests;
using Tut5proj.DTOs.Responses;
using Tut5proj.Models;
using Tut5proj.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tut5proj.Controllers
{
    [Route("api/enrollments/")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class EnrollmentsController : ControllerBase
    {

        private IStudentsServiceDb _service;

        public EnrollmentsController(IStudentsServiceDb service) // dependency injection
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
                var response = _service.PromoteStudets(request);
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