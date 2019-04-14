namespace Microsoft.eShopWeb.Infrastructure.Tests.Repositories.OrderRepositoryTests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.eShopWeb.Infrastructure.Data;
    using Microsoft.eShopWeb.Tests.Common.Builders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class GetById
    {
        private readonly CatalogContext _catalogContext;
        private readonly OrderRepository _orderRepository;
        private OrderBuilder OrderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;
        public GetById(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "TestCatalog")
                .Options;
            _catalogContext = new CatalogContext(dbOptions);
            _orderRepository = new OrderRepository(_catalogContext);
        }

        [TestMethod]
        public async Task GetsExistingOrder()
        {
            var existingOrder = OrderBuilder.WithDefaultValues();
            _catalogContext.Orders.Add(existingOrder);
            _catalogContext.SaveChanges();
            int orderId = existingOrder.Id;
            _output.WriteLine($"OrderId: {orderId}");

            var orderFromRepo = await _orderRepository.GetByIdAsync(orderId);
            Assert.AreEqual(OrderBuilder.TestBuyerId, orderFromRepo.BuyerId);

            // Note: Using InMemoryDatabase OrderItems is available. Will be null if using SQL DB.
            var firstItem = orderFromRepo.OrderItems.FirstOrDefault();
            Assert.AreEqual(OrderBuilder.TestUnits, firstItem.Units);
        }
    }
}
