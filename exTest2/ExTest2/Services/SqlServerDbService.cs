using System.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ExTest2.Entities;

namespace ExTest2.Services
{
    public class SqlServerDbService : IDbService
    {
        private readonly s18827DbContext _context;

        public SqlServerDbService(s18827DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> ListOrders(string custName)
        {
            IEnumerable<Order> ordersList = null;
            if (custName == null) ordersList = await _context.Orders.ToListAsync();
            else
            {
                var custFound = GetCustomerWhereName(custName);
                var idFoundCust = custFound.Result.IdCustomer;
                ordersList = await _context.Orders.Where(o => o.IdCustomer == idFoundCust).ToListAsync();
            }
            return ordersList;
        }

        public Task<Customer> GetCustomerWhereName(string name)
        {
            var cust = _context.Customers.FirstOrDefaultAsync(c => c.Name == name);
            if (cust == null) throw new ArgumentNullException("Customer with given name not found");
            return cust;
        }
    }
}
