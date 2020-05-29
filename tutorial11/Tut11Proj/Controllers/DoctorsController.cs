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
    public class DoctorsController : ControllerBase
    {

        private IDbService _service;
        private readonly s18827DbContext _context;
        public DoctorsController(IDbService service, s18827DbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListDoctors()
        {
            try
            {
                var doctorsList = await _service.ListDoctors();
                return Ok(doctorsList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
