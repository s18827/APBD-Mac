using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tut11Proj.Entities;
using Tut11Proj.Services;

namespace Tut11Proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {

        private IDbService _service;
        private readonly s18827DbContext _context;
        public PatientsController(IDbService service, s18827DbContext context)
        {
            _service = service;
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
        public async Task<IActionResult> ListPatients()
        {
            try
            {
                // var listOfStudents = await _service.ListStudents();
                // return Ok(listOfStudents);
                var patientsList = await _service.ListPatients();
                return Ok(patientsList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
