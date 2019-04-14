namespace Microsoft.eShopWeb.ApplicationCore.Tests.Entities.BasketTests
{
    using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BasketAddItem
    {
        private int _testCatalogItemId = 123;
        private decimal _testUnitPrice = 1.23m;
        private int _testQuantity = 2;

        [TestMethod]
        public void AddsBasketItemIfNotPresent()
        {
            var basket = new Basket();
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.AreEqual(_testCatalogItemId, firstItem.CatalogItemId);
            Assert.AreEqual(_testUnitPrice, firstItem.UnitPrice);
            Assert.AreEqual(_testQuantity, firstItem.Quantity);
        }

        [TestMethod]
        public void IncrementsQuantityOfItemIfPresent()
        {
            var basket = new Basket();
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.AreEqual(_testQuantity * 2, firstItem.Quantity);
        }

        [TestMethod]
        public void KeepsOriginalUnitPriceIfMoreItemsAdded()
        {
            var basket = new Basket();
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testCatalogItemId, _testUnitPrice * 2, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.AreEqual(_testUnitPrice, firstItem.UnitPrice);
        }

        [TestMethod]
        public void DefaultsToQuantityOfOne()
        {
            var basket = new Basket();
            basket.AddItem(_testCatalogItemId, _testUnitPrice);

            var firstItem = basket.Items.Single();
            Assert.AreEqual(1, firstItem.Quantity);
        }
    }
}
