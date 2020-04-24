using System.Collections.Generic;
using Test1.DTOs.Responses;
using Test1.Models;

namespace Test1.Services
{
    public interface IDbService
    {
        GetTeamMemberInfoResponse GetTeamMemberInfo(int idTeamMember);

        string RemoveProject(int idProject);
    }
}