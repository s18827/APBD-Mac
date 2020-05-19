using System.Collections;
namespace Tut10Proj.Models.DTOs.Responses
{
    public class EditStudentResponse
    {
        public string EditMessage { get; set; }
        public IEnumerable EditedFields { get; set; }
    }
}