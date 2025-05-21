using OrderManagement.API.Models;
using OrderManagement.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests
{
    public class DiscountServiceTests
    {
        [Fact]
        public void ApplyDiscount_VIPCustomerAndFrequentOrders_ReturnsExpected()
        {
            var service = new DiscountService();
            var customer = new Customer { Segment = "VIP", OrderCount = 12 };
            var order = new Order { TotalAmount = 200 };

            var result = service.ApplyDiscount(order, customer);
            Assert.Equal(170, result);
        }
    }
}
