using Microsoft.Data.SqlClient;
using SoundSphere.Database.Attributes;
using SoundSphere.Database.Dtos.Request.Models;
using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Request.Pagination
{
    public record FeedbackPaginationRequest(
        Dictionary<FeedbackSortCriterion, SortOrder>? SortCriteria,
        
        List<FeedbackSearchCriterion>? SearchCriteria,

        FeedbackType? Type,

        [StringLength(500, ErrorMessage = "Message can't be longer than 500 characters")]
        string? Message,

        [StringLength(75, ErrorMessage = "User name can't be longer than 75 characters")]
        string? UserName,
        
        [DateRange]
        DateTimeRange? DateRange) : PaginationRequest;

    public enum FeedbackSortCriterion { InvalidSortCriterion, ByType, ByMessage, ByUserName, ByCreateDate }

    public enum FeedbackSearchCriterion { InvalidSerchCriterion, ByType, ByMessage, ByUserName, ByCreateDateRange }
}