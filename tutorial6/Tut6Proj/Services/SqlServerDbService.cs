using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Tut6Proj.DTOs.Requests;
using Tut6Proj.DTOs.Responses;
using Tut6Proj.Models;

namespace Tut6Proj.Services
{
    public class SqlServerDbService : IDbService
    {
        private string ConnString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s18827;User ID=apbds18827;Password=admin";

        #region StudentsController
        
        public IEnumerable<Student> GetStudents()
        {
            var listOfStudents = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Student";

                con.Open();
                SqlDataReader dr = com.ExecuteReader(); 
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString(); // "" names should be like in db fields of Student
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
                    listOfStudents.Add(st);
                }
            }
            return listOfStudents;
        }

        public Enrollment GetEnrollOfStudByIndNo(string indexNumber)
        {
            var enr = new Enrollment();
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT e.IdEnrollment, e.semester, e.idstudy, e.startdate FROM Enrollment e, Student st WHERE st.IndexNumber = @index AND st.IdEnrollment = e.IdEnrollment";
                com.Parameters.AddWithValue("index", indexNumber);

                // diffrent way for the above:
                // SqlParameter par1 = new SqlParameter();
                // par1.ParameterName = "index";
                // par1.Value = indexNumber;
                // com.Parameters.Add(par1);

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    throw new ArgumentNullException("Enrollment of student with given index number hasn't been found");
                }
                enr.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
                enr.Semester = Convert.ToInt32(dr["Semester"]);
                enr.IdStudy = Convert.ToInt32(dr["IdStudy"]);
                enr.StartDate = dr["StartDate"].ToString();
            }
            return enr;
        }

        public Student GetStudentByIndex(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Student WHERE IndexNumber = @index";
                com.Parameters.AddWithValue("index", indexNumber);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString(); // "" names should be like in db fields of student
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
                    return st;
                }
            }
            return null;
        }

        #endregion

        #region EnrollmentsController
    
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response = null;
            Enrollment respEnrollment = new Enrollment();
            SqlTransaction tran = null;

            using (var con = new SqlConnection(ConnString))
            using (var com = new SqlCommand())
            {
                #region 2.
                // statement1
                com.CommandText = "SELECT * FROM Studies WHERE Name = @StudyName";
                com.Parameters.AddWithValue("StudyName", request.Studies);

                com.Connection = con;
                con.Open();
                tran = con.BeginTransaction();

                com.Transaction = tran;
                SqlDataReader dr = com.ExecuteReader();
                if (!dr.Read()) // if given studies don't exist
                {
                    // return NotFound("Studies not found");
                    dr.Close();
                    tran.Rollback();
                    dr.Dispose();
                    throw new ArgumentException("Studies not found");
                }
                int idStudy = (int)dr["IdStudy"]; // needed for 3.
                dr.Close();
                #endregion

                #region 3.
                int idEnrollment = 0;
                // statement2 - searching for existing enrollment
                com.CommandText = "SELECT * FROM Enrollment WHERE Semester = 1 AND IdStudy = @IdStudy";
                com.Parameters.AddWithValue("IdStudy", idStudy);
                com.Transaction = tran;
                dr = com.ExecuteReader();
                if (dr.Read()) // select IdEnrollment of existing Enrollment for later new Student creation
                {
                    idEnrollment = (int)dr["IdEnrollment"]; // DOES THIS CASTING/CONVERSION WORK???
                }
                else // if given enrollment doesn't exist
                {
                    dr.Close();
                    // statement3 - setting up new IdEnrollment
                    // com.CommandText = "SELECT CAST(MAX(IdEnrollment) AS integer) FROM Enrollment)"; // I wanted to do sth like that but id doesn't seem to read any value even using dr.GetInt32(0) ...
                    com.CommandText = "SELECT IdEnrollment FROM Enrollment";
                    com.Transaction = tran;
                    dr = com.ExecuteReader();
                    int newIdEnrollment = 0;
                    while (dr.Read())
                    {
                        int maxIdEnrollment = (int)dr["IdEnrollment"];
                        if (maxIdEnrollment > newIdEnrollment) newIdEnrollment = maxIdEnrollment;
                    }
                    newIdEnrollment++; // maxEnrolmentId + 1
                    dr.Close();

                    // statement4 - inserting new Enrollment
                    com.CommandText = "INSERT INTO Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES (@NewIdEnrollment, 1, @IdStudy, convert(varchar, getdate(), 110))";
                    // com.Parameters.AddWithValue("IdStudy", idStudy);
                    com.Parameters.AddWithValue("NewIdEnrollment", newIdEnrollment);
                    com.Transaction = tran;
                    com.ExecuteNonQuery();
                    idEnrollment = newIdEnrollment;
                }
                dr.Close();
                #endregion

                #region 4.
                // statement5 - check if Student with index from request alerady exists
                com.CommandText = "SELECT * FROM Student WHERE IndexNumber = @IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                com.Transaction = tran;
                dr = com.ExecuteReader();
                if (dr.Read()) // if Student with index from request already exists
                {
                    dr.Close();
                    tran.Rollback();
                    dr.Dispose();
                    throw new InvalidOperationException("Student with given index number already exists");
                }
                dr.Close();

                #region additional - not needed since we have new IndexNumber from request
                /* 
                // statementADDITIONAL - setting up new IndexNumber for Student
                com.CommandText = "SELECT max(IndexNumber) FROM Student";
                dr = com.ExecuteReader();
                string strIndNum = dr["IndexNumber"].ToString();
                int newIndexNumber = Convert.ToInt32(strIndNum.Substring(1, strIndNum.Length - 1)) + 1;
                // ^substring instead of regex (ommiting s)
                */
                #endregion

                // statement6 - inserting new Student
                com.CommandText = "INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (@IndexNumber, @Firstname, @LastName, convert(datetime, @BirthDate, 104), @IdEnrollment)";
                com.Parameters.AddWithValue("FirstName", request.FirstName);
                com.Parameters.AddWithValue("LastName", request.LastName);
                com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                com.Transaction = tran;
                com.ExecuteNonQuery();
                dr.Close();
                #endregion

                #region 5. RESPONSE
                // statement7 - filling in response data
                com.CommandText = "SELECT * FROM Enrollment WHERE IdEnrollment = @IdEnrollment";
                com.Transaction = tran;
                dr = com.ExecuteReader();
                dr.Read(); // no exception handling needed bc it has to exist - we made sure about that earlier

                response = new EnrollStudentResponse();
                response.Semester = 1; // JUST THAT?
                response.Enrollment = respEnrollment;
                respEnrollment.IdEnrollment = (int)dr["IdEnrollment"];
                respEnrollment.Semester = (int)dr["Semester"];
                respEnrollment.IdStudy = (int)dr["IdStudy"];
                respEnrollment.StartDate = dr["StartDate"].ToString();
                #endregion

                dr.Dispose(); // dispose == close ?
                tran.Commit();
            }
            return response;
        }

        public PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request)
        {
            if (request.Studies == null || request.Semester == 0)
            {
                throw new ArgumentNullException("Incorrect request");
            }
            PromoteStudentsResponse response = null;
            Enrollment respEnrollment = new Enrollment();

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("PromoteStudents", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@StudyName", request.Studies));
                cmd.Parameters.Add(new SqlParameter("@Semester", request.Semester));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                    {
                        cmd.Dispose();
                        throw new ArgumentException("Nothing to be read by SqlDataReader");
                    }
                    response = new PromoteStudentsResponse();
                    response.Enrollment = respEnrollment;
                    respEnrollment.IdEnrollment = (int)dr["IdEnrollment"];
                    respEnrollment.Semester = (int)dr["Semester"];
                    respEnrollment.IdStudy = (int)dr["IdStudy"];
                    respEnrollment.StartDate = dr["StartDate"].ToString();
                }
                cmd.Dispose();
            }
            return response;
        }

        #endregion
    }
}