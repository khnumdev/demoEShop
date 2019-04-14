namespace Microsoft.eShopWeb.Infrastructure.Tests.Repositories.OrderRepositoryTests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
    using Microsoft.eShopWeb.Infrastructure.Data;
    using Microsoft.eShopWeb.Tests.Common.Builders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class GetByIdWithItemsAsync_Should
    {
        private CatalogContext _catalogContext;
        private OrderRepository _orderRepository;
        private OrderBuilder OrderBuilder { get; } = new OrderBuilder();

        [TestInitialize]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "TestCatalog")
                .Options;
            _catalogContext = new CatalogContext(dbOptions);
            _orderRepository = new OrderRepository(_catalogContext);
        }

        [TestMethod]
        public async Task GetOrderAndItemsByOrderId_When_MultipleOrdersPresent()
        {
            //Arrange
            var itemOneUnitPrice = 5.50m;
            var itemOneUnits = 2;
            var itemTwoUnitPrice = 7.50m;
            var itemTwoUnits = 5;

            var firstOrder = OrderBuilder.WithDefaultValues();
            _catalogContext.Orders.Add(firstOrder);
            int firstOrderId = firstOrder.Id;

            var secondOrderItems = new List<OrderItem>();
            secondOrderItems.Add(new OrderItem(OrderBuilder.TestCatalogItemOrdered, itemOneUnitPrice, itemOneUnits));
            secondOrderItems.Add(new OrderItem(OrderBuilder.TestCatalogItemOrdered, itemTwoUnitPrice, itemTwoUnits));
            var secondOrder = OrderBuilder.WithItems(secondOrderItems);
            _catalogContext.Orders.Add(secondOrder);
            int secondOrderId = secondOrder.Id;

            _catalogContext.SaveChanges();

            //Act
            var orderFromRepo = await _orderRepository.GetByIdWithItemsAsync(secondOrderId);

            //Assert
            Assert.AreEqual(secondOrderId, orderFromRepo.Id);
            Assert.AreEqual(secondOrder.OrderItems.Count, orderFromRepo.OrderItems.Count);
            Assert.AreEqual(1, orderFromRepo.OrderItems.Count(x => x.UnitPrice == itemOneUnitPrice));
            Assert.AreEqual(1, orderFromRepo.OrderItems.Count(x => x.UnitPrice == itemTwoUnitPrice));
            Assert.AreEqual(itemOneUnits, orderFromRepo.OrderItems.SingleOrDefault(x => x.UnitPrice == itemOneUnitPrice).Units);
            Assert.AreEqual(itemTwoUnits, orderFromRepo.OrderItems.SingleOrDefault(x => x.UnitPrice == itemTwoUnitPrice).Units);
        }
    }
}
