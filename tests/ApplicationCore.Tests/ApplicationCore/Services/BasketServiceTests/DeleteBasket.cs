﻿namespace Microsoft.eShopWeb.ApplicationCore.Tests.Services.BasketServiceTests
{
    using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
    using Microsoft.eShopWeb.ApplicationCore.Interfaces;
    using Microsoft.eShopWeb.ApplicationCore.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteBasket
    {
        private Mock<IAsyncRepository<Basket>> _mockBasketRepo;
        private Mock<IAsyncRepository<BasketItem>> _mockBasketItemRepo;

        public DeleteBasket()
        {
            _mockBasketRepo = new Mock<IAsyncRepository<Basket>>();
            _mockBasketItemRepo = new Mock<IAsyncRepository<BasketItem>>();
        }

        [TestMethod]
        public async Task Should_InvokeBasketRepoOnceAndBasketItemRepoTwice_Given_TwoItemsInBasket()
        {
            var basket = new Basket();
            basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<int>());
            basket.AddItem(2, It.IsAny<decimal>(), It.IsAny<int>());
            _mockBasketRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(basket);
            var basketService = new BasketService(_mockBasketRepo.Object, null, null, _mockBasketItemRepo.Object);

            await basketService.DeleteBasketAsync(It.IsAny<int>());

            _mockBasketRepo.Verify(x => x.DeleteAsync(It.IsAny<Basket>()), Times.Once);
            _mockBasketItemRepo.Verify(x => x.DeleteAsync(It.IsAny<BasketItem>()), Times.Exactly(2));
        }
    }
}
