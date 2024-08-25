using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class AlbumControllerTest
    {
        private readonly Mock<IAlbumService> _albumServiceMock = new();
        private readonly AlbumController _albumController;

        public AlbumControllerTest() => _albumController = new(_albumServiceMock.Object);

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedAlbums()
        {
            _albumServiceMock.Setup(mock => mock.GetAllAsync(_albumPayload)).ReturnsAsync(_albumDtosPagination);
            OkObjectResult? result = await _albumController.GetAllAsync(_albumPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_albumDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnAlbum_WhenAlbumIdIsValid()
        {
            _albumServiceMock.Setup(mock => mock.GetByIdAsync(ValidAlbumId)).ReturnsAsync(_albumDtos[0]);
            OkObjectResult? result = await _albumController.GetByIdAsync(ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_albumDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid()
        {
            _albumServiceMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            await _albumController.Invoking(controller => controller.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewAlbum_WhenAlbumDtoIsValid()
        {
            _albumServiceMock.Setup(mock => mock.AddAsync(It.IsAny<AlbumDto>())).ReturnsAsync(_newAlbumDto);
            CreatedResult? result = await _albumController.AddAsync(_newAlbumDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newAlbumDto);
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateAlbum_WhenAlbumIdIsValid()
        {
            AlbumDto updatedAlbumDto = _albumDtos[0];
            updatedAlbumDto.Title = _albumDtos[1].Title;
            updatedAlbumDto.ImageUrl = _albumDtos[1].ImageUrl;
            updatedAlbumDto.ReleaseDate = _albumDtos[1].ReleaseDate;
            updatedAlbumDto.SimilarAlbumsIds = _albumDtos[1].SimilarAlbumsIds;
            _albumServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<AlbumDto>(), ValidAlbumId)).ReturnsAsync(updatedAlbumDto);
            OkObjectResult? result = await _albumController.UpdateByIdAsync(_albumDtos[1], ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedAlbumDto);
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid()
        {
            _albumServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<AlbumDto>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            await _albumController.Invoking(controller => controller.UpdateByIdAsync(_albumDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteAlbum_WhenAlbumIdIsValid()
        {
            AlbumDto deletedAlbumDto = _albumDtos[0];
            deletedAlbumDto.DeletedAt = DateTime.UtcNow;
            _albumServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidAlbumId)).ReturnsAsync(deletedAlbumDto);
            OkObjectResult? result = await _albumController.DeleteByIdAsync(ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedAlbumDto);
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid()
        {
            _albumServiceMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            await _albumController.Invoking(controller => controller.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
        }
    }
}