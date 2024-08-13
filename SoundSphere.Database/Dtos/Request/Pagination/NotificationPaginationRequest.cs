using Microsoft.Data.SqlClient;
using SoundSphere.Database.Attributes;
using SoundSphere.Database.Dtos.Request.Models;
using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Request.Pagination
{
    public record NotificationPaginationRequest(
        Dictionary<NotificationSortCriterion, SortOrder>? SortCriteria,
        
        List<NotificationSearchCriterion>? SearchCriteria,

        NotificationType? Type,

        [StringLength(500, ErrorMessage = "Message can't be longer than 500 characters")]
        string? Message,

        [StringLength(75, ErrorMessage = "Sender name can't be longer than 75 characters")]
        string? SenderName,
        
        bool? IsRead,

        [DateRange]
        DateTimeRange? DateRange): PaginationRequest;

    public enum NotificationSortCriterion { InvalidSortCriterion, ByType, ByMessage, BySenderName, ByCreateDate }

    public enum NotificationSearchCriterion { InvalidSerchCriterion, ByType, ByMessage, BySenderName, ByIsRead, ByCreateDateRange }
}