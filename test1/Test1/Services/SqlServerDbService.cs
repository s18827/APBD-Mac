using System.Net.Cache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Test1.DTOs.Responses;
using Test1.Models;

namespace Test1.Services
{
    public class SqlServerDbService : IDbService
    {
        private string ConnString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s18827;User ID=apbds18827;Password=admin";

        public GetTeamMemberInfoResponse GetTeamMemberInfo(int idTeamMember)
        {
            GetTeamMemberInfoResponse response = new GetTeamMemberInfoResponse();
            TeamMember tm = new TeamMember();
            List<TaskResp> tasksCreat = null;
            List<TaskResp> tasksAssign = null;
            // TaskResp task = null;

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetTeamMemberInfo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idTeamMember", idTeamMember));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            throw new ArgumentNullException("No data to be read");
                        }
                        tm.IdTeamMember = Convert.ToInt32(dr["IdTeamMember"].ToString());
                        tm.FirstName = dr["FirstName"].ToString();
                        tm.LastName = dr["LastName"].ToString();
                        tm.Email = dr["Email"].ToString();
                        response.TeamMember = tm;
                    }

                }

                using (SqlCommand cmd = new SqlCommand("GetTeamMemberTasksCr", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idTeamMember", idTeamMember));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        tasksCreat = new List<TaskResp>();
                        while (dr.Read())
                        {
                            tasksCreat.Add(new TaskResp()
                            {
                                Name = dr["Name"].ToString(),
                                Description = dr["Description"].ToString(),
                                Deadline = dr["Deadline"].ToString(),
                                NameOfProject = dr["ProjectName"].ToString(),
                                TaskType = dr["TypeName"].ToString()
                            });
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand("GetTeamMemberTasksAs", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idTeamMember", idTeamMember));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        tasksAssign = new List<TaskResp>();
                        while (dr.Read())
                        {
                            tasksAssign.Add(new TaskResp()
                            {
                                Name = dr["Name"].ToString(),
                                Description = dr["Description"].ToString(),
                                Deadline = dr["Deadline"].ToString(),
                                NameOfProject = dr["ProjectName"].ToString(),
                                TaskType = dr["TypeName"].ToString()
                            });
                        }
                    }
                }
                // if (tasksCreat.Capacity == 0 && tasksAssign.Capacity == 0) throw new ArgumentNullException("No created tasks to be read for this team memeber.");
                // above is not necessary I think
                response.TasksCreated = tasksCreat;
                response.TasksAssigned = tasksAssign;
            }
            return response;
        }

        public string RemoveProject(int idProject)
        {
            string deleteInfo = null;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteProject", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idProject", idProject));
                    cmd.ExecuteNonQuery();

                    deleteInfo = "deleted " + cmd.ExecuteNonQuery() + " rows";
                }
            }
            return deleteInfo;
        }

    }
}