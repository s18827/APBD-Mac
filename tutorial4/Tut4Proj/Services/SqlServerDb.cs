using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Tut4Proj.Models;

namespace Tut4Proj.Services
{
    public class SqlServerDb : IStudentsDb
    {
        // we could have some repository or sth patterns to reuse our code
        // Repository pattern
        // or Unit of work + ORM
        // we'll focus on that later

        private string ConnString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s18827;User ID=apbds18827;Password=admin";

        public IEnumerable<Student> GetStudents()
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
                dr.Close();
                dr.Dispose();
            }
            //con.Dispose(); // important to dispose connection after using them when we don't create connection in the using() block
            return listOfStudents;
        }

        public Enrollment GetEnrollmentByIndNo(string indexNumber)
        {
            var enr = new Enrollment();
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select e.IdEnrollment, e.semester, e.idstudy, e.startdate from enrollment e, student st where st.IndexNumber = @index and st.IdEnrollment = e.IdEnrollment";
                // 1 way of dealing
                // com.Parameters.AddWithValue("index", indexNumber);
                // ^ treat any pass to our URL as text (no possibility to do drop table or other action in URL - INJECTION ATTACK resistance)
                // 2 way of dealing
                SqlParameter par1 = new SqlParameter();
                par1.ParameterName = "index";
                par1.Value = indexNumber;
                com.Parameters.Add(par1);

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read()) // if bc we search for single enrollment for that student
                {
                    enr.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
                    enr.Semester = Convert.ToInt32(dr["Semester"]);
                    enr.IdStudy = Convert.ToInt32(dr["IdStudy"]);
                    enr.StartDate = dr["StartDate"].ToString();
                }
                dr.Close();
                dr.Dispose();
            }
            return enr;
        }

        public bool StudentExists(string indexNumber)
        {
            var st = new Student();
            using (SqlConnection con = new SqlConnection(ConnString)) // managining connection with db
            using (SqlCommand com = new SqlCommand()) // manages sqlQuerries or other commands send to db
            {
                com.Connection = con;
                com.CommandText = "select * from student where IndexNumber = @index";
                com.Parameters.AddWithValue("index", indexNumber);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read()) return true;
                dr.Close();
                dr.Dispose();
            }
            return false;
        }
    }
}