using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExTest2.DTOs.Responses;
using ExTest2.Entities;

namespace ExTest2.Services
{
    public interface IDbService
    {
        Task<IEnumerable<OrderResponse>> ListOrders(string custName);

        Task<Customer> GetCustomerWhereName(string name);

    }
}