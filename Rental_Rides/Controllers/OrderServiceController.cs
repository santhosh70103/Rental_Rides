using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rental_Rides.IRepo;

namespace Rental_Rides.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderServiceController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderServiceController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("customer/{customerMail}")]
        public async Task<ActionResult> GetOrdersByCustomerId(string customerMail)
        {
            var groupedOrders = await _orderService.GetOrdersByEmailAsync(customerMail);

            if (groupedOrders == null || !groupedOrders.Any())
            {
                return NotFound($"No orders found for Customer ID {customerMail}");
            }

            return Ok(groupedOrders);
        }
    }

}
