namespace Microsoft.eShopWeb.Web.Tests.Controllers
{
    using Microsoft.eShopWeb.Web;
    using Microsoft.eShopWeb.Web.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestClass]
    public class ApiCatalogControllerList
    {
        readonly HttpClient _client;

        public ApiCatalogControllerList()
        {
            CustomWebApplicationFactory<Startup> factory = new CustomWebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task ReturnsFirst10CatalogItems()
        {
            var response = await _client.GetAsync("/api/catalog/list");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CatalogIndexViewModel>(stringResponse);

            Assert.AreEqual(10, model.CatalogItems.Count());
        }

        [TestMethod]
        public async Task ReturnsLast2CatalogItemsGivenPageIndex1()
        {
            var response = await _client.GetAsync("/api/catalog/list?page=1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CatalogIndexViewModel>(stringResponse);

            Assert.AreEqual(2, model.CatalogItems.Count());
        }
    }
}
