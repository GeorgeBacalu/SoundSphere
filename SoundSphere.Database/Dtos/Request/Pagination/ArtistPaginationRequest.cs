using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Request.Pagination
{
    public record ArtistPaginationRequest(
        Dictionary<ArtistSortCriterion, SortOrder>? SortCriteria,

        List<ArtistSearchCriterion>? SearchCriteria,

        [StringLength(75, ErrorMessage = "Name can't be longer than 75 characters")]
        string? Name) : PaginationRequest;

    public enum ArtistSortCriterion { InvalidSortCriterion, ByName }

    public enum ArtistSearchCriterion { InvalidSerchCriterion, ByName }
}