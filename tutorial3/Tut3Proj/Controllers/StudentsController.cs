using System;
using Tut3Proj.Models;
using Tut3Proj.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApbdLecture3WebApi.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase // handles request
    {
        private IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet]
        // 2. Passing the data by QueryString = limited option (friendly URLs)
        public IActionResult GetStudents(string orderBy = "FirstName") // handles get request
        {
            // return $"Aleks, Maria, Dominik sortBy={orderBy}"; // for access to ordering by
            return Ok(_dbService.GetStudents());
        }
        // data within QueryString is encoded
        // filtering/sorting additional ifo about how to get some data
        // data send in the body using json or xml - there are ways to secure this info

        //1. How to pass data using URL segment?
        [HttpGet("{id}")] // calls id from getStudent
                          // above makes below accepts http get request url has to be unique
                          //[-] url segments are visible to user (of course)

        public IActionResult GetStudent(int id)
        {
            if (_dbService.IdExists(id) == true) return Ok(_dbService.GetStudentById(id));
            else return NotFound("Student was not found");
        }

        // 3. Passing the data using Body (usually POST)
        // good for creating the data
        [HttpPost]
        public IActionResult CreateAndAddStudent(Student student) //import library
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
