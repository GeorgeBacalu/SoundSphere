using AutoMapper;
using FluentAssertions;
using Moq;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;
using static SoundSphere.Test.Mocks.ArtistMock;
using static SoundSphere.Test.Mocks.SongMock;

namespace SoundSphere.Test.Unit.Services
{
    public class SongServiceTest
    {
        private readonly Mock<ISongRepository> _songRepositoryMock = new();
        private readonly Mock<IAlbumRepository> _albumRepositoryMock = new();
        private readonly Mock<IArtistRepository> _artistRepositoryMock = new();
        private readonly ISongService _songService;
        private readonly IMapper _mapper;

        public SongServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _songService = new SongService(_songRepositoryMock.Object, _albumRepositoryMock.Object, _artistRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _songRepositoryMock.Setup(mock => mock.GetAll(_songPayload)).Returns(_songsPagination);
            _songService.GetAll(_songPayload).Should().BeEquivalentTo(_songDtosPagination);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.GetById(ValidSongId)).Returns(_songs[0]);
            _songService.GetById(ValidSongId).Should().BeEquivalentTo(_songDtos[0]);
        }

        [Fact] public void GetById_InvalidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.GetById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            _songService.Invoking(service => service.GetById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
            _songRepositoryMock.Verify(mock => mock.GetById(InvalidId));
        }

        [Fact] public void Add_Test()
        {
            _artists.Take(1).ToList().ForEach(artist => _artistRepositoryMock.Setup(mock => mock.GetById(artist.Id)).Returns(artist));
            _albumRepositoryMock.Setup(mock => mock.GetById(ValidAlbumId)).Returns(_albums[0]);
            _songRepositoryMock.Setup(mock => mock.Add(It.IsAny<Song>())).Returns(_newSong);
            _songService.Add(_newSongDto).Should().BeEquivalentTo(_newSongDto, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));
            _songRepositoryMock.Verify(mock => mock.Add(It.IsAny<Song>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Song updatedSong = _songs[0];
            updatedSong.Title = _songs[1].Title;
            updatedSong.ImageUrl = _songs[1].ImageUrl;
            updatedSong.Genre = _songs[1].Genre;
            updatedSong.ReleaseDate = _songs[1].ReleaseDate;
            updatedSong.DurationSeconds = _songs[1].DurationSeconds;
            updatedSong.Album = _songs[1].Album;
            updatedSong.Artists = _songs[1].Artists;
            updatedSong.SimilarSongs = _songs[1].SimilarSongs;
            SongDto updatedSongDto = updatedSong.ToDto(_mapper);
            _songRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Song>(), ValidSongId)).Returns(updatedSong);
            _songService.UpdateById(_songDtos[1], ValidSongId).Should().BeEquivalentTo(updatedSongDto);
            _songRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Song>(), ValidSongId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Song>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            _songService.Invoking(service => service.UpdateById(_songDtos[1], InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
            _songRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Song>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Song deletedSong = _songs[0];
            deletedSong.DeletedAt = DateTime.UtcNow;
            SongDto deletedSongDto = deletedSong.ToDto(_mapper);
            _songRepositoryMock.Setup(mock => mock.DeleteById(ValidSongId)).Returns(deletedSong);
            _songService.DeleteById(ValidSongId).Should().BeEquivalentTo(deletedSongDto);
            _songRepositoryMock.Verify(mock => mock.DeleteById(ValidSongId));
        }

        [Fact] public void DeleteById_InvalidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.DeleteById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            _songService.Invoking(service => service.DeleteById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
            _songRepositoryMock.Verify(mock => mock.DeleteById(InvalidId));
        }
    }
}