using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tut11Proj.Entities
{
    public class Medicament
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMedicament { get; set; }
        // [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public ICollection<Prescription_Medicament> Prescriptions_Medicaments { get; set; }
    }
}