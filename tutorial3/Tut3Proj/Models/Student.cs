using System;

namespace Tut3Proj.Models
{
    public class Student
    {
        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + ", " + IndexNumber;
        }
    }
}