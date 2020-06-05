using System;
using System.Collections.Generic;

namespace ExTest2.Entities
{
    public class Customer
    {
        public int IdCustomer { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}