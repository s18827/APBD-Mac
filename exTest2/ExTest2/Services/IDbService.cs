using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExTest2.Entities;

namespace ExTest2.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Order>> ListOrders(string custName);

        Task<Customer> GetCustomerWhereName(string name);

    }
}