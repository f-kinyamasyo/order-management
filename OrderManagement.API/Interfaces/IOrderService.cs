using OrderManagement.API.Dtos;
using OrderManagement.API.Models;

namespace OrderManagement.API.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);
        bool UpdateOrderStatus(Guid id, string newStatus);
        OrderAnalyticsDto GetOrderAnalytics();
    }
}
