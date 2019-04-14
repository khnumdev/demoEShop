namespace Web.Tests.UI
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory("UI")]
    public class IndexTests
    {
        [TestMethod]
        [TestCategory("Chrome")]
        public void Title_should_be_catalog()
        {
            var driver = TestHelper.GetDriver();

            driver.Navigate().GoToUrl(TestHelper.AppUrl + "/");
           
            Assert.IsTrue(driver.Title.Contains("Catalog"), "Verified title of the page");

            driver.Quit();
        }
    }
}
