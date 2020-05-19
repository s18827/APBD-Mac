using System;
using System.Collections.Generic;

namespace Tut10Proj.Models
{
    public partial class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }

        public virtual Enrollment Enrollment { get; set; } // changed name
    }
}
