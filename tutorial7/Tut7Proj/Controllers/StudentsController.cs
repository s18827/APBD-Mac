using System;
using Microsoft.AspNetCore.Mvc;
using Tut7Proj.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Tut7Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class StudentsController : ControllerBase
    {

        private IDbService _service;

        public IConfiguration Configuration { get; set; }
        
        public StudentsController(IDbService service, IConfiguration configuration) // dependency injection
        {
            _service = service;
            Configuration = configuration;
        }

        [HttpGet]
        [Authorize(Roles = "employee")]
        public IActionResult GetStudents(int? idStudy)
        {
            try
            {
                var listOfStudents = _service.GetStudents(idStudy);
                return Ok(listOfStudents);
            }
            // catch (InvalidOperationException)
            // {
            //     return BadRequest();
            // }
            catch (ArgumentNullException)
            {
                return NotFound("Studies with given id not found");
            }
            catch (ArgumentException)
            {
                return NotFound("No students for this studies found");
            }
        }
    }
}