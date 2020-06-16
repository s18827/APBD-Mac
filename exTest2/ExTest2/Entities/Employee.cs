using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExTest2.Entities
{
    [Table("Employee")]

    public class Employee
    {
        public int IdEmployee { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}