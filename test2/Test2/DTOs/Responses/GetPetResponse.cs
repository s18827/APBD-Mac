using System;
using System.Collections.Generic;
using Test2.Entities;

namespace Test2.DTOs.Responses
{
    public class GetPetResponse
    {
        public int IdPet { get; set; }
        public int IdBreedType { get; set; }
        public string Name { get; set; }
        public bool IsMale { get; set; }
        public DateTime DateRegistered { get; set; } // sorted ascending
        public DateTime ApproxDateOfBirth { get; set; }
        public DateTime? DateAdopted { get; set; }

        public IEnumerable<GetVolunteerResponse> VolunteerList { get; set; }
    }
}