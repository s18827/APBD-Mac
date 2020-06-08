using System.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ExTest2.Entities;
using ExTest2.DTOs.Responses;

namespace ExTest2.Services
{
    public class SqlServerDbService : IDbService
    {
        private readonly s18827DbContext _context;

        public SqlServerDbService(s18827DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderResponse>> ListOrders(string custName)
        {
            IEnumerable<OrderResponse> ordersRespList = null;
            if (custName == null) ordersRespList = await _context.Orders.Select(or => new OrderResponse
            {
                IdOrder = or.IdOrder,
                    DateAccepted = or.DateAccepted,
                    DateFinished = or.DateFinished,
                    Notes = or.Notes,
                    IdCustomer = or.IdCustomer,
                    IdEmployee = or.IdEmployee
            }).ToListAsync();
            else
            {
                var custFound = await GetCustomerWhereName(custName);
                if (custFound == null) throw new ArgumentNullException("Customer with given name not found");
                var idFoundCust = custFound.IdCustomer;
                ordersRespList = await _context.Orders.Where(o => o.IdCustomer == idFoundCust).Select(or => new OrderResponse
                {
                    IdOrder = or.IdOrder,
                    DateAccepted = or.DateAccepted,
                    DateFinished = or.DateFinished,
                    Notes = or.Notes,
                    IdCustomer = or.IdCustomer,
                    IdEmployee = or.IdEmployee
                }).ToListAsync();
            }
            return ordersRespList;
        }

        public Task<Customer> GetCustomerWhereName(string name)
        {
            var cust = _context.Customers.FirstOrDefaultAsync(c => c.Name == name);
            return cust;
        }
    }
}
