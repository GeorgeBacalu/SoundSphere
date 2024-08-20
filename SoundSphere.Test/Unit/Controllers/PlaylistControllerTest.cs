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

        [Fact] public async Task GetAll_Test()
        {
            _playlistServiceMock.Setup(mock => mock.GetAllAsync(_playlistPayload)).ReturnsAsync(_playlistDtosPagination);
            OkObjectResult? result = await _playlistController.GetAllAsync(_playlistPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_playlistDtosPagination);
        }

        [Fact] public async Task GetById_Test()
        {
            _playlistServiceMock.Setup(mock => mock.GetByIdAsync(ValidPlaylistId)).ReturnsAsync(_playlistDtos[0]);
            OkObjectResult? result = await _playlistController.GetByIdAsync(ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_playlistDtos[0]);
        }

        [Fact] public async Task Add_Test()
        {
            _playlistServiceMock.Setup(mock => mock.AddAsync(It.IsAny<PlaylistDto>())).ReturnsAsync(_newPlaylistDto);
            CreatedResult? result = await _playlistController.AddAsync(_newPlaylistDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newPlaylistDto);
        }

        [Fact] public async Task UpdateById_Test()
        {
            PlaylistDto updatedPlaylistDto = _playlistDtos[0];
            updatedPlaylistDto.Title = _playlistDtos[1].Title;
            _playlistServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<PlaylistDto>(), ValidPlaylistId)).ReturnsAsync(updatedPlaylistDto);
            OkObjectResult? result = await _playlistController.UpdateByIdAsync(_playlistDtos[1], ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedPlaylistDto);
        }

        [Fact] public async Task DeleteById_Test()
        {
            PlaylistDto deletedPlaylistDto = _playlistDtos[0];
            deletedPlaylistDto.DeletedAt = DateTime.UtcNow;
            _playlistServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidPlaylistId)).ReturnsAsync(deletedPlaylistDto);
            OkObjectResult? result = await _playlistController.DeleteByIdAsync(ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedPlaylistDto);
        }
    }
}