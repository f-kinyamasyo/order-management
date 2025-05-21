using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests
{
    [CollectionDefinition("WebAppFactory Collection")]
    public class WebAppFactoryCollection : ICollectionFixture<CustomWebApplicationFactory>
    {
    }
}
