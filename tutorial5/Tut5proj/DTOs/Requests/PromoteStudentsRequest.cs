using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace Tut5proj.DTOs.Requests
{
    /*{
     "Studies": "IT",
     "Semester": 1
     }*/
    public class PromoteStudentsRequest
    {
        [Required]
        [MaxLength(100)]
        public string StudyName { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}