using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SoundSphere.Core.Services;
using SoundSphere.Core.Services.Interfaces;

namespace SoundSphere.Test.Integration
{
    public class CustomWebAppFactory : WebApplicationFactory<Program>
    {
        private readonly DbFixture _dbFixture;

        public CustomWebAppFactory(DbFixture dbFixture) => _dbFixture = dbFixture;

        protected override void ConfigureWebHost(IWebHostBuilder builder) => builder.ConfigureServices(services =>
        {
            services.AddScoped(_ => _dbFixture.CreateContext());
            services.AddScoped<IUserService, UserService>();
        });
    }
}