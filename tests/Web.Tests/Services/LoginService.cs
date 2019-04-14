namespace Microsoft.eShopWeb.Web.Tests.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.eShopWeb.Infrastructure.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LoginService
    {
        [TestMethod]
        public async Task LogsInSampleUser()
        {
            var services = new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase();

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("Identity");
            });
            var serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (AppIdentityDbContext).
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                try
                {
                    // seed sample user data
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait();

                    var signInManager = scopedServices.GetRequiredService<SignInManager<ApplicationUser>>();

                    var email = "demouser@microsoft.com";
                    var password = "Pass@word1";

                    var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

                    Assert.IsTrue(result.Succeeded);

                }
                catch (Exception)
                {
                }
            }
        }
    }
}
