using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test2.DTOs.Requests;
using Test2.Services;

namespace Test2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IDbService _service;

        public PetsController(IDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListPetsRegistered(int? year)
        {
            try
            {
                var list = await _service.ListPetsRegistered(year);
                return Ok(list);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("No registered pets found - list is empty");
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPet(AddPetRequest request)
        {
            try
            {
                var newPet = await _service.AddPet(request);
                return Ok(newPet);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound(e.Message);
                    if (e is ArgumentException) return BadRequest();
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }


    }
}