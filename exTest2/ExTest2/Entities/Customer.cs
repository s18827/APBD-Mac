using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExTest2.Entities
{
    [Table("Customer")]
    public class Customer
    {
        public int IdCustomer { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}