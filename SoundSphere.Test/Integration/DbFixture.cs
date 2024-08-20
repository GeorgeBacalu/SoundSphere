using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoundSphere.Database.Context;

namespace SoundSphere.Test.Integration
{
    public class DbFixture
    {
        private readonly IConfiguration _configuration;

        public DbFixture() => _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.Test.json").Build();

        public AppDbContext CreateContext() => new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options);

        public async Task TrackAndAddAsync<T>(AppDbContext context, List<T> entities) where T : class
        {
            entities.ForEach(entity =>
            {
                if (context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(property => property.Name).SingleOrDefault() is not string idProperty) return;
                if (!context.ChangeTracker.Entries<T>().Any(entry => entry.Property(idProperty).CurrentValue?.Equals(context.Entry(entity).Property(idProperty).CurrentValue) == true))
                {
                    context.Entry(entity).State = EntityState.Unchanged;
                    context.Set<T>().Add(entity);
                }
            });
            await context.SaveChangesAsync();
        }
    }
}