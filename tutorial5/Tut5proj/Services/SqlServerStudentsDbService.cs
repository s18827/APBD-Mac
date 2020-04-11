using System.Data;
using System;
using System.Data.SqlClient;
using Tut5proj.Models;
using Tut5proj.DTOs.Requests;
using Tut5proj.DTOs.Responses;


namespace Tut5proj.Services
{
    public class SqlServerStudentsDbService : IStudentsServiceDb
    {

        private string ConnString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s18827;User ID=apbds18827;Password=admin";

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            // TODO
            // 1.Validation - [ApiController]
            // 2. Check if studies from request exist -> if not 404
            // 3. Check if enrollment that points to the specific studies that the student wants to enroll exists and semester = 1 -> INSERT, setup StartDate = CurrDate
            // 4. Create new student (if index of new student (from request) exists) -> 400, else INSERT new Student
            // 5. return Enrollment mode;

            // ^ all in one transaction
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

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("PromoteStudents", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@StudyName", request.Studies));
                cmd.Parameters.Add(new SqlParameter("@Semester", request.Semester));

                // execute the command
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
    }
}