using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tut7Proj.Models;
using Tut7Proj.Services;

namespace Tut7Proj.Controllers
{
    public class DataSavingsController : ControllerBase
    {
        private IDbService _service;

        public IConfiguration Configuration { get; set; }

        public DataSavingsController(IDbService service/*, IConfiguration configuration*/) // dependency injection
        {
            _service = service;
            // Configuration = configuration;
        }

        public IActionResult SaveLoginDataToFile(LoginModel loginModel)
        {
            try
            {
                _service.SaveLoginDataToFile(loginModel);
                return Ok("Login data has been successfully saved to a file");
            }
            catch (Exception)
            {
                return BadRequest("Error when writting data to the file");
            }
        }

        public IActionResult SaveLoginDataToDb(LoginModel loginModel)
        {
            try
            {
                _service.SaveLoginDataToFile(loginModel);
                return Ok("Login data has been successfully saved to database");
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
                return BadRequest("Error in sql: " + errorMessages.ToString());
                // return Ok("User logged in");
            }
        }

        public IActionResult SaveRequestDataToFile(LoginModel loginModel)
        {
            try
            {
                _service.SaveLoginDataToFile(loginModel);
                return Ok("Request data has been successfully saved to a file");
            }
            catch (Exception)
            {
                return BadRequest("Error when writting data to the file");
            }
        }
    }
}