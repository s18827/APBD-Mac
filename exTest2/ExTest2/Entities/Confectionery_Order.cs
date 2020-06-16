using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExTest2.Entities
{
    [Table("Confectionery_Order")]
    public class Confectionery_Order
    {
        public int IdConfectionery { get; set; }

        public Confectionery Confectionery { get; set; }
        public int IdOrder { get; set; }

        public Order Order { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
    }
}