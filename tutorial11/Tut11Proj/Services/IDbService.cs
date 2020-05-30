using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tut11Proj.DTOs.Requests;
using Tut11Proj.DTOs.Responses;
using Tut11Proj.Entities;

namespace Tut11Proj.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Doctor>> ListDoctors();

        Task<Doctor> GetDoctor(int idDoctor);

        // Task<string> GetDoctorEmail(int idDoctor);
        Task<Doctor> GetDocWithEmail(string email);

        Task<Doctor> AddDoctor(Doctor doctor);

        Task ModifyDoctor(Doctor doctor);

        Task DeleteDoctor(int idDoctor);
        
    }
}