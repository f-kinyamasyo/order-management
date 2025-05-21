using OrderManagement.API.Models;

namespace OrderManagement.API.Interfaces
{
    public interface IDiscountService
    {
        decimal ApplyDiscount(Order order, Customer customer);
    }
}
