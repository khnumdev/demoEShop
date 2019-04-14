namespace Microsoft.eShopWeb.ApplicationCore.Tests.Services.BasketServiceTests
{
    using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
    using Microsoft.eShopWeb.ApplicationCore.Exceptions;
    using Microsoft.eShopWeb.ApplicationCore.Interfaces;
    using Microsoft.eShopWeb.ApplicationCore.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SetQuantities
    {
        private int _invalidId = -1;
        private Mock<IAsyncRepository<Basket>> _mockBasketRepo;

        public SetQuantities()
        {
            _mockBasketRepo = new Mock<IAsyncRepository<Basket>>();
        }

        [TestMethod]
        public async void ThrowsGivenInvalidBasketId()
        {
            var basketService = new BasketService(_mockBasketRepo.Object, null, null, null);

            await Assert.ThrowsExceptionAsync<BasketNotFoundException>(async () =>
                await basketService.SetQuantities(_invalidId, new System.Collections.Generic.Dictionary<string, int>()));
        }

        [TestMethod]
        public async void ThrowsGivenNullQuantities()
        {
            var basketService = new BasketService(null, null, null, null);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
                await basketService.SetQuantities(123, null));
        }

    }
}
