using System;
using System.Data.SqlClient;
using System.Text;
using ExTest1.DTOs.Requests;
using ExTest1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExTest1.Controllers
{
    [Route("api/animals/")]
    [ApiController] // implicit model validation - validates our Required and all other adnotation
    public class AnimalsController : ControllerBase
    {
        private IDbService _service;
        public AnimalsController(IDbService service)
        {
            _service = service;
        }


        [HttpGet("list")]
        public IActionResult GetAnimals(string sortBy)
        {
            try
            {
                var response = _service.GetAnimals(sortBy);
                return Ok(response);
            }
            catch (SqlException)
            {
                return BadRequest("Incorrect sort request (sortBy value doesn't match proper column name)");
            }
            catch (ArgumentException)
            {
                return NotFound("No animals found");
            }
        }

        // [Route("add")]
        [HttpPost("add")]
        [ActionName("AddAnimal")]
        public IActionResult AddAnimal(AddAnimalRequest request)
        {
            try
            {
                var response = _service.AddAnimal(request);
                return CreatedAtAction("AddAnimal", response);
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                };
                return BadRequest("Error in request: " + errorMessages.ToString());
            }
            catch (ArgumentException)
            {
                return BadRequest("Error when creating response");
            }
            // catch(ArgumentNullException)
            // {
            //     return NotFound("");
            // }
        }

    }
}