using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SoundSphere.Core.Services;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Context;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Middlewares;
using System.Reflection;

public class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.MigrationsAssembly("SoundSphere.Api")));
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(SoundSphere.Core.Mappings.AutoMapperProfile).Assembly);
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();

        builder.Services.AddScoped<IAlbumRepository, AlbumRepository>()
                        .AddScoped<IArtistRepository, ArtistRepository>()
                        .AddScoped<IFeedbackRepository, FeedbackRepository>()
                        .AddScoped<INotificationRepository, NotificationRepository>()
                        .AddScoped<IPlaylistRepository, PlaylistRepository>()
                        .AddScoped<ISongRepository, SongRepository>()
                        .AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<IAlbumService, AlbumService>()
                        .AddScoped<IArtistService, ArtistService>()
                        .AddScoped<IFeedbackService, FeedbackService>()
                        .AddScoped<INotificationService, NotificationService>()
                        .AddScoped<IPlaylistService, PlaylistService>()
                        .AddScoped<ISongService, SongService>()
                        .AddScoped<IUserService, UserService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "SoundSphere API", Description = "This is a sample REST API documentation for a music streaming service.", Version = "1.0" });
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            ExecuteSql(app.Services, Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Scripts", $"{Assembly.GetExecutingAssembly().GetName().Name}.sql"));
        }
        app.UseHttpsRedirection();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    static void ExecuteSql(IServiceProvider services, string path)
    {
        var context = services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        if (!File.Exists(path)) throw new FileNotFoundException("SQL file not found", path);
        context.Database.ExecuteSqlRaw(File.ReadAllText(path));
    }
}