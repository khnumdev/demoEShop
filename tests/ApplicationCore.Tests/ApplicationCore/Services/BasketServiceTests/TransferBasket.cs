namespace Microsoft.eShopWeb.ApplicationCore.Tests.Services.BasketServiceTests
{
    using Microsoft.eShopWeb.ApplicationCore.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TransferBasket
    {
        [TestMethod]
        public async void ThrowsGivenNullAnonymousId()
        {
            var basketService = new BasketService(null, null, null, null);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await basketService.TransferBasketAsync(null, "steve"));
        }

        [TestMethod]
        public async void ThrowsGivenNullUserId()
        {
            var basketService = new BasketService(null, null, null, null);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await basketService.TransferBasketAsync("abcdefg", null));
        }
    }
}
