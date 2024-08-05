using Microsoft.EntityFrameworkCore;
using SoundSphere.Core.Services;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Context;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.MigrationsAssembly("SoundSphere.Api")));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(SoundSphere.Core.Mappings.AutoMapperProfile).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();