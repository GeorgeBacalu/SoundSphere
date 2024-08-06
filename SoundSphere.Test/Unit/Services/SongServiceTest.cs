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
        private readonly Song _song1 = GetSong1();
        private readonly Song _song2 = GetSong2();
        private readonly Song _newSong = GetNewSong();
        private readonly List<Song> _songs = GetSongs();
        private readonly SongDto _songDto1 = GetSongDto1();
        private readonly SongDto _songDto2 = GetSongDto2();
        private readonly SongDto _newSongDto = GetNewSongDto();
        private readonly List<SongDto> _songDtos = GetSongDtos();
        private readonly Album _album1 = GetAlbum1();
        private readonly List<Artist> _artists1 = [GetArtist1()];

        public SongServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _songService = new SongService(_songRepositoryMock.Object, _albumRepositoryMock.Object, _artistRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _songRepositoryMock.Setup(mock => mock.GetAll()).Returns(_songs);
            _songService.GetAll().Should().BeEquivalentTo(_songDtos);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.GetById(ValidSongId)).Returns(_song1);
            _songService.GetById(ValidSongId).Should().BeEquivalentTo(_songDto1);
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
            _artists1.ForEach(artist => _artistRepositoryMock.Setup(mock => mock.GetById(artist.Id)).Returns(artist));
            _albumRepositoryMock.Setup(mock => mock.GetById(ValidAlbumId)).Returns(_album1);
            _songRepositoryMock.Setup(mock => mock.Add(It.IsAny<Song>())).Returns(_newSong);
            _songService.Add(_newSongDto).Should().BeEquivalentTo(_newSongDto, options => options.Excluding(song => song.Id).Excluding(song => song.CreatedAt).Excluding(song => song.UpdatedAt));
            _songRepositoryMock.Verify(mock => mock.Add(It.IsAny<Song>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Song updatedSong = _song1;
            updatedSong.Title = _song2.Title;
            updatedSong.ImageUrl = _song2.ImageUrl;
            updatedSong.Genre = _song2.Genre;
            updatedSong.ReleaseDate = _song2.ReleaseDate;
            updatedSong.DurationSeconds = _song2.DurationSeconds;
            updatedSong.Album = _song2.Album;
            updatedSong.Artists = _song2.Artists;
            updatedSong.SimilarSongs = _song2.SimilarSongs;
            SongDto updatedSongDto = updatedSong.ToDto(_mapper);
            _songRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Song>(), ValidSongId)).Returns(updatedSong);
            _songService.UpdateById(_songDto2, ValidSongId).Should().BeEquivalentTo(updatedSongDto);
            _songRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Song>(), ValidSongId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _songRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Song>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            _songService.Invoking(service => service.UpdateById(_songDto2, InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
            _songRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Song>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Song deletedSong = _song1;
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