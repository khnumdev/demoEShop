namespace Microsoft.eShopWeb.ApplicationCore.Tests.Specifications
{
    using Microsoft.eShopWeb.ApplicationCore.Entities;
    using Microsoft.eShopWeb.ApplicationCore.Specifications;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CatalogFilterSpecificationFilter
    {
        [DataRow(null, null, 5)]
        [DataRow(1, null, 3)]
        [DataRow(2, null, 2)]
        [DataRow(null, 1, 2)]
        [DataRow(null, 3, 1)]
        [DataRow(1, 3, 1)]
        [DataRow(2, 3, 0)]
        [DataTestMethod]
        public void MatchesExpectedNumberOfItems(int? brandId, int? typeId, int expectedCount)
        {
            var spec = new CatalogFilterSpecification(brandId, typeId);

            var result = GetTestItemCollection()
                .AsQueryable()
                .Where(spec.Criteria);

            Assert.AreEqual(expectedCount, result.Count());
        }

        public List<CatalogItem> GetTestItemCollection()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem() { Id = 1, CatalogBrandId = 1, CatalogTypeId= 1 },
                new CatalogItem() { Id = 2, CatalogBrandId = 1, CatalogTypeId= 2 },
                new CatalogItem() { Id = 3, CatalogBrandId = 1, CatalogTypeId= 3 },
                new CatalogItem() { Id = 4, CatalogBrandId = 2, CatalogTypeId= 1 },
                new CatalogItem() { Id = 5, CatalogBrandId = 2, CatalogTypeId= 2 },
            };
        }
    }
}
