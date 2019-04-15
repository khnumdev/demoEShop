namespace Web.Tests.UI
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    static class TestHelper
    {
        public static string AppUrl { get; set; }

        public static IWebDriver GetDriver()
        {
            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    return new ChromeDriver();
                case "Firefox":
                    return new FirefoxDriver();
                case "IE":
                    return new InternetExplorerDriver();
                default:
                    return new ChromeDriver();
            }
        }
    }
}
