namespace Microsoft.eShopWeb.ApplicationCore.Tests.Specifications
{
    using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
    using Microsoft.eShopWeb.ApplicationCore.Specifications;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class BasketWithItems
    {
        private int _testBasketId = 123;

        [TestMethod]
        public void MatchesBasketWithGivenId()
        {
            var spec = new BasketWithItemsSpecification(_testBasketId);

            var result = GetTestBasketCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.IsNotNull(result);
            Assert.AreEqual(_testBasketId, result.Id);

        }

        [TestMethod]
        public void MatchesNoBasketsIfIdNotPresent()
        {
            int badId = -1;
            var spec = new BasketWithItemsSpecification(badId);

            Assert.IsFalse(GetTestBasketCollection()
                .AsQueryable()
                .Any(spec.Criteria));
        }

        public List<Basket> GetTestBasketCollection()
        {
            return new List<Basket>()
            {
                new Basket() { Id = 1 },
                new Basket() { Id = 2 },
                new Basket() { Id = _testBasketId }
            };
        }
    }
}
