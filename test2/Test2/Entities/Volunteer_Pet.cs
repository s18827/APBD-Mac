using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Entities
{
    [Table("Volunteer_Pet")]

    public class Volunteer_Pet
    {
        public int IdVolunteer { get; set; }
        public Volunteer Volunteer { get; set; }

        public int IdPet { get; set; }
        public Pet Pet { get; set; }

        public DateTime DateAccepted { get; set; }

    }
}