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

        public SongControllerTest() => _songController = new(_songServiceMock.Object);

        [Fact] public void GetAll_Test()
        {
            _songServiceMock.Setup(mock => mock.GetAll(_songPayload)).Returns(_songDtosPagination);
            OkObjectResult? result = _songController.GetAll(_songPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_songDtosPagination);
        }

        [Fact] public void GetById_Test()
        {
            _songServiceMock.Setup(mock => mock.GetById(ValidSongId)).Returns(_songDtos[0]);
            OkObjectResult? result = _songController.GetById(ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_songDtos[0]);
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
            SongDto updatedSongDto = _songDtos[0];
            updatedSongDto.Title = _songDtos[1].Title;
            updatedSongDto.ImageUrl = _songDtos[1].ImageUrl;
            updatedSongDto.Genre = _songDtos[1].Genre;
            updatedSongDto.ReleaseDate = _songDtos[1].ReleaseDate;
            updatedSongDto.DurationSeconds = _songDtos[1].DurationSeconds;
            updatedSongDto.AlbumId = _songDtos[1].AlbumId;
            updatedSongDto.ArtistsIds = _songDtos[1].ArtistsIds;
            updatedSongDto.SimilarSongsIds = _songDtos[1].SimilarSongsIds;
            _songServiceMock.Setup(mock => mock.UpdateById(It.IsAny<SongDto>(), ValidSongId)).Returns(updatedSongDto);
            OkObjectResult? result = _songController.UpdateById(_songDtos[1], ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedSongDto);
        }

        [Fact] public void DeleteById_Test()
        {
            SongDto deletedSongDto = _songDtos[0];
            deletedSongDto.DeletedAt = DateTime.UtcNow;
            _songServiceMock.Setup(mock => mock.DeleteById(ValidSongId)).Returns(deletedSongDto);
            OkObjectResult? result = _songController.DeleteById(ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedSongDto);
        }
    }
}