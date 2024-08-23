namespace SoundSphere.Infrastructure.Config.Models
{
    public record JwtSettings(string Issuer, string Audience, string Secret, int TokenValidMinutes);
}