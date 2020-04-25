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

namespace Tut7Proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class LoginController : ControllerBase
    {
        private IDbService _service;

        public IConfiguration Configuration { get; set; }

        public LoginController(IDbService service, IConfiguration configuration) // dependency injection
        {
            _service = service;
            Configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(Log_inRequest request)
        {
            Log_inResponse response = _service.Login(request, Configuration);   
            return Ok(response);
        }

        [HttpPost("refresh-token/{token}")] // DOESN'T WORK YET - TODO
        public IActionResult RefreshToken(string requestToken)
        {
            Log_inResponse response = _service.RefreshToken(requestToken, Configuration);   
            return Ok(response);
        }

        // public Claim SetRoles(IEnumerable roles) // don't know if it works
        // {
        //     foreach (string role in roles)
        //     {
        //         new Claim(ClaimTypes.Role, role);
        //     }
        //     return null;
        // }
    }
}