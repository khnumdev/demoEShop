namespace Web.Tests.UI
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [ExcludeFromCodeCoverage]
    static class TestHelper
    {
        public static IWebDriver GetDriver()
        {
            var rootDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directoryName = Path.GetDirectoryName(rootDir);

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    {
                        // https://www.dotnetcatch.com/2017/06/19/selenium-chromedriver-webdriver-error-with-tfs/
                        var options = new OpenQA.Selenium.Chrome.ChromeOptions { };
                        // https://github.com/SeleniumHQ/selenium/issues/5457#issuecomment-443355179
                        options.AddArgument("no-sandbox");
                        return new ChromeDriver(directoryName, options);
                    }
                case "Firefox":
                    return new FirefoxDriver(directoryName);
                case "IE":
                    return new InternetExplorerDriver(directoryName);
                default:
                    return new ChromeDriver(directoryName);
            }
        }
    }
}
