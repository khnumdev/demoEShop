namespace Microsoft.eShopWeb.Web.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.eShopWeb.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestClass]
    public class OrderIndexOnGet
    {
        readonly HttpClient _client;

        public OrderIndexOnGet()
        {
            CustomWebApplicationFactory<Startup> factory = new CustomWebApplicationFactory<Startup>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [TestMethod]
        public async Task ReturnsRedirectGivenAnonymousUser()
        {
            var response = await _client.GetAsync("/order/my-orders");
            var redirectLocation = response.Headers.Location.OriginalString;

            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
            Assert.IsTrue(redirectLocation.Contains("/Account/Login"));
        }
    }
}
