using System;

namespace ExTest2.Entities
{
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