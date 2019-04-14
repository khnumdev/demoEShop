namespace Microsoft.eShopWeb.Web.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.eShopWeb.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory("Functional")]
    public class AccountControllerSignIn
    {
        readonly HttpClient _client;

        public AccountControllerSignIn()
        {
            CustomWebApplicationFactory<Startup> factory = new CustomWebApplicationFactory<Startup>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [TestMethod]
        public async Task ReturnsSignInScreenOnGet()
        {
            var response = await _client.GetAsync("/identity/account/login");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(stringResponse.Contains("demouser@microsoft.com"));
        }

        [TestMethod]
        public void RegexMatchesValidRequestVerificationToken()
        {
            // TODO: Move to a unit test
            // TODO: Move regex to a constant in test project
            var input = @"<input name=""__RequestVerificationToken"" type=""hidden"" value=""CfDJ8Obhlq65OzlDkoBvsSX0tgxFUkIZ_qDDSt49D_StnYwphIyXO4zxfjopCWsygfOkngsL6P0tPmS2HTB1oYW-p_JzE0_MCFb7tF9Ol_qoOg_IC_yTjBNChF0qRgoZPmKYOIJigg7e2rsBsmMZDTdbnGo"" /><input name=""RememberMe"" type=""hidden"" value=""false"" /></form>";
            string regexpression = @"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""";
            var regex = new Regex(regexpression);
            var match = regex.Match(input);
            var group = match.Groups.LastOrDefault();
            Assert.IsNotNull(group);
            Assert.IsTrue(group.Value.Length > 50);
        }

        [TestMethod]
        public async Task ReturnsFormWithRequestVerificationToken()
        {
            var response = await _client.GetAsync("/identity/account/login");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            string token = GetRequestVerificationToken(stringResponse);
            Assert.IsTrue(token.Length > 50);
        }

        [TestMethod]
        public async Task ReturnsSuccessfulSignInOnPostWithValidCredentials()
        {
            var getResponse = await _client.GetAsync("/identity/account/login");
            getResponse.EnsureSuccessStatusCode();
            var stringResponse1 = await getResponse.Content.ReadAsStringAsync();
            string token = GetRequestVerificationToken(stringResponse1);

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Email", "demouser@microsoft.com"));
            keyValues.Add(new KeyValuePair<string, string>("Password", "Pass@word1"));

            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));
            var formContent = new FormUrlEncodedContent(keyValues);

            var postResponse = await _client.PostAsync("/account/sign-in", formContent);
            Assert.AreEqual(HttpStatusCode.Redirect, postResponse.StatusCode);
            Assert.AreEqual(new System.Uri("/", UriKind.Relative), postResponse.Headers.Location);
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
