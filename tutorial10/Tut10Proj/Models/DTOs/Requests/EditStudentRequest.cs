using System;

namespace Tut10Proj.Models.DTOs.Requests
{
    public class EditStudentRequest
    {
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        public DateTime? BirthDate { get; set; } = null;
        public int? IdEnrollment { get; set; } = null;
    }
}