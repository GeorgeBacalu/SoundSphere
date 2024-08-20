using Microsoft.Data.SqlClient;
using SoundSphere.Database.Attributes;
using SoundSphere.Database.Dtos.Request.Models;
using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Request.Pagination
{
    public record SongPaginationRequest(
        Dictionary<SongSortCriterion, SortOrder>? SortCriteria,
        
        List<SongSearchCriterion>? SearchCriteria,
        
        [StringLength(75, ErrorMessage = "Title can't be longer than 75 characters")]
        string? Title,

        Genre? Genre,

        [DateRange]
        DateRange? DateRange,

        [DurationRange]
        DurationRange? DurationRange,

        [StringLength(75, ErrorMessage = "Album title can't be longer than 75 characters")]
        string? AlbumTitle,

        [StringLength(75, ErrorMessage = "Artist name can't be longer than 75 characters")]
        string? ArtistName) : PaginationRequest;

    public enum SongSortCriterion { InvalidSortCriterion, ByTitle, ByGenre, ByReleaseDate, ByDuration }

    public enum SongSearchCriterion { InvalidSerchCriterion, ByTitle, ByGenre, ByReleaseDateRange, ByDurationRange, ByAlbumTitle, ByArtistName }
}