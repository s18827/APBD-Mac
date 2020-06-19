using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Entities
{
    [Table("Pet")]
    public class Pet
    {
        public int IdPet { get; set; }
        public int IdBreedType { get; set; }
        public BreedType BreedType { get; set; }
        public string Name { get; set; }
        public bool IsMale { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime ApproxDateOfBirth { get; set; }
        public DateTime? DateAdopted { get; set; }

        public virtual ICollection<Volunteer_Pet> Volunteers_Pets { get; set; }

    }
}