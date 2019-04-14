namespace Microsoft.eShopWeb.Web.Tests.Controllers
{
    using Microsoft.eShopWeb.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestClass]
    public class CatalogControllerIndex
    {
        readonly HttpClient _client;

        public CatalogControllerIndex()
        {
            CustomWebApplicationFactory<Startup> factory = new CustomWebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task ReturnsHomePageWithProductListing()
        {
            // Arrange & Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.IsTrue(stringResponse.Contains(".NET Bot Black Sweatshirt"));
        }
    }
}
