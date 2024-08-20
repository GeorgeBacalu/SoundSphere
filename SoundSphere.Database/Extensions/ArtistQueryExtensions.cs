using Microsoft.Data.SqlClient;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Extensions
{
    public static class ArtistQueryExtensions
    {
        public static IQueryable<Artist> ApplyPagination(this IQueryable<Artist> query, ArtistPaginationRequest payload) => payload == null ? query.Take(10) : query.Filter(payload).Sort(payload).Paginate(payload);

        private static IQueryable<Artist> Filter(this IQueryable<Artist> query, ArtistPaginationRequest payload)
        {
            if (payload.SearchCriteria == null || payload.SearchCriteria.Count == 0)
                return query;
            payload.SearchCriteria.ForEach(searchCriterion => query = searchCriterion switch
            {
                ArtistSearchCriterion.ByName => !string.IsNullOrWhiteSpace(payload.Name) ? query.Where(artist => artist.Name.Contains(payload.Name)) : query,
                _ => query
            });
            return query;
        }

        private static IQueryable<Artist> Sort(this IQueryable<Artist> query, ArtistPaginationRequest payload)
        {
            if (payload.SortCriteria == null || payload.SortCriteria.Count == 0)
                return query.OrderBy(artist => artist.CreatedAt);
            var firstCriterion = payload.SortCriteria.First();
            var newQuery = firstCriterion.Key switch
            {
                ArtistSortCriterion.ByName => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(artist => artist.Name) : query.OrderByDescending(artist => artist.Name),
                _ => query.OrderBy(artist => artist.CreatedAt)
            };
            payload.SortCriteria.Skip(1).ToList().ForEach(sortCriterion => newQuery = sortCriterion.Key switch
            {
                ArtistSortCriterion.ByName => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(artist => artist.Name) : newQuery.ThenByDescending(artist => artist.Name),
                _ => newQuery
            });
            return newQuery;
        }

        private static IQueryable<Artist> Paginate(this IQueryable<Artist> query, ArtistPaginationRequest payload) => query.Skip(payload.Page * payload.Size).Take(payload.Size);
    }
}