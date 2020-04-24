using System.Net.Cache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ExTest1.DTOs.Responses;
using ExTest1.DTOs.Requests;
using ExTest1.Models;

namespace ExTest1.Services
{
    public class SqlServerDbService : IDbService
    {
        private string ConnString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s18827;User ID=apbds18827;Password=admin";

        public GetAnimalsResponse GetAnimals(string sortBy)
        {
            GetAnimalsResponse response = null;
            List<AnimalFoundTemplate> respList = null;
            AnimalFoundTemplate animalTempl = null;

            if (sortBy == null) sortBy = "AdmissionDate";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetAnimals", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@sortBy", sortBy));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    respList = new List<AnimalFoundTemplate>();
                    while (dr.Read())
                    {
                        animalTempl = new AnimalFoundTemplate();
                        animalTempl.Name = dr["Name"].ToString();
                        animalTempl.Type = dr["Type"].ToString();
                        animalTempl.DateOfAdmission = dr["AdmissionDate"].ToString();
                        animalTempl.LastNameOfOwner = dr["LastName"].ToString();
                        respList.Add(animalTempl);
                    }

                    if (respList.Capacity == 0)
                    {
                        cmd.Dispose();
                        throw new ArgumentException("No animals found");
                    }

                    response = new GetAnimalsResponse();
                    response.List = respList;
                }
                cmd.Dispose();
            }
            return response;
        }

        public AddAnimalResponse AddAnimal(AddAnimalRequest request)
        {
            AddAnimalResponse response = null;
            // List<string> 
            List<string> respMessages = null;

            // spAddAnimal:
            // 1. check if Owner exists /yes -> good, no -> throw error/
            // 2. check if Animal elready exists /yes -> throw error, no -> go to 3./
            // 3. add Animal            

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddAnimal", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idAnimal", request.Animal.IdAnimal)); // make that automatic?
                    cmd.Parameters.Add(new SqlParameter("@name", request.Animal.Name));
                    cmd.Parameters.Add(new SqlParameter("@type", request.Animal.Type));
                    cmd.Parameters.Add(new SqlParameter("@admissionDate", request.Animal.AdmissionDate));
                    cmd.Parameters.Add(new SqlParameter("@idOwner", request.Animal.IdOwner));

                    respMessages = new List<string>();

                    cmd.ExecuteNonQuery();
                }
                respMessages.Add($"Animal {request.Animal.Name} has been succesfully added to the database");

                // ADDPROCEDURE PART:


                foreach (Procedure proc in request.PastTreatments)
                {
                    using (SqlCommand cmd2 = new SqlCommand("AddProcedure", conn))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;

                        cmd2.Parameters.Add(new SqlParameter("@idProcedure", proc.IdProcedure)); // make that automatic?
                        cmd2.Parameters.Add(new SqlParameter("@name", proc.Name));
                        cmd2.Parameters.Add(new SqlParameter("@desc", proc.Description));
                        cmd2.Parameters.Add(new SqlParameter("@idAnimal", request.Animal.IdAnimal));

                        cmd2.ExecuteNonQuery();
                        respMessages.Add($"Procedure {proc.Name} has been succesfully assigned to animal {request.Animal.Name}");
                    }
                }
                response = new AddAnimalResponse();
                response.Messages = respMessages;
            }
            return response;
        }



        // public Enrollment GetEnrollOfStudByIndNo(string indexNumber)
        // {
        //     var enr = new Enrollment();
        //     using (SqlConnection con = new SqlConnection(ConnString))
        //     using (SqlCommand com = new SqlCommand())
        //     {
        //         com.Connection = con;
        //         com.CommandText = "SELECT e.IdEnrollment, e.semester, e.idstudy, e.startdate FROM Enrollment e, Student st WHERE st.IndexNumber = @index AND st.IdEnrollment = e.IdEnrollment";
        //         com.Parameters.AddWithValue("index", indexNumber);

        //         // diffrent way for the above:
        //         // SqlParameter par1 = new SqlParameter();
        //         // par1.ParameterName = "index";
        //         // par1.Value = indexNumber;
        //         // com.Parameters.Add(par1);

        //         con.Open();
        //         SqlDataReader dr = com.ExecuteReader();
        //         if (!dr.Read())
        //         {
        //             throw new ArgumentNullException("Enrollment of student with given index number hasn't been found");
        //         }
        //         enr.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
        //         enr.Semester = Convert.ToInt32(dr["Semester"]);
        //         enr.IdStudy = Convert.ToInt32(dr["IdStudy"]);
        //         enr.StartDate = dr["StartDate"].ToString();
        //     }
        //     return enr;
        // }

        // // example of searching by - modify for sorting by
        // public Student GetStudentByIndex(string indexNumber)
        // {
        //     using (SqlConnection con = new SqlConnection(ConnString))
        //     using (SqlCommand com = new SqlCommand())
        //     {
        //         com.Connection = con;
        //         com.CommandText = "SELECT * FROM Student WHERE IndexNumber = @index";
        //         com.Parameters.AddWithValue("index", indexNumber);
        //         con.Open();
        //         SqlDataReader dr = com.ExecuteReader();
        //         if (dr.Read())
        //         {
        //             var st = new Student();
        //             st.IndexNumber = dr["IndexNumber"].ToString();
        //             st.FirstName = dr["FirstName"].ToString(); // "" names should be like in db fields of student
        //             st.LastName = dr["LastName"].ToString();
        //             st.BirthDate = dr["BirthDate"].ToString();
        //             st.IdEnrollment = Convert.ToInt32(dr["IdEnrollment"]);
        //             return st;
        //         }
        //     }
        //     return null;
        // }

        // // example of using stored procedure
        // public PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request)
        // {
        //     if (request.Studies == null || request.Semester == 0)
        //     {
        //         throw new ArgumentNullException("Incorrect request");
        //     }
        //     PromoteStudentsResponse response = null;
        //     Enrollment respEnrollment = new Enrollment();

        //     using (SqlConnection conn = new SqlConnection(ConnString))
        //     {
        //         conn.Open();

        //         SqlCommand cmd = new SqlCommand("PromoteStudents", conn);

        //         cmd.CommandType = CommandType.StoredProcedure;

        //         cmd.Parameters.Add(new SqlParameter("@StudyName", request.Studies));
        //         cmd.Parameters.Add(new SqlParameter("@Semester", request.Semester));

        //         using (SqlDataReader dr = cmd.ExecuteReader())
        //         {
        //             if (!dr.Read())
        //             {
        //                 cmd.Dispose();
        //                 throw new ArgumentException("Nothing to be read by SqlDataReader");
        //             }
        //             response = new PromoteStudentsResponse();
        //             response.Enrollment = respEnrollment;
        //             respEnrollment.IdEnrollment = (int)dr["IdEnrollment"];
        //             respEnrollment.Semester = (int)dr["Semester"];
        //             respEnrollment.IdStudy = (int)dr["IdStudy"];
        //             respEnrollment.StartDate = dr["StartDate"].ToString();
        //         }
        //         cmd.Dispose();
        //     }
        //     return response;
        // }


        // log error data to file
        // public void SaveLogData(IEnumerable<string> logData)
        // {
        //     string logFilePath = "/Users/azyl/Git-Uni/APBD-Mac/tutorial6/Tut6Proj/requestsLog.txt";
        //     try
        //     {
        //         using (StreamWriter writer = File.AppendText(logFilePath))
        //         {
        //             foreach (string data in logData)
        //             {
        //                 writer.WriteLine(data);
        //             }
        //         }
        //     }
        //     catch (Exception) { }
        // }

    }

}