using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Entities
{
    [Table("BreedType")]
    public class BreedType
    {
        public int IdBreedType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }

    }
}