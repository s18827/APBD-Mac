using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tut11Proj.Entities;
using Tut11Proj.Services;
using Tut11Proj.DTOs.Requests;

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

        [HttpGet("idDoctor")]
        public async Task<IActionResult> GetDoctor(int idDoctor)
        {
            try
            {
                var doctor = await _service.GetDoctor(idDoctor);
                return Ok(doctor);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ActionName("AddDoctor")]
        public async Task<IActionResult> AddDoctor(Doctor doctor)
        {
            try
            {
                var response = await _service.AddDoctor(doctor); // make it return entity of added student to be printed in Ok(...);
                return CreatedAtAction("AddDoctor", response);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return BadRequest("Doctor cannot be added: Doctor with given id already exists in the db");
                    if (e is ArgumentException) return BadRequest("Doctor cannot be added: This email address is already used by some Doctor");
                    else return BadRequest("add: OTHER ERROR\n" + e.StackTrace);
                }
                return null;
            }
        }

        [HttpPut("{idDoctor}")]
        public async Task<IActionResult> ModifyDoctor(Doctor doctor)
        {
            try
            {
                await _service.ModifyDoctor(doctor);
                return NoContent();
            }
             catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("Doctor cannot be modified: Doctor with given id not found");
                    if (e is ArgumentException) return BadRequest("Doctor cannot be modified: Only one Doctor can have one email address");
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }
        

        [HttpDelete("{idDoctor}")]
        public async Task<IActionResult> DeleteDoctor(int idDoctor)
        {
            try
            {
                await _service.DeleteDoctor(idDoctor);
                return NoContent();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("Doctor cannot be removed: Doctore with given id not found");
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }
    }
}
