using System.Collections;
using System.Collections.Generic;
using Test1.Models;
using Test1.Services;


namespace Test1.DTOs.Responses
{
    public class GetTeamMemberInfoResponse
    {
        public TeamMember TeamMember { get; set; }
        public IEnumerable<TaskResp> TasksCreated { get; set; }
        public IEnumerable<TaskResp> TasksAssigned { get; set; }

    }
}