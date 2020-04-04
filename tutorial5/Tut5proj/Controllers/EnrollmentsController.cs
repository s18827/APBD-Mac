using System.Data.SqlClient;
using System.Data.SqlTypes;
using Tut5proj.DTOs.Requests;
using Tut5proj.DTOs.Responses;
using Tut5proj.Models;
using Tut5proj.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tut5proj.Controllers
{
    // enroll new student to 
    // check if this studies exist
    // it there is enrollment record that point to specific studies that student wants to enroll and semester = 1 if not exits andd to table and setup date to currentdate
    // insert new student number

    // ^ instansaction all if sth did't happen rollback transaction
    [Route("api/enrollments/")]
    [ApiController] // implicit model validation - validates our Required and all other adnotations
    public class EnrollmentsController : ControllerBase
    {

        private IStudentsServiceDb _service;

        public EnrollmentsController(IStudentsServiceDb service) // dependency injection
        {
            _service = service;
        }
        [HttpPost] //, Name = "EnrollStudent"
        [ActionName("EnrollStudent")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            // if (!ModelState.IsValid) // from ControllerBase, checks all components from Request 
            // { // [ApiController] manages that
            //     return BadRequest("!!!");
            // }
            // if(request==null || request.FirstName == null) // we do validation by specifying fields in DTO's
            // {
            //     return BadRequest("Request is not correct");
            // }
            // Manual mapping bussiness model: (normally use library for that)
            // var st = new Student()
            // st.FirstName = request.FirstName;
            // ...

            // DTO - Data Transfer Object
            /* { // this is what we want to return
                "Semester": 1,
                "LastName": "Kowalski"
            }*/
            var response = _service.EnrollStudent(request);
            if (response == null) // find better way
            {
                return BadRequest("Requested studies not found or student with this IndexNumber already exists");
            } // split error msg?
            return CreatedAtAction("EnrollStudent", response);
        }

        // commented for now so that I can test above Post

        [Route("promotions")]
        [HttpPost]
        [ActionName("PromoteStudents")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            // Requested - name of sudies=..., semeseter=...

            // chek if studies exist
            // find all studies from studies=.. and semester=...
            // 3. promote all sttudemts to 
            // ^ find enrollment record with sudies=... and semester=...+1 -> IdEnrollment = 10
            // Update all the students
            // If Enrollment does not exist ->add new one

            // ^^^ do all that in stored procedure

            // - not always procedues are the best bc our bussines logic gets split into vscode program and sql program
            // - no good way of 
            // - hard to switch btw different database languages
            // - we cannot scale database easly using our approach (like when adding second server to handle our growing db - loadbalancer gets mixed up when starting procedures)
            // - hard to use with ORM libraries - we'll use them alter
            // + faster to execute code when its directly on db - should be more prone to InjectionAtacks
            // + using stored procedures we can easly provide accessability to other users for some things only
            // ^applying security rules on users
            var response = _service.PromoteStudets(request);
            if (response == null) // find better way
            {
                return BadRequest("Enrollment not returned by stored procedure");
            }
            return CreatedAtAction("PromoteStudents", response);
        }

        // move data form endpoint to IStudentsServiceDb -- persistance rule! Solid - S!
    }
}