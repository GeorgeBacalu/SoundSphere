using Microsoft.Data.SqlClient;
using SoundSphere.Database.Attributes;
using SoundSphere.Database.Dtos.Request.Models;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Request.Pagination
{
    public record AlbumPaginationRequest(
        Dictionary<AlbumSortCriterion, SortOrder>? SortCriteria,

        List<AlbumSearchCriterion>? SearchCriteria,

        [StringLength(75, ErrorMessage = "Title can't be longer than 75 characters")]
        string? Title,

        [DateRange]
        DateRange? DateRange) : PaginationRequest;

    public enum AlbumSortCriterion { InvalidSortCriterion, ByTitle, ByReleaseDate }

    public enum AlbumSearchCriterion { InvalidSerchCriterion, ByTitle, ByReleaseDateRange }
}