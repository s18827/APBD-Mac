using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tut11Proj.Models
{
    public class Prescription
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPrescription { get; set; }
        // [Required]
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }


        // [ForeignKey("Patient")]
        public int IdPatient { get; set; } // I think it shuldn't be 'int?'
        public virtual Patient Patient { get; set; }


        // [ForeignKey("Doctor")]
        public int? IdDoctor { get; set; } // foreign key used for Doctor Doctor
        public virtual Doctor Doctor { get; set; }


        public ICollection<Prescription_Medicament> Prescriptions_Medicaments { get; set; }

    }
}