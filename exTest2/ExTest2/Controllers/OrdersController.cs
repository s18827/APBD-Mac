using System;
using System.Threading.Tasks;
using ExTest2.DTOs.Requests;
using ExTest2.Entities;
using ExTest2.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExTest2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private IDbService _service;
        // private readonly s18827DbContext _context; // not needed in controller
        public OrdersController(IDbService service/*, s18827DbContext context*/)
        {
            _service = service;
            // _context = context;
        }

        [HttpGet] // to call: /api/orders?name=<name>
        public async Task<IActionResult> ListOrders(string name)
        {
            try
            {
                var ordersRespList = await _service.ListOrders(name);
                return Ok(ordersRespList);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound("Customer with given name not found");
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderRequest request)
        {
            try
            {
                var newOrder = await _service.AddOrder(request);
                return Ok(newOrder);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is ArgumentNullException) return NotFound(e.Message);
                    if (e is ArgumentException) return BadRequest();
                    else return BadRequest(e.StackTrace);
                }
                return null;
            }
        }

    }
}