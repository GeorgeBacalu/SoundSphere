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
        private readonly Playlist _playlist1 = GetPlaylist1();
        private readonly Playlist _playlist2 = GetPlaylist2();
        private readonly Playlist _newPlaylist = GetNewPlaylist();
        private readonly List<Playlist> _playlists = GetPlaylists();

        public PlaylistRepositoryTest()
        {
            IQueryable<Playlist> queryablePlaylists = _playlists.AsQueryable();
            _dbSetMock.As<IQueryable<Playlist>>().Setup(mock => mock.Provider).Returns(queryablePlaylists.Provider);
            _dbSetMock.As<IQueryable<Playlist>>().Setup(mock => mock.Expression).Returns(queryablePlaylists.Expression);
            _dbSetMock.As<IQueryable<Playlist>>().Setup(mock => mock.ElementType).Returns(queryablePlaylists.ElementType);
            _dbSetMock.As<IQueryable<Playlist>>().Setup(mock => mock.GetEnumerator()).Returns(queryablePlaylists.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Playlists).Returns(_dbSetMock.Object);
            _playlistRepository = new PlaylistRepository(_dbContextMock.Object);
        }

        [Fact] public void GetAll_Test() => _playlistRepository.GetAll().Should().BeEquivalentTo(_playlists);

        [Fact] public void GetById_ValidId_Test() => _playlistRepository.GetById(ValidPlaylistId).Should().BeEquivalentTo(_playlist1);

        [Fact] public void GetById_InvalidId_Test() => _playlistRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId));

        [Fact] public void Add_Test()
        {
            Playlist result = _playlistRepository.Add(_newPlaylist);
            result.Should().BeEquivalentTo(_newPlaylist, options => options.Excluding(playlist => playlist.Id).Excluding(playlist => playlist.CreatedAt).Excluding(playlist => playlist.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<Playlist>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<Playlist>())).Returns(entryMock.Object);
            Playlist updatedPlaylist = _playlist1;
            updatedPlaylist.Title = _playlist2.Title;
            Playlist result = _playlistRepository.UpdateById(_playlist2, ValidPlaylistId);
            result.Should().BeEquivalentTo(updatedPlaylist, options => options.Excluding(playlist => playlist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_InvalidId_Test() => _playlistRepository
            .Invoking(repository => repository.UpdateById(_playlist2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId));

        [Fact] public void DeleteById_ValidId_Test()
        {
            Playlist result = _playlistRepository.DeleteById(ValidPlaylistId);
            result.Should().BeEquivalentTo(_playlist1, options => options.Excluding(playlist => playlist.DeletedAt));
            result.DeletedAt.Should().NotBe(null);
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void DeleteById_InvalidId_Test() => _playlistRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(PlaylistNotFound, InvalidId));
    }
}