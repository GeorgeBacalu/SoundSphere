using Microsoft.Data.SqlClient;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Extensions
{
    public static class NotificationQueryExtensions
    {
        public static IQueryable<Notification> ApplyPagination(this IQueryable<Notification> query, NotificationPaginationRequest payload) => payload == null ? query.Take(10) : query.Filter(payload).Sort(payload).Paginate(payload);

        private static IQueryable<Notification> Filter(this IQueryable<Notification> query, NotificationPaginationRequest payload)
        {
            if (payload.SearchCriteria == null || payload.SearchCriteria.Count == 0)
                return query;
            payload.SearchCriteria.ForEach(searchCriteria => query = searchCriteria switch
            {
                NotificationSearchCriterion.ByType => query.Where(notification => notification.Type == payload.Type),
                NotificationSearchCriterion.ByMessage => !string.IsNullOrWhiteSpace(payload.Message) ? query.Where(notification => notification.Message.Contains(payload.Message)) : query,
                NotificationSearchCriterion.BySenderName => !string.IsNullOrWhiteSpace(payload.SenderName) ? query.Where(notification => notification.Sender.Name.Contains(payload.SenderName)) : query,
                NotificationSearchCriterion.ByIsRead => query.Where(notification => notification.IsRead == payload.IsRead),
                NotificationSearchCriterion.ByCreateDateRange => payload.DateRange is { StartDate: DateTime startDate, EndDate: DateTime endDate } ? query.Where(notification => notification.CreatedAt >= startDate && notification.CreatedAt <= endDate) : query,
                _ => query
            });
            return query;
        }

        private static IQueryable<Notification> Sort(this IQueryable<Notification> query, NotificationPaginationRequest payload)
        {
            if (payload.SortCriteria == null || payload.SortCriteria.Count == 0)
                return query;
            var firstCriterion = payload.SortCriteria.First();
            var newQuery = firstCriterion.Key switch
            {
                NotificationSortCriterion.ByType => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(notification => notification.Type) : query.OrderByDescending(notification => notification.Type),
                NotificationSortCriterion.ByMessage => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(notification => notification.Message) : query.OrderByDescending(notification => notification.Message),
                NotificationSortCriterion.BySenderName => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(notification => notification.Sender.Name) : query.OrderByDescending(notification => notification.Sender.Name),
                NotificationSortCriterion.ByCreateDate => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(notification => notification.CreatedAt) : query.OrderByDescending(notification => notification.CreatedAt),
                _ => query.OrderBy(notification => notification.CreatedAt)
            };
            payload.SortCriteria.Skip(1).ToList().ForEach(sortCriterion => newQuery = sortCriterion.Key switch
            {
                NotificationSortCriterion.ByType => firstCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(notification => notification.Type) : newQuery.ThenByDescending(notification => notification.Type),
                NotificationSortCriterion.ByMessage => firstCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(notification => notification.Message) : newQuery.ThenByDescending(notification => notification.Message),
                NotificationSortCriterion.BySenderName => firstCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(notification => notification.Sender.Name) : newQuery.ThenByDescending(notification => notification.Sender.Name),
                NotificationSortCriterion.ByCreateDate => firstCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(notification => notification.CreatedAt) : newQuery.ThenByDescending(notification => notification.CreatedAt),
                _ => newQuery
            });
            return newQuery;
        }

        private static IQueryable<Notification> Paginate(this IQueryable<Notification> query, NotificationPaginationRequest payload) => query.Skip(payload.Page * payload.Size).Take(payload.Size);
    }
}