using OrderManagement.API.Dtos;
using OrderManagement.API.Interfaces;
using OrderManagement.API.Models;

namespace OrderManagement.API.Services
{
    public class OrderService: IOrderService
    {
        private readonly List<Order> _orders = new();

        public Order CreateOrder(Order order)
        {
            // Apply discounts based on customer segment and order history
            var discount = CalculateDiscount(order.CustomerId);
            order.TotalAmount -= (decimal)order.TotalAmount * discount;
            order.Status = "Pending";
            order.CreatedAt = DateTime.UtcNow;
            order.Id = Guid.NewGuid();
            _orders.Add(order);
            return order;
        }

        public bool UpdateOrderStatus(Guid id, string newStatus)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return false;

            // Define valid transitions
            var validTransitions = new Dictionary<string, string[]>
            {
                ["Pending"] = new[] { "Processing" },
                ["Processing"] = new[] { "Shipped" },
                ["Shipped"] = new[] { "Delivered" },
            };

            if (validTransitions.ContainsKey(order.Status) &&
                validTransitions[order.Status].Contains(newStatus))
            {
                order.Status = newStatus;
                if (newStatus == "Delivered")
                    order.DeliveredAt = DateTime.UtcNow;
                return true;
            }
            return false;
        }

        public OrderAnalyticsDto GetOrderAnalytics()
        {
            if (_orders.Count == 0)
                return new OrderAnalyticsDto { AverageOrderValue = 0, AverageFulfillmentHours = 0 };

            var avgValue = _orders.Average(o => o.TotalAmount);
            var fulfilled = _orders.Where(o => o.DeliveredAt.HasValue).ToList();

            var avgFulfillment = fulfilled.Count > 0
                ? fulfilled.Average(o => (o.DeliveredAt.Value - o.CreatedAt).TotalHours)
                : 0;

            return new OrderAnalyticsDto
            {
                AverageOrderValue = Math.Round(avgValue, 2),
                AverageFulfillmentHours = (decimal) Math.Round(avgFulfillment, 2)
            };
        }

        private decimal CalculateDiscount(Guid customerId)
        {
            // Simulate history check: VIPs get 10%, >10 orders get extra 5%
            var customerOrders = _orders.Count(o => o.CustomerId == customerId);
            decimal discount = 0;

            var isVip = customerId.ToString().StartsWith("vip");
            if (isVip) discount += 0.10m; 
            if (customerOrders > 10) discount += 0.05m; 

            return discount;
        }

       
    }
}
