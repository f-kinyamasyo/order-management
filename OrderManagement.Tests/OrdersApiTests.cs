using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests
{
    [Collection("WebAppFactory Collection")]
    public class OrdersApiTests
    {
        private readonly HttpClient _client;

        public OrdersApiTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAnalytics_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/api/orders/analytics");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("averageOrderValue", content);
        }
    }
}
