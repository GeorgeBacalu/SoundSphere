using Microsoft.Data.SqlClient;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Extensions
{
    public static class SongQueryExtensions
    {
        public static IQueryable<Song> ApplyPagination(this IQueryable<Song> query, SongPaginationRequest payload) => payload == null ? query.Take(10) : query.Filter(payload).Sort(payload).Paginate(payload);

        private static IQueryable<Song> Filter(this IQueryable<Song> query, SongPaginationRequest payload)
        {
            if (payload.SearchCriteria == null || payload.SearchCriteria.Count == 0)
                return query;
            payload.SearchCriteria.ForEach(searchCriterion => query = searchCriterion switch
            {
                SongSearchCriterion.ByTitle => !string.IsNullOrWhiteSpace(payload.Title) ? query.Where(song => song.Title.Contains(payload.Title)) : query,
                SongSearchCriterion.ByGenre => query.Where(song => song.Genre == payload.Genre),
                SongSearchCriterion.ByReleaseDateRange => payload.DateRange is { StartDate: DateOnly startDate, EndDate: DateOnly endDate } ? query.Where(song => song.ReleaseDate >= startDate && song.ReleaseDate <= endDate) : query,
                SongSearchCriterion.ByDurationRange => payload.DurationRange is { MinSeconds: int minSeconds, MaxSeconds: int maxSeconds } ? query.Where(song => song.DurationSeconds >= minSeconds && song.DurationSeconds <= maxSeconds) : query,
                SongSearchCriterion.ByAlbumTitle => !string.IsNullOrWhiteSpace(payload.AlbumTitle) ? query.Where(song => song.Album.Title.Contains(payload.AlbumTitle)) : query,
                SongSearchCriterion.ByArtistName => !string.IsNullOrWhiteSpace(payload.ArtistName) ? query.Where(song => song.Artists.Any(artist => artist.Name.Contains(payload.ArtistName))) : query,
                _ => query
            });
            return query;
        }

        private static IQueryable<Song> Sort(this IQueryable<Song> query, SongPaginationRequest payload)
        {
            if (payload.SortCriteria == null || payload.SortCriteria.Count == 0)
                return query.OrderBy(song => song.CreatedAt);
            var firstCriterion = payload.SortCriteria.First();
            var newQuery = firstCriterion.Key switch
            {
                SongSortCriterion.ByTitle => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(song => song.Title) : query.OrderByDescending(song => song.Title),
                SongSortCriterion.ByGenre => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(song => song.Genre) : query.OrderByDescending(song => song.Genre),
                SongSortCriterion.ByReleaseDate => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(song => song.ReleaseDate) : query.OrderByDescending(song => song.ReleaseDate),
                SongSortCriterion.ByDuration => firstCriterion.Value == SortOrder.Ascending ? query.OrderBy(song => song.DurationSeconds) : query.OrderByDescending(song => song.DurationSeconds),
                _ => query.OrderBy(song => song.CreatedAt)
            };
            payload.SortCriteria.Skip(1).ToList().ForEach(sortCriterion => newQuery = sortCriterion.Key switch
            {
                SongSortCriterion.ByTitle => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(song => song.Title) : newQuery.ThenByDescending(song => song.Title),
                SongSortCriterion.ByGenre => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(song => song.Genre) : newQuery.ThenByDescending(song => song.Genre),
                SongSortCriterion.ByReleaseDate => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(song => song.ReleaseDate) : newQuery.ThenByDescending(song => song.ReleaseDate),
                SongSortCriterion.ByDuration => sortCriterion.Value == SortOrder.Ascending ? newQuery.ThenBy(song => song.DurationSeconds) : newQuery.ThenByDescending(song => song.DurationSeconds),
                _ => newQuery
            });
            return newQuery;
        }

        private static IQueryable<Song> Paginate(this IQueryable<Song> query, SongPaginationRequest payload) => query.Skip(payload.Page * payload.Size).Take(payload.Size);
    }
}