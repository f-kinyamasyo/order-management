using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Interfaces;
using OrderManagement.API.Models;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Gets analytics for all orders.
        /// Returns average order value and average fulfillment time.
        /// </summary>
        /// <returns>Analytics metrics</returns>
        [HttpGet("analytics")]
        public IActionResult GetAnalytics()
        {
            var result = _orderService.GetOrderAnalytics();
            return Ok(result);
        }

        /// <summary>
        /// Updates the status of an order.
        /// Valid transitions are enforced (Pending → Processing → Shipped → Delivered).
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <param name="request">Request body with new status</param>
        /// <returns>No content if successful</returns>
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
        {
            var result = _orderService.UpdateOrderStatus(id, request.NewStatus);
            if (!result)
                return BadRequest("Invalid status transition.");

            return NoContent();
        }

        /// <summary>
        /// Creates a new order with automatic discount calculation based on customer history.
        /// </summary>
        /// <param name="order">Order object</param>
        /// <returns>Created order</returns>
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            var createdOrder = _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(CreateOrder), new { id = createdOrder.Id }, createdOrder);
        }
    }
}
