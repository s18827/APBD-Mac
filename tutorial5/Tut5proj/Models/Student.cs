using System;
using System.ComponentModel.DataAnnotations;

namespace Tut5proj.Models
{
    public class Student
    {
        [Required(ErrorMessage="This fied is required")]
        // this field has to be specified before our code is even executed so its good to do it here insetead of in method respongind to HTTPrequest
        [MaxLength(10)]
        // but in fact its mixing validation with logic of creation so for big apps THERE IS BETTER WAY TO DO IT!- in separate class
        public string IndexNumber { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string BirthDate { get; set; }

        public int IdEnrollment { get; set; }

 
        // public override string ToString()
        // {
        //     return FirstName + " " + LastName + ", " + IndexNumber;
        // }
    }
}
