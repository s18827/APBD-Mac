using System;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Test1.DTOs.Responses;
using Test1.Services;

namespace Test1.Controllers
{
    [Route("api/tasks/")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private IDbService _service;
        public TasksController(IDbService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetTeamMemberInfo(int idTeamMember)
        {
            try
            {
            GetTeamMemberInfoResponse response = _service.GetTeamMemberInfo(idTeamMember);
            return Ok(response);
            }
            catch(SqlException ex)
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
            catch(ArgumentException)
            {
                return BadRequest("No data to be read");
            }
            // catch(ArgumentNullException)
            // {
            //     return NotFound("No tasks of given team member found")
            // }
        }

        [HttpDelete("{idProject}")] // throws error but works, I didn't have time to correct that :(
        public IActionResult RemoveProject(int idProject)
        {
            try{
                string response = _service.RemoveProject(idProject);
                return Ok(response);
            }
            catch(SqlException ex)
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
                return BadRequest("Error in sql: " + errorMessages.ToString()); 
            }
            catch(ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}