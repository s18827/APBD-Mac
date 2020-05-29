using System;
using System.Collections.Generic;

namespace Tut10Proj.Entities
{
    public partial class Enrollment
    {
        public Enrollment()
        {
            Student = new HashSet<Student>();
        }

        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Studies Studies { get; set; } // changed name
        public virtual ICollection<Student> Student { get; set; }
    }
}
