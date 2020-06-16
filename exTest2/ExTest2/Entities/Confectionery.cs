using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExTest2.Entities
{
    [Table("Confectionery")]
    public class Confectionery
    {
        public int IdConfectionery { get; set; }

        public string Name { get; set; }

        public float PricePerItem { get; set; }

        public string Type { get; set; }

        public virtual ICollection<Confectionery_Order> Confectioneries_Orders { get; set; }
    }
}