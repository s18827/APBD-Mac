using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace Tut7Proj.DTOs.Requests
{
    /*
{
    "Studies": "Informatyka dzienne",
    "Semester": 1
}
    */
    
    public class PromoteStudentsRequest
    {
        [Required]
        [MaxLength(100)]
        public string Studies { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}