using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExTest2.DTOs.Requests;
using ExTest2.DTOs.Responses;
using ExTest2.Entities;

namespace ExTest2.Services
{
    public interface IDbService
    {
        Task<IEnumerable<GetOrderResponse>> ListOrders(string custName);

        Task<Customer> GetCustomerWhereName(string name);

        Task<Order> AddOrder(AddOrderRequest newOrder);

        Task<Confectionery_Order> CreateNewCon_Ord(int idConfectionery, int idOrder, int quantity, string notes);

        Task<Confectionery> GetConfectioneryWhereName(string name);

        Task<Order> CreateNewOrder(DateTime dateAccepted, string notes);

        Task<int> GetMaxIdOrder();
    }
}