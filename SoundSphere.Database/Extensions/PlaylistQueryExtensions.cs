using Microsoft.Data.SqlClient;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Extensions
{
    public static class PlaylistQueryExtensions
    {
        public static IQueryable<Playlist> ApplyPagination(this IQueryable<Playlist> query, PlaylistPaginationRequest payload) => payload == null ? query.Take(10) : query.Filter(payload).Sort(payload).Paginate(payload);

        private static IQueryable<Playlist> Filter(this IQueryable<Playlist> query, PlaylistPaginationRequest payload)
        {
            if (payload.SearchCriteria == null || payload.SearchCriteria.Count == 0)
                return query;
            payload.SearchCriteria.ForEach(searchCriterion => query = searchCriterion switch
            {
                PlaylistSearchCriterion.ByTitle => !string.IsNullOrWhiteSpace(payload.Title) ? query.Where(playlist => playlist.Title.Contains(payload.Title)) : query,
                PlaylistSearchCriterion.ByUserName => !string.IsNullOrWhiteSpace(payload.UserName) ? query.Where(playlist => playlist.User.Name.Contains(payload.UserName)) : query,
                PlaylistSearchCriterion.BySongTitle => !string.IsNullOrWhiteSpace(payload.SongTitle) ? query.Where(playlist => playlist.Songs.Any(song => song.Title.Contains(payload.SongTitle))) : query,
                PlaylistSearchCriterion.ByCreateDateRange => payload.DateRange is { StartDate: DateTime startDate, EndDate: DateTime endDate } ? query.Where(playlist => playlist.CreatedAt >= startDate && playlist.CreatedAt <= endDate) : query,
                _ => query
            });
            return query;
        }

        private static IQueryable<Playlist> Sort(this IQueryable<Playlist> query, PlaylistPaginationRequest payload)
        {
            if (payload.SortCriteria == null || payload.SortCriteria.Count == 0)
                return query;
            var firstCriterion = payload.SortCriteria.First();
            var newQuery = firstCriterion.Key switch
            {
                PlaylistSortCriterion.ByTitle => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(playlist => playlist.Title) : query.OrderByDescending(playlist => playlist.Title),
                PlaylistSortCriterion.ByUserName => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(playlist => playlist.User.Name) : query.OrderByDescending(playlist => playlist.User.Name),
                PlaylistSortCriterion.ByCreateDate => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(playlist => playlist.CreatedAt) : query.OrderByDescending(playlist => playlist.CreatedAt),
                _ => query.OrderBy(playlist => playlist.CreatedAt)
            };
            payload.SortCriteria.Skip(1).ToList().ForEach(sortCriterion => newQuery = sortCriterion.Key switch
            {
                PlaylistSortCriterion.ByTitle => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(playlist => playlist.Title) : newQuery.ThenByDescending(playlist => playlist.Title),
                PlaylistSortCriterion.ByUserName => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(playlist => playlist.User.Name) : newQuery.ThenByDescending(playlist => playlist.User.Name),
                PlaylistSortCriterion.ByCreateDate => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(playlist => playlist.CreatedAt) : newQuery.ThenByDescending(playlist => playlist.CreatedAt),
                _ => newQuery
            });
            return newQuery;
        }

        private static IQueryable<Playlist> Paginate(this IQueryable<Playlist> query, PlaylistPaginationRequest payload) => query.Skip(payload.Page * payload.Size).Take(payload.Size);
    }
}