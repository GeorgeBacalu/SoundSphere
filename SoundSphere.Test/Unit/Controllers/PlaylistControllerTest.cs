using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.PlaylistMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class PlaylistControllerTest
    {
        private readonly Mock<IPlaylistService> _playlistServiceMock = new();
        private readonly PlaylistController _playlistController;

        public PlaylistControllerTest() => _playlistController = new(_playlistServiceMock.Object);

        [Fact] public void GetAll_Test()
        {
            _playlistServiceMock.Setup(mock => mock.GetAll(_playlistPayload)).Returns(_playlistDtosPagination);
            OkObjectResult? result = _playlistController.GetAll(_playlistPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_playlistDtosPagination);
        }

        [Fact] public void GetById_Test()
        {
            _playlistServiceMock.Setup(mock => mock.GetById(ValidPlaylistId)).Returns(_playlistDtos[0]);
            OkObjectResult? result = _playlistController.GetById(ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_playlistDtos[0]);
        }

        [Fact] public void Add_Test()
        {
            _playlistServiceMock.Setup(mock => mock.Add(It.IsAny<PlaylistDto>())).Returns(_newPlaylistDto);
            CreatedResult? result = _playlistController.Add(_newPlaylistDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newPlaylistDto);
        }

        [Fact] public void UpdateById_Test()
        {
            PlaylistDto updatedPlaylistDto = _playlistDtos[0];
            updatedPlaylistDto.Title = _playlistDtos[1].Title;
            _playlistServiceMock.Setup(mock => mock.UpdateById(It.IsAny<PlaylistDto>(), ValidPlaylistId)).Returns(updatedPlaylistDto);
            OkObjectResult? result = _playlistController.UpdateById(_playlistDtos[1], ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedPlaylistDto);
        }

        [Fact] public void DeleteById_Test()
        {
            PlaylistDto deletedPlaylistDto = _playlistDtos[0];
            deletedPlaylistDto.DeletedAt = DateTime.UtcNow;
            _playlistServiceMock.Setup(mock => mock.DeleteById(ValidPlaylistId)).Returns(deletedPlaylistDto);
            OkObjectResult? result = _playlistController.DeleteById(ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedPlaylistDto);
        }
    }
}