using Microsoft.Data.SqlClient;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Extensions
{
    public static class AlbumQueryExtensions
    {
        public static IQueryable<Album> ApplyPagination(this IQueryable<Album> query, AlbumPaginationRequest payload) => payload == null ? query.Take(10) : query.Filter(payload).Sort(payload).Paginate(payload);

        private static IQueryable<Album> Filter(this IQueryable<Album> query, AlbumPaginationRequest payload)
        {
            if (payload.SearchCriteria == null || payload.SearchCriteria.Count == 0)
                return query;
            payload.SearchCriteria.ForEach(searchCriterion => query = searchCriterion switch
            {
                AlbumSearchCriterion.ByTitle => !string.IsNullOrWhiteSpace(payload.Title) ? query.Where(album => album.Title.Contains(payload.Title)) : query,
                AlbumSearchCriterion.ByReleaseDateRange => payload.DateRange is { StartDate: DateOnly startDate, EndDate: DateOnly endDate } ? query.Where(album => album.ReleaseDate >= startDate && album.ReleaseDate <= endDate) : query,
                _ => query
            });
            return query;
        }

        private static IQueryable<Album> Sort(this IQueryable<Album> query, AlbumPaginationRequest payload)
        {
            if (payload.SortCriteria == null || payload.SortCriteria.Count == 0)
                return query;
            var firstCriterion = payload.SortCriteria.First();
            var newQuery = firstCriterion.Key switch
            {
                AlbumSortCriterion.ByTitle => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(album => album.Title) : query.OrderByDescending(album => album.Title),
                AlbumSortCriterion.ByReleaseDate => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(album => album.ReleaseDate) : query.OrderByDescending(album => album.ReleaseDate),
                _ => query.OrderBy(album => album.CreatedAt)
            };
            payload.SortCriteria.Skip(1).ToList().ForEach(sortCriterion => newQuery = sortCriterion.Key switch
            {
                AlbumSortCriterion.ByTitle => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(album => album.Title) : newQuery.ThenByDescending(album => album.Title),
                AlbumSortCriterion.ByReleaseDate => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(album => album.ReleaseDate) : newQuery.ThenByDescending(album => album.ReleaseDate),
                _ => newQuery
            });
            return newQuery;
        }

        private static IQueryable<Album> Paginate(this IQueryable<Album> query, AlbumPaginationRequest payload) => query.Skip(payload.Page * payload.Size).Take(payload.Size);
    }
}