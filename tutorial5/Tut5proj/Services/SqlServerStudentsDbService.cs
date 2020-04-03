using System.Data.SqlClient;
using Tut5proj.DTOs.Requests;

namespace Tut5proj.Services
{
    public class SqlServerStudentsDbService : IStudentsServiceDb
    {
        public void EnrollStudent(EnrollStudentRequest request)
        {
            // try{
            using (var con = new SqlConnection("constring"))
            using (var com = new SqlCommand())
            {
                com.CommandText = "SELECT * FROM Studeis WHRE Name = @Name";
                com.Parameters.AddWithValue("Name", request.Studies);
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();

                // 2. To EXECUTE 1 statement
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    // return NotFound("Studies not found");
                }
                int idStudies = (int)dr["IdStudies"]; // apply regex to get rid of 's'
                                                      // 3.
                com.CommandText = "Select * from Enrollment Where Semester = 1 AND IdStudeies = @IdStud";

                // 4.

                // x.. INSERT Student

                com.CommandText = "INSERT INFO Student (IndexNumber, FirstName, LastName) VALUES (@Fiestname, @LastName ...";

                com.Parameters.AddWithValue("FirstName", request.FirstName);

                com.ExecuteNonQuery();

                tran.Commit(); // unsell this comit is issued we can still enroll everytingk
            }
            // }
            // catch (SqlException)
            // {
            //     tran.Rollback();
            // }
        }

        public void PromoteStudets(int semester, string studies)
        {
            throw new System.NotImplementedException();
        }
    }
}