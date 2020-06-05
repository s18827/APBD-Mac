using ExTest2.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ExTest2.Entities
{
    public class s18827DbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Confectionery> Confectioneries { get; set; }

        public DbSet<Confectionery_Order> Confectioneries_Orders { get; set; }

        public s18827DbContext() { }
        public s18827DbContext(DbContextOptions options)
            : base(options) { } // constructor from superclass - we use it with options => ... in Startup.cs to pass conection string

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // to not mix bussiness logic with DataAdnotations

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeEfConfiguration());

            modelBuilder.ApplyConfiguration(new CustomerEfConfiguration());

            modelBuilder.ApplyConfiguration(new OrderEfConfiguration());

            modelBuilder.ApplyConfiguration(new ConfectioneryEfConfiguration());

            modelBuilder.ApplyConfiguration(new Confectionery_OrderEfConfiguration());
        }
    }
}