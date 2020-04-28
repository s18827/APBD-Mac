using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Tut7Proj.Services;
using Tut7Proj.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Tut7Proj.DTOs.Responses;
using System.Data.SqlClient;

namespace Tut7Proj.Controllers
{
    [Route("api/login")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class LoginsController : ControllerBase
    {
        private IDbService _service;

        public IConfiguration Configuration { get; set; }

        public LoginsController(IDbService service, IConfiguration configuration)
        {
            _service = service;
            Configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(Log_inRequest request)
        {
            try
            {
                Log_inResponse response = _service.Login(request, Configuration);
                return Ok(response);
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
                // if(errorMessages.ToString().Contains(errorCode=1));
                return BadRequest("Error in SQL stored procedure: " + errorMessages.ToString());
            }
            catch (ArgumentNullException)
            {
                return BadRequest("No passwordSalt found for this user");
            }
            catch (ArgumentException)
            {
                return BadRequest("User not updated with RefreshToken");
            }
        }

        [HttpPost("refresh-token/{token}")] // DOESN'T WORK YET - TODO
        public IActionResult RefreshToken(string requestToken)
        {
            try
            {
                Log_inResponse response = _service.RefreshToken(requestToken, Configuration);
                return Ok(response);
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
                // if(errorMessages.ToString().Contains(errorCode=1)) return NotFound();
                return BadRequest("Error in sql: " + errorMessages.ToString());
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Cannot refresh access token for given refresh token");
            }
            catch (ArgumentException)
            {
                return BadRequest("SqlException: New refresh token cannot be added to database");
            }
        }
    }
}