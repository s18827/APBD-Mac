
using System;

namespace Tut10Proj.Models.DTOs.Responses
{
    public class PromoteStudentsResponse
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }
        
    }
}