namespace Microsoft.eShopWeb.Infrastructure.Tests.Repositories.BasketItemRepositoryTests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
    using Microsoft.eShopWeb.Infrastructure.Data;
    using Microsoft.eShopWeb.Tests.Common.Builders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory("Integration")]
    public class DeleteAsync_Should
    {
        private CatalogContext _catalogContext;
        private EfRepository<Basket> _basketRepository;
        private EfRepository<BasketItem> _basketItemRepository;
        private BasketBuilder BasketBuilder { get; } = new BasketBuilder();

        [TestInitialize]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "TestCatalog")
                .Options;
            _catalogContext = new CatalogContext(dbOptions);
            _basketRepository = new EfRepository<Basket>(_catalogContext);
            _basketItemRepository = new EfRepository<BasketItem>(_catalogContext);
        }

        [TestMethod]
        public async Task DeleteItemFromBasket()
        {
            var existingBasket = BasketBuilder.WithOneBasketItem();
            _catalogContext.Add(existingBasket);
            _catalogContext.SaveChanges();
            
            await _basketItemRepository.DeleteAsync(existingBasket.Items.FirstOrDefault());
            _catalogContext.SaveChanges();

            var basketFromDB = await _basketRepository.GetByIdAsync(BasketBuilder.BasketId);

            Assert.AreEqual(0, basketFromDB.Items.Count);
        }
    }
}
