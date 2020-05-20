
using System;

namespace Tut10Proj.Models.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }

        // public override string ToString()
        // {
        //     return "Student has been enrolled to semester = " + Semester + ", \n" + Enrollment.ToString();
        // }
    }
}