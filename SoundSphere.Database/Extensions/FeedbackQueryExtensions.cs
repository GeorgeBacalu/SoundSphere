using Microsoft.Data.SqlClient;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Extensions
{
    public static class FeedbackQueryExtensions
    {
        public static IQueryable<Feedback> ApplyPagination(this IQueryable<Feedback> query, FeedbackPaginationRequest payload) => payload == null ? query.Take(10) : query.Filter(payload).Sort(payload).Paginate(payload);

        private static IQueryable<Feedback> Filter(this IQueryable<Feedback> query, FeedbackPaginationRequest payload)
        {
            if (payload.SearchCriteria == null || payload.SearchCriteria.Count == 0)
                return query;
            payload.SearchCriteria.ForEach(searchCriterion => query = searchCriterion switch
            {
                FeedbackSearchCriterion.ByType => query.Where(feedback => feedback.Type == payload.Type),
                FeedbackSearchCriterion.ByMessage => !string.IsNullOrWhiteSpace(payload.Message) ? query.Where(feedback => feedback.Message.Contains(payload.Message)) : query,
                FeedbackSearchCriterion.ByUserName => !string.IsNullOrWhiteSpace(payload.UserName) ? query.Where(feedback => feedback.User.Name.Contains(payload.UserName)) : query,
                FeedbackSearchCriterion.ByCreateDateRange => payload.DateRange is { StartDate: DateTime startDate, EndDate: DateTime endDate } ? query.Where(feedback => feedback.CreatedAt >= startDate && feedback.CreatedAt <= endDate) : query,
                _ => query
            });
            return query;
        }

        private static IQueryable<Feedback> Sort(this IQueryable<Feedback> query, FeedbackPaginationRequest payload)
        {
            if (payload.SortCriteria == null || payload.SortCriteria.Count == 0)
                return query.OrderBy(feedback => feedback.CreatedAt);
            var firstCriterion = payload.SortCriteria.First();
            var newQuery = firstCriterion.Key switch
            {
                FeedbackSortCriterion.ByType => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(feedback => feedback.Type) : query.OrderByDescending(feedback => feedback.Type),
                FeedbackSortCriterion.ByMessage => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(feedback => feedback.Message) : query.OrderByDescending(feedback => feedback.Message),
                FeedbackSortCriterion.ByUserName => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(feedback => feedback.User.Name) : query.OrderByDescending(feedback => feedback.User.Name),
                FeedbackSortCriterion.ByCreateDate => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(feedback => feedback.CreatedAt) : query.OrderByDescending(feedback => feedback.CreatedAt),
                _ => query.OrderBy(feedback => feedback.CreatedAt)
            };
            payload.SortCriteria.Skip(1).ToList().ForEach(sortCriterion => newQuery = sortCriterion.Key switch
            {
                FeedbackSortCriterion.ByType => firstCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(feedback => feedback.Type) : newQuery.ThenByDescending(feedback => feedback.Type),
                FeedbackSortCriterion.ByMessage => firstCriterion.Value == SortOrder.Ascending ? newQuery.OrderBy(feedback => feedback.Message) : newQuery.ThenByDescending(feedback => feedback.Message),
                FeedbackSortCriterion.ByUserName => firstCriterion.Value == SortOrder.Ascending ? newQuery.OrderBy(feedback => feedback.User.Name) : newQuery.ThenByDescending(feedback => feedback.User.Name),
                FeedbackSortCriterion.ByCreateDate => firstCriterion.Value == SortOrder.Ascending ? newQuery.OrderBy(feedback => feedback.CreatedAt) : newQuery.ThenByDescending(feedback => feedback.CreatedAt),
                _ => newQuery
            });
            return newQuery;
        }

        private static IQueryable<Feedback> Paginate(this IQueryable<Feedback> query, FeedbackPaginationRequest payload) => query.Skip(payload.Page * payload.Size).Take(payload.Size);
    }
}