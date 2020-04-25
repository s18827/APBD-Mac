using System.Net;
using System.Collections;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Mvc;
using Tut7Proj.Services;
using Tut7Proj.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;

namespace Tut7Proj.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class StudentsController : ControllerBase
    {

        private IDbService _service;

        public StudentsController(IDbService service) // dependency injection
        {
            _service = service;
        }

        [HttpGet]
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

    //     [HttpPost]
    //     public IActionResult Login(LoginRequest request)
    //     {
    //         var Claims = new[]
    //         {
    //             new Claim(ClaimTypes.NameIdentifier, request.Id),
    //             new Claim(ClaimTypes.Name, request.Name),
    //             SetRoles(request.Roles)
    //         };
    //         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
    // SymmetricSecurityKey xsd = new SymmetricSecurityKey();
    //         return Ok();
    //     }


        public Claim SetRoles(IEnumerable roles) // don't know if it works
        {
            foreach (string role in roles)
            {
                new Claim(ClaimTypes.Role, role);
            }
            return null;
        }
    }
}