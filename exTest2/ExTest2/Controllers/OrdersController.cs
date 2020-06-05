using System;
using System.Threading.Tasks;
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
        private readonly s18827DbContext _context;
        public OrdersController(IDbService service, s18827DbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        [HttpGet("{custName}")]
        public async Task<IActionResult> ListOrders(string custName)
        {
            try
            {
                var ordersList = await _service.ListOrders(custName);
                return Ok(ordersList);
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

    }
}