using System;
using Tut4Proj.Models;
using Tut4Proj.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tut4Proj.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet] // pasing param by QuerryString
        public IActionResult GetStudents(string orderBy = "FirstName") // handles get request
        {
            // return $"Aleks, Maria, Dominik sortBy={orderBy}"; // for access to ordering by
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")] // pasing param by URL segment

        public IActionResult GetStudent(int id)
        {
            if (_dbService.IdExists(id) == true) return Ok(_dbService.GetStudentById(id));
            else return NotFound("Student was not found");
        }

        [HttpPost] // pasing param by the body
        public IActionResult CreateSAndAddtudent(Student student) //import library
        {
            var newIndexNum = $"s{new Random().Next(1, 20000)}";
            student.IndexNumber = newIndexNum;
            if (_dbService.IdExists(student.IdStudent) == false) return Ok(_dbService.AddStudent(student)); // adds student created with POST (with random sNumber) to list of students
            else return UnprocessableEntity("Student with this id already exits");
            //return Ok(student); // in case we want to see new added student
        }

        [HttpPut("{id}")] // To update: /api/students/10/?fName=Teodor&lName=Mako&indNum=666
        public IActionResult EditStudent(int id, string fName = null, string lName = null, string indNum=null)
        { // strings have default values so that not all props have to be updated
            if (_dbService.IdExists(id) == true) return Ok(_dbService.EditStudentById(id, fName, lName, indNum));
            else return NotFound("Student was not found");
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveStudent(int id)
        {
            if (_dbService.IdExists(id) == true) return Ok(_dbService.RemoveStudentById(id));
            else return NotFound("Student was not found");
        }
    }
}
