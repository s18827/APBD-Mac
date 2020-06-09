using System.Xml.Linq;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ExTest2.DTOs;
using ExTest2.DTOs.Responses;
using ExTest2.DTOs.Requests;
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

        public async Task<IEnumerable<GetOrderResponse>> ListOrders(string custName)
        {
            IEnumerable<GetOrderResponse> ordersRespList = null;
            if (custName == null) ordersRespList = await _context.Orders.Select(or => new GetOrderResponse
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
                ordersRespList = await _context.Orders.Where(o => o.IdCustomer == idFoundCust).Select(or => new GetOrderResponse
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

        public async Task<GetOrderResponse> AddOrder(AddOrderRequest request)
        {
            GetOrderResponse resp = null;

            List<string> missingProd = null;
            foreach (ConfectioneryProduct cp in request.Confectionery)
            {
                missingProd = new List<string>();
                var product = await GetConfectioneryWhereName(cp.Name);
                if (product == null) missingProd.Add(product.Name);
                if (missingProd.Count != 0) throw new ArgumentNullException("Confectioneries: {" + string.Join(", ", missingProd) + "} not found");
            }

            return resp;
        }

        public Task<Confectionery> GetConfectioneryWhereName(string name)
        {
            var conf = _context.Confectioneries.FirstOrDefaultAsync(c => c.Name == name);
            return conf;
        }
        public Task<Order> CreateOrder(DateTime dateAccepted, string notes, Confectionery_Order co_or)
        {

        }
    }
}
