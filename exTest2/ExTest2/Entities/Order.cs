using System;
using System.Collections.Generic;

namespace ExTest2.Entities
{
    public class Order
    {
        public int IdOrder { get; set; }

        public DateTime DateAccepted { get; set; }

        public DateTime DateFinished { get; set; }

        public string Notes { get; set; }

        public int IdCustomer { get; set; }
        public Customer Customer { get; set; }

        public int IdEmployee { get; set; }
        public Employee Employee { get; set; }

        public virtual ICollection<Confectionery_Order> Confectioneries_Orders { get; set; }

    }
}