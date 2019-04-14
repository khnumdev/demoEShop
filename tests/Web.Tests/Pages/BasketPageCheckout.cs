namespace Microsoft.eShopWeb.Web.Tests.Pages
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.eShopWeb.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory("Functional")]
    public class BasketPageCheckout
    {
        readonly HttpClient _client;

        public BasketPageCheckout()
        {
            CustomWebApplicationFactory<Startup> factory = new CustomWebApplicationFactory<Startup>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [TestMethod]
        public async Task RedirectsToLoginIfNotAuthenticated()
        {
            // Arrange & Act

            // Load Home Page
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse1 = await response.Content.ReadAsStringAsync();

            string token = GetRequestVerificationToken(stringResponse1);

            // Add Item to Cart
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("id", "2"));
            keyValues.Add(new KeyValuePair<string, string>("name", "shirt"));

            keyValues.Add(new KeyValuePair<string, string>("price", "19.49"));
            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));

            var formContent = new FormUrlEncodedContent(keyValues);

            var postResponse = await _client.PostAsync("/basket/index", formContent);
            postResponse.EnsureSuccessStatusCode();
            var stringResponse = await postResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.IsTrue(stringResponse.Contains(".NET Black &amp; White Mug"));

            keyValues.Clear();
            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));

            formContent = new FormUrlEncodedContent(keyValues);
            var postResponse2 = await _client.PostAsync("/Basket/Checkout", formContent);
            Assert.IsTrue(postResponse2.RequestMessage.RequestUri.ToString().Contains("/Identity/Account/Login"));
        }

        private string GetRequestVerificationToken(string input)
        {
            string regexpression = @"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""";
            var regex = new Regex(regexpression);
            var match = regex.Match(input);
            return match.Groups.LastOrDefault().Value;
        }
    }
}
