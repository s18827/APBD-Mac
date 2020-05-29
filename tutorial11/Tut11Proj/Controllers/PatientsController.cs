using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tut11Proj.Models;

namespace Tut11Proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {

        private readonly s18827DbContext _context;
        public PatientsController(s18827DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddPatient()
        {
            Patient p = new Patient{
                IdPatient = 1,
                FirstName = "fname",
                LastName = "lname",
                Birthdate = new DateTime(1999,12,30)
            };
            
            return Ok(_context.Patients.Add(p));
        }

        [HttpGet]
        public IActionResult GetPatients()
        {
            return Ok(_context.Patients.ToList());
        }
    }
}
