using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.SongMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class SongControllerTest
    {
        private readonly Mock<ISongService> _songServiceMock = new();
        private readonly SongController _songController;

        public SongControllerTest() => _songController = new(_songServiceMock.Object);

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedSongs()
        {
            _songServiceMock.Setup(mock => mock.GetAllAsync(_songPayload)).ReturnsAsync(_songDtosPagination);
            OkObjectResult? result = await _songController.GetAllAsync(_songPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_songDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnSong_WhenSongIdIsValid()
        {
            _songServiceMock.Setup(mock => mock.GetByIdAsync(ValidSongId)).ReturnsAsync(_songDtos[0]);
            OkObjectResult? result = await _songController.GetByIdAsync(ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_songDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenSongIdIsInvalid()
        {
            _songServiceMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            await _songController.Invoking(controller => controller.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewSong_WhenSongDtoIsValid()
        {
            _songServiceMock.Setup(mock => mock.AddAsync(It.IsAny<SongDto>())).ReturnsAsync(_newSongDto);
            CreatedResult? result = await _songController.AddAsync(_newSongDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newSongDto);
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateSong_WhenSongIdIsValid()
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
            _songServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<SongDto>(), ValidSongId)).ReturnsAsync(updatedSongDto);
            OkObjectResult? result = await _songController.UpdateByIdAsync(_songDtos[1], ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedSongDto);
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenSongIdIsInvalid()
        {
            _songServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<SongDto>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            await _songController.Invoking(controller => controller.UpdateByIdAsync(_songDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteSong_WhenSongIdIsValid()
        {
            SongDto deletedSongDto = _songDtos[0];
            deletedSongDto.DeletedAt = DateTime.UtcNow;
            _songServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidSongId)).ReturnsAsync(deletedSongDto);
            OkObjectResult? result = await _songController.DeleteByIdAsync(ValidSongId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedSongDto);
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenSongIdIsInvalid()
        {
            _songServiceMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(SongNotFound, InvalidId)));
            await _songController.Invoking(controller => controller.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(SongNotFound, InvalidId));
        }
    }
}