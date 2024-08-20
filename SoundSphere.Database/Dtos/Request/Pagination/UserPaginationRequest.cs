using Microsoft.Data.SqlClient;
using SoundSphere.Database.Attributes;
using SoundSphere.Database.Dtos.Request.Models;
using SoundSphere.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Database.Dtos.Request.Pagination
{
    public record UserPaginationRequest(
        Dictionary<UserSortCriterion, SortOrder>? SortCriteria,
        
        List<UserSeachCriterion>? SeachCriteria,

        [StringLength(75, ErrorMessage = "Name can't be longer than 75 characters")]
        string? Name,

        [StringLength(75, ErrorMessage = "Email can't be longer than 75 characters")]
        string? Email,

        [DateRange]
        DateRange? DateRange,
        
        Role? Role) : PaginationRequest;

    public enum UserSortCriterion { InvalidSortCriterion, ByName, ByEmail, ByRole, ByBirthday }

    public enum UserSeachCriterion { InvalidSerchCriterion, ByName, ByEmail, ByRole, ByBirthdayRange }
}