using System;
using Tut4Proj.Models;
using Tut4Proj.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Tut4Proj.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentsDb _dbService;
        private string ConnString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s18827;User ID=apbds18827;Password=admin";

        public StudentsController(IStudentsDb dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet] // pasing param by QuerryString
        public IActionResult GetStudents()
        {
            var listOfStudents = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConnString)) // managining connection with db
            using (SqlCommand com = new SqlCommand()) // manages sqlQuerries or other commands send to db
            {
                com.Connection = con;
                com.CommandText = "select * from student";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                // returns stream of data (in rows)
                // allows to execute any command for which we wait for response from server
                while (dr.Read()) // for parsing data read from db to class
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString(); // "" names should be like in db fields of student
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString(); // poprawić zeby nie bylo hh:mm:ss
                    st.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
                    listOfStudents.Add(st);
                }
            }
            //con.Dispose(); // important to dispose connection after using them when we don't create connection in the using() block
            return Ok(listOfStudents);
        }


        [HttpGet("{indexNumber}")] // pasing param by URL segment

        public IActionResult GetEnrollmentByIndNo(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select e.IdEnrollment, e.semester, e.idstudy, e.startdate from enrollment e, student st where st.IndexNumber = @index and st.IdEnrollment = e.IdEnrollment";
                // 1 way of dealing
                // com.Parameters.AddWithValue("index", indexNumber);
                // ^ treat any pass to our URL as text (no possibility to do drop table or other action in URL - INJECTION ATTACK secure)
                // 2 way of dealing
                SqlParameter par1 = new SqlParameter();
                par1.ParameterName = "index";
                par1.Value = indexNumber;
                com.Parameters.Add(par1);

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read()) // if bc we search for single student
                {
                    var enr = new Enrollment();
                    enr.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
                    enr.Semester = Convert.ToInt32(dr["Semester"]);
                    enr.IdStudy = Convert.ToInt32(dr["IdStudy"]);
                    enr.StartDate = dr["StartDate"].ToString();
                    return Ok(enr);
                }
                return NotFound("Enrollment of student with this index number hasn't been found.");
            }
        }

/*
        [HttpGet("{indexNumber}")] // pasing param by URL segment
        // doesn't work
        public IActionResult GetEnrollmentByIndNoUsingProc(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "EnrollByIndoNoProc";
                com.CommandType = System.Data.CommandType.StoredProcedure;

                //com.Parameters.AddWithValue("IndexNumber", idex)
                // 1 way of dealing
                com.Parameters.AddWithValue("IndexNumber", indexNumber); // treat any pass to our URL as text (no possibility to do drop table or other action in URL)
                // 2 way of dealing
                // SqlParameter par1 = new SqlParameter();
                // par1.ParameterName = "index";
                // par1.Value = indexNumber;
                // com.Parameters.Add(par1);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read()) // if bc we search for single student
                {
                    var enr = new Enrollment();
                    enr.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
                    enr.Semester = Convert.ToInt32(dr["Semester"]);
                    enr.IdStudy = Convert.ToInt32(dr["IdStudy"]);
                    enr.StartDate = dr["StartDate"].ToString();
                    return Ok(enr);
                }
                return NotFound("Enrollment of student with this index number hasn't been found.");
            }
        }
        */

        // [HttpPost] // pasing param by the body
        // public IActionResult CreateSAndAddtudent(Student student) //import library
        // {
        //     var newIndexNum = $"s{new Random().Next(1, 20000)}";
        //     student.IndexNumber = newIndexNum;
        //     if (_dbService.IdExists(student.IdStudent) == false) return Ok(_dbService.AddStudent(student)); // adds student created with POST (with random sNumber) to list of students
        //     else return UnprocessableEntity("Student with this id already exits");
        //     //return Ok(student); // in case we want to see new added student
        // }

        // [HttpPut("{id}")] // To update: /api/students/10/?fName=Teodor&lName=Mako&indNum=666
        // public IActionResult EditStudent(int id, string fName = null, string lName = null, string indNum = null)
        // { // strings have default values so that not all props have to be updated
        //     if (_dbService.IdExists(id) == true) return Ok(_dbService.EditStudentById(id, fName, lName, indNum));
        //     else return NotFound("Student was not found");
        // }

        // [HttpDelete("{id}")]
        // public IActionResult RemoveStudent(int id)
        // {
        //     if (_dbService.IdExists(id) == true) return Ok(_dbService.RemoveStudentById(id));
        //     else return NotFound("Student was not found");
        // }
    }
}
