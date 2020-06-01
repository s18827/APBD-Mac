using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tut11Proj.DTOs.Requests;
using Tut11Proj.Entities;

namespace Tut11Proj.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Doctor>> ListDoctors();

        Task<Doctor> GetDoctor(int idDoctor);

        Task<Doctor> GetDoctorWhereId(int idDoctor);

        Task<Doctor> GetDocWhereEmail(string email);

        Task<Doctor> AddDoctor(Doctor doctor);

        Task ModifyDoctor(int idDoctor, EditDoctorRequest doctor);

        Task DeleteDoctor(int idDoctor);
        
    }
}