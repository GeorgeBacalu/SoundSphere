using Microsoft.Data.SqlClient;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Extensions
{
    public static class UserQueryExtensions
    {
        public static IQueryable<User> ApplyPagination(this IQueryable<User> query, UserPaginationRequest payload) => payload == null ? query.Take(10) : query.Filter(payload).Sort(payload).Paginate(payload);

        private static IQueryable<User> Filter(this IQueryable<User> query, UserPaginationRequest payload)
        {
            if (payload.SeachCriteria == null || payload.SeachCriteria.Count == 0)
                return query;
            payload.SeachCriteria.ForEach(searchCriterion => query = searchCriterion switch
            {
                UserSeachCriterion.ByName => !string.IsNullOrWhiteSpace(payload.Name) ? query.Where(user => user.Name.Contains(payload.Name)) : query,
                UserSeachCriterion.ByEmail => !string.IsNullOrWhiteSpace(payload.Email) ? query.Where(user => user.Email.Contains(payload.Email)) : query,
                UserSeachCriterion.ByRole => query.Where(user => user.Role == payload.Role),
                UserSeachCriterion.ByBirthdayRange => payload.DateRange is { StartDate: DateOnly startDate, EndDate: DateOnly endDate } ? query.Where(user => user.Birthday >= startDate && user.Birthday <= endDate) : query,
                _ => query
            });
            return query;
        }

        private static IQueryable<User> Sort(this IQueryable<User> query, UserPaginationRequest payload)
        {
            if (payload.SortCriteria == null || payload.SortCriteria.Count == 0)
                return query.OrderBy(user => user.CreatedAt);
            var firstCriterion = payload.SortCriteria.First();
            var newQuery = firstCriterion.Key switch
            {
                UserSortCriterion.ByName => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(user => user.Name) : query.OrderByDescending(user => user.Name),
                UserSortCriterion.ByEmail => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(user => user.Email) : query.OrderByDescending(user => user.Email),
                UserSortCriterion.ByRole => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(user => user.Role) : query.OrderByDescending(user => user.Role),
                UserSortCriterion.ByBirthday => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(user => user.Birthday) : query.OrderByDescending(user => user.Birthday),
                _ => query.OrderBy(user => user.CreatedAt)
            };
            payload.SortCriteria.Skip(1).ToList().ForEach(sortCriterion => newQuery = sortCriterion.Key switch
            {
                UserSortCriterion.ByName => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(user => user.Name) : newQuery.ThenByDescending(user => user.Name),
                UserSortCriterion.ByEmail => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(user => user.Email) : newQuery.ThenByDescending(user => user.Email),
                UserSortCriterion.ByRole => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(user => user.Role) : newQuery.ThenByDescending(user => user.Role),
                UserSortCriterion.ByBirthday => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(user => user.Birthday) : newQuery.ThenByDescending(user => user.Birthday),
                _ => newQuery
            });
            return newQuery;
        }

        private static IQueryable<User> Paginate(this IQueryable<User> query, UserPaginationRequest payload) => query.Skip(payload.Page * payload.Size).Take(payload.Size);
    }
}