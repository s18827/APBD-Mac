using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Tut11Proj.Models
{
    public class Prescription_Medicament
    {
        // [Key]
        // [ForeignKey("Medicament")]
        public int IdMedicament { get; set; }
        public Medicament Medicament { get; set; }

        // [Key]
        // [ForeignKey("Prescription")]
        public int IdPrescription { get; set; }
        public Prescription Prescription { get; set; }

        public int Dose { get; set; }
        public string Details { get; set; }
    }
}