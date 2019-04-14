namespace Web.Tests.UI
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestInitialization
    {
        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            TestHelper.AppUrl = testContext.Properties["AppUrl"].ToString();
        }
    }
}
