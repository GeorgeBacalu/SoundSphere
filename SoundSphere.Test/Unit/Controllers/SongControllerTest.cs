using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.SongMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class SongControllerTest
    {
        private readonly Mock<ISongService> _songServiceMock = new();
        private readonly SongController _songController;
        private readonly SongDto _songDto1 = GetSongDto1();
        private readonly SongDto _songDto2 = GetSongDto1();
        private readonly SongDto _newSongDto = GetNewSongDto();
        private readonly List<SongDto> _songDtos = GetSongDtos();

        public SongControllerTest() => _songController = new(_songServiceMock.Object);

        [Fact] public void GetAll_Test()
        {
            _songServiceMock.Setup(mock => mock.GetAll()).Returns(_songDtos);
            OkObjectResult? result = _songController.GetAll() as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_songDtos);
        }

        [Fact] public void GetById_Test()
        {
            _songServiceMock.Setup(mock => mock.GetById(ValidSongId)).Returns(_songDto1);
            OkObjectResult? result = _songController.GetById(ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_songDto1);
        }

        [Fact] public void Add_Test()
        {
            _songServiceMock.Setup(mock => mock.Add(It.IsAny<SongDto>())).Returns(_newSongDto);
            CreatedResult? result = _songController.Add(_newSongDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newSongDto);
        }

        [Fact] public void UpdateById_Test()
        {
            SongDto updatedSongDto = _songDto1;
            updatedSongDto.Title = _songDto2.Title;
            updatedSongDto.ImageUrl = _songDto2.ImageUrl;
            updatedSongDto.Genre = _songDto2.Genre;
            updatedSongDto.ReleaseDate = _songDto2.ReleaseDate;
            updatedSongDto.DurationSeconds = _songDto2.DurationSeconds;
            updatedSongDto.AlbumId = _songDto2.AlbumId;
            updatedSongDto.ArtistsIds = _songDto2.ArtistsIds;
            updatedSongDto.SimilarSongsIds = _songDto2.SimilarSongsIds;
            _songServiceMock.Setup(mock => mock.UpdateById(It.IsAny<SongDto>(), ValidSongId)).Returns(updatedSongDto);
            OkObjectResult? result = _songController.UpdateById(_songDto2, ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedSongDto);
        }

        [Fact] public void DeleteById_Test()
        {
            SongDto deletedSongDto = _songDto1;
            deletedSongDto.DeletedAt = DateTime.UtcNow;
            _songServiceMock.Setup(mock => mock.DeleteById(ValidSongId)).Returns(deletedSongDto);
            OkObjectResult? result = _songController.DeleteById(ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedSongDto);
        }
    }
}