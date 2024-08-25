using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.PlaylistMock;

namespace SoundSphere.Test.Unit.Repositories
{
    public class PlaylistRepositoryTest
    {
        private readonly Mock<DbSet<Playlist>> _dbSetMock = new();
        private readonly Mock<AppDbContext> _dbContextMock = new();
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistRepositoryTest()
        {
            var asyncQueryablePlaylists = (IQueryable<Playlist>)new AsyncQueryable<Playlist>(_playlists);
            _dbSetMock.As<IQueryable<Playlist>>().Setup(mock => mock.Provider).Returns(asyncQueryablePlaylists.Provider);
            _dbSetMock.As<IQueryable<Playlist>>().Setup(mock => mock.Expression).Returns(asyncQueryablePlaylists.Expression);
            _dbSetMock.As<IQueryable<Playlist>>().Setup(mock => mock.ElementType).Returns(asyncQueryablePlaylists.ElementType);
            _dbSetMock.As<IQueryable<Playlist>>().Setup(mock => mock.GetEnumerator()).Returns(asyncQueryablePlaylists.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Playlists).Returns(_dbSetMock.Object);
            _playlistRepository = new PlaylistRepository(_dbContextMock.Object);
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedPlaylists() => (await _playlistRepository.GetAllAsync(_playlistPayload)).Should().BeEquivalentTo(_playlistsPagination);

        [Fact] public async Task GetByIdAsync_ShouldReturnPlaylist_WhenPlaylistIdIsValid() => (await _playlistRepository.GetByIdAsync(ValidPlaylistId)).Should().BeEquivalentTo(_playlists[0]);

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenPlaylistIdIsInvalid() => await _playlistRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId));

        [Fact] public async Task AddAsync_ShouldAddNewPlaylist_WhenPlaylistDtoIsValid()
        {
            Playlist result = await _playlistRepository.AddAsync(_newPlaylist);
            result.Should().BeEquivalentTo(_newPlaylist, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdatePlaylist_WhenPlaylistIdIsValid()
        {
            Mock<CustomEntityEntry<Playlist>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Playlist>())).Returns(entryMock.Object);
            Playlist updatedPlaylist = _playlists[0];
            updatedPlaylist.Title = _playlists[1].Title;
            Playlist result = await _playlistRepository.UpdateByIdAsync(_playlists[1], ValidPlaylistId);
            result.Should().BeEquivalentTo(updatedPlaylist, options => options.Excluding(playlist => playlist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenPlaylistIdIsInvalid() => await _playlistRepository
            .Invoking(repository => repository.UpdateByIdAsync(_playlists[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId));

        [Fact] public async Task DeleteByIdAsync_ShouldDeletePlaylist_WhenPlaylistIdIsValid()
        {
            Playlist result = await _playlistRepository.DeleteByIdAsync(ValidPlaylistId);
            result.Should().BeEquivalentTo(_playlists[0], options => options.Excluding(playlist => playlist.DeletedAt));
            result.DeletedAt.Should().NotBe(null);
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenPlaylistIdIsInvalid() => await _playlistRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId));
    }
}