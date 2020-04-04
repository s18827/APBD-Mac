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

        // TODO
        // 1.(Done) Validation - [ApiController]
        // 2. Check if studies from request exist -> if not 404
        // 3. Check if enrollment that points to the specific studies that the student wants to enroll exists and semester = 1 -> INSERT, setup StartDate = CurrDate
        // 4. Create new student (if index of new student (from request) exists) -> 400, else INSERT new Student
        // 5. return Enrollment mode;

        // all in one transaction
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response = new EnrollStudentResponse();
            Enrollment respEnrollment = new Enrollment();
            SqlTransaction tran = null;

            using (var con = new SqlConnection(ConnString))
            using (var com = new SqlCommand())
            {
                // try
                // {
                #region 2.
                // statement1
                com.CommandText = "SELECT * FROM Studies WHERE Name = @StudyName";
                //string cmd1Text = "SELECT * FROM Studies WHERE Name = @Name";
                //SqlCommand cmd1 = new SqlCommand(cmd1Text, con, tran),
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
                    return response; // null
                }
                int idStudy = (int)dr["IdStudy"]; // needed for 3. - IS THIS CASTING OK???
                dr.Close();
                #endregion

                #region 3.
                int idEnrollment;
                // statement2 - searching for existing enrollment
                com.CommandText = "SELECT * FROM Enrollment WHERE Semester = 1 AND IdStudy = @IdStudy";
                com.Parameters.AddWithValue("IdStudy", idStudy);
                com.Transaction = tran;
                dr = com.ExecuteReader();
                if (dr.Read()) // select IdEnrollment of existing Enrollment for later new Student creation
                {
                    // dr.Close();
                    // com.Transaction = tran;
                    // dr = com.ExecuteReader();
                    idEnrollment = (int)dr["IdEnrollment"]; // DOES THIS CASTING/CONVERSION WORK???
                }
                else // if given enrollment doesn't exist
                {
                    dr.Close();
                    // statement3 - setting up new IdEnrollment
                    com.CommandText = "SELECT max(IdEnrollment) FROM Enrollment";
                    com.Transaction = tran;
                    dr = com.ExecuteReader();
                    int newIdEnrollment = dr.GetInt32(0) + 1;
                    
                    dr.Close();
                    // statement4 - inserting new Enrollment
                    com.CommandText = "INSERT INTO Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES (@NewIdEnrollment, 1, @IdStudy, convert(varchar, getdate(), 104))";
                    com.Parameters.AddWithValue("IdStudy", idStudy);
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
                    return response; // null
                }
                dr.Close();

                /* not needed since we have new IndexNumber from request
                // statementADDITIONAL - setting up new IndexNumber for Student
                com.CommandText = "SELECT max(IndexNumber) FROM Student";
                dr = com.ExecuteReader();
                string strIndNum = dr["IndexNumber"].ToString();
                int newIndexNumber = Convert.ToInt32(strIndNum.Substring(1, strIndNum.Length - 1)) + 1;
                // ^substring instead of regex (ommiting s)
                */

                // statement6 - inserting new Student
                com.CommandText = "INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (@IndexNumber, @Firstname, @LastName, convert(datetime, @BirthDate, 104), @IdEnrollment)";
                //!!! com.Parameters.AddWithValue("IndexNumber", request.IndexNumber); // nie musze powtarzac?
                com.Parameters.AddWithValue("FirstName", request.FirstName);
                com.Parameters.AddWithValue("LastName", request.LastName);
                com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                com.Transaction = tran;
                com.ExecuteNonQuery();
                dr.Close();
                #endregion

                #region 5. chyba moge uzyc Enrollmentu ktorego uzywalem wyzej
                // statement7 - filling in response data
                com.CommandText = "SELECT * FROM Enrollment WHERE IdEnrollment = @IdEnrollment";
                //!!! com.Parameters.AddWithValue("IdEnrollment", idEnrollment); // DO I HAVE TO REPEAT THAT
                com.Transaction = tran;
                dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    dr.Dispose();
                    System.Console.WriteLine("ERROR AT THE END!"); // no exception handling bc it has to exist - we made sure about that earlier
                }
                response.Semester = 1; // JUST THAT?
                response.Enrollment = respEnrollment;
                respEnrollment.IdEnrollment = (int)dr["IdEnrollment"];
                respEnrollment.Semester = (int)dr["Semester"];
                respEnrollment.IdStudy = (int)dr["IdStudy"];
                respEnrollment.StartDate = dr["StartDate"].ToString();
                #endregion
                
                dr.Close();
                dr.Dispose();
                tran.Commit(); // unsell this comit is issued we can still enroll everytingk
            }
            // catch (SqlException)
            // {
            //     tran.Rollback();
            // }
            // }
            return response;
        }


        public void PromoteStudets(int semester, string studies)
        {
            throw new System.NotImplementedException();
        }
    }
}