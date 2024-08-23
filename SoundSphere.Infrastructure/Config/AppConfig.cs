using Microsoft.Extensions.Configuration;
using SoundSphere.Infrastructure.Config.Models;
using SoundSphere.Infrastructure.Exceptions;

namespace SoundSphere.Infrastructure.Config
{
    public static class AppConfig
    {
        public static ConnectionStringsSettings ConnectionStringsSettings { get; private set; } = null!;
        public static JwtSettings JwtSettings { get; private set; } = null!;

        public static void Init(IConfiguration configuration)
        {
            IConfigurationSection connectionStringsConfigSection = configuration.GetSection("ConnectionStrings");
            ConnectionStringsSettings = connectionStringsConfigSection.Get<ConnectionStringsSettings>() ?? throw new ResourceNotFoundException("ConnectionStrings settings not found");
            IConfigurationSection jwtConfigSection = configuration.GetSection("JWT");
            JwtSettings = jwtConfigSection.Get<JwtSettings>() ?? throw new ResourceNotFoundException("JWT settings not found");
        }
    }
}