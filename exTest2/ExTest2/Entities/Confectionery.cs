using System;
using System.Collections.Generic;

namespace ExTest2.Entities
{
    public class Confectionery
    {
        public int IdConfectionery { get; set; }

        public string Name { get; set; }

        public float PricePerItem { get; set; }

        public string Type { get; set; }

        public ICollection<Confectionery_Order> Confectioneries_Orders { get; set; }
    }
}