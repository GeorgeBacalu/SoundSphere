using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Common
{
    public class PlaylistDto : BaseEntity
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [MaxLength(100, ErrorMessage = "There can't be more than 100 songs in a playlist")]
        public List<Guid> SongsIds { get; set; } = new()!;
    }
}