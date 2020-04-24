using System.Collections.Generic;
using ExTest1.Models;

namespace ExTest1.DTOs.Requests
{
    public class AddAnimalRequest
    {
        public Animal Animal { get; set; }
        public IEnumerable<Procedure> PastTreatments { get; set; }
      
    }
}