using Microsoft.Data.SqlClient;
using SoundSphere.Database.Attributes;
using SoundSphere.Database.Dtos.Request.Models;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Request.Pagination
{
    public record PlaylistPaginationRequest(
        Dictionary<PlaylistSortCriterion, SortOrder>? SortCriteria,
        
        List<PlaylistSearchCriterion>? SearchCriteria,

        [StringLength(75, ErrorMessage = "Title can't be longer than 75 characters")]
        string? Title,

        [StringLength(75, ErrorMessage = "User name can't be longer than 75 characters")]
        string? UserName,

        [StringLength(75, ErrorMessage = "Song title can't be longer than 75 characters")]
        string? SongTitle,
        
        [DateRange]
        DateTimeRange? DateRange) : PaginationRequest;

    public enum PlaylistSortCriterion { InvalidSortCriterion, ByTitle, ByUserName, ByCreateDate }

    public enum PlaylistSearchCriterion { InvalidSerchCriterion, ByTitle, ByUserName, BySongTitle, ByCreateDateRange }
}