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
                DateFinished = (DateTime)or.DateFinished,
                Notes = or.Notes,
                IdCustomer = (int)or.IdCustomer,
                IdEmployee = (int)or.IdEmployee
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
                    DateFinished = (DateTime)or.DateFinished,
                    Notes = or.Notes,
                    IdCustomer = (int)or.IdCustomer,
                    IdEmployee = (int)or.IdEmployee
                }).ToListAsync();
            }
            return ordersRespList;
        }

        public Task<Customer> GetCustomerWhereName(string name)
        {
            var cust = _context.Customers.FirstOrDefaultAsync(c => c.Name == name);
            return cust;
        }

        public async Task<Order> AddOrder(AddOrderRequest request)
        {
            Order newOrder = await CreateNewOrder(request.DateAccepted, request.Notes);
            var idNewOrd = newOrder.IdOrder;

            foreach (ConfectioneryProduct cp in request.Confectionery)
            {
                var confProd = await GetConfectioneryWhereName(cp.Name);
                if (confProd == null) throw new ArgumentNullException("At least one Confectionery not found. Missing Confectionery: " + cp.Name);

                var idConf = confProd.IdConfectionery;
                var newCon_Or = await CreateNewCon_Ord(idConf, idNewOrd, cp.Quantity, cp.Notes);
            }

            return newOrder;
        }

        public async Task<Confectionery_Order> CreateNewCon_Ord(int idConfectionery, int idOrder, int quantity, string notes)
        {
            var newCon_ord = new Confectionery_Order
            {
                IdConfectionery = idConfectionery,
                IdOrder = idOrder,
                Quantity = quantity,
                Notes = notes
            };
            _context.Entry(newCon_ord).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return newCon_ord;
        }

        public Task<Confectionery> GetConfectioneryWhereName(string name)
        {
            var conf = _context.Confectioneries.FirstOrDefaultAsync(c => c.Name == name);
            return conf;
        }
        public async Task<Order> CreateNewOrder(DateTime dateAccepted, string notes)
        {
            var newIdOrder = await GetMaxIdOrder() + 1;

            var newOrder = new Order
            {
                IdOrder = newIdOrder,
                DateAccepted = dateAccepted,
                Notes = notes
                // IdCustomer = ,
                // IdEmployee = 
            };
            _context.Entry(newOrder).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return newOrder;
        }

        public Task<int> GetMaxIdOrder()
        {
            var res = _context.Orders.Select(o => o.IdOrder).MaxAsync();
            return res;
        }
    }
}
