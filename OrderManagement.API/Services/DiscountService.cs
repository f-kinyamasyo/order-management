using OrderManagement.API.Interfaces;
using OrderManagement.API.Models;

namespace OrderManagement.API.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal ApplyDiscount(Order order, Customer customer)
        {
            decimal discount = 0;
            if (customer.Segment == "VIP")
                discount += 0.10m;
            if (customer.OrderCount > 10)
                discount += 0.05m;

            return  order.TotalAmount * (1 - discount);
        }
    }
}
