using System;

namespace Tut10Proj.Models.DTOs.Requests
{
    public class AddStudentRequest
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
         public int IdEnrollment { get; set; }
    }
}