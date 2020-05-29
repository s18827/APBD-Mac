using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Tut11Proj.Models
{
    public class Doctor
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDoctor { get; set; }
        // [Required]
        public string FirstName { get; set; }
        // [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Prescription> Precriptions { get; set; }

    }
}