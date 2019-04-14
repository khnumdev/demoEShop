namespace Microsoft.eShopWeb.ApplicationCore.Tests.Entities.OrderTests
{
    using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
    using Microsoft.eShopWeb.Tests.Common.Builders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class OrderTotal
    {
        private decimal _testUnitPrice = 42m;

        [TestMethod]
        public void IsZeroForNewOrder()
        {
            var order = new OrderBuilder().WithNoItems();

            Assert.AreEqual(0, order.Total());
        }

        [TestMethod]
        public void IsCorrectGiven1Item()
        {
            var builder = new OrderBuilder();
            var items = new List<OrderItem>
            {
                new OrderItem(builder.TestCatalogItemOrdered, _testUnitPrice, 1)
            };
            var order = new OrderBuilder().WithItems(items);
            Assert.AreEqual(_testUnitPrice, order.Total());
        }

        [TestMethod]
        public void IsCorrectGiven3Items()
        {
            var builder = new OrderBuilder();
            var order = builder.WithDefaultValues();

            Assert.AreEqual(builder.TestUnitPrice * builder.TestUnits, order.Total());
        }
    }
}
