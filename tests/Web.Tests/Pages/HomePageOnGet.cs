namespace Microsoft.eShopWeb.Web.Tests.Pages
{
    using Microsoft.eShopWeb.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestClass]
    public class HomePageOnGet
    {
        readonly HttpClient _client;

        public HomePageOnGet()
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
