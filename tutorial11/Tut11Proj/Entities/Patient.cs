using System.Collections;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Tut11Proj.Entities
{
    public class Patient
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPatient { get; set; }

        // [Required]
        public string FirstName { get; set; }
        // [Required]
        public string LastName { get; set; }
        // [Required]
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Prescription> Precriptions { get; set; }
    }
}