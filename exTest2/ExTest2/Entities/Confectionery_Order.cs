using System;

namespace ExTest2.Entities
{
    public class Confectionery_Order
    {
        public int IdConfectionery { get; set; }

        public virtual Confectionery Confectionery { get; set; }
        public int IdOrder { get; set; }

        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
    }
}