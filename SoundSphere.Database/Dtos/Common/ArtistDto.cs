﻿using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Common
{
    public class ArtistDto : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(75, ErrorMessage = "Name can't be longer than 75 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Invalid URL format")]
        public string ImageUrl { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Bio can't be longer than 500 characters")]
        public string? Bio { get; set; }

        [MaxLength(15, ErrorMessage = "There can't be more than 15 similar artists")]
        public List<Guid> SimilarArtistsIds { get; set; } = new()!;
    }
}