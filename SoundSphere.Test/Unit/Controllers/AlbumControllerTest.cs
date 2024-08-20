using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.AlbumMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class AlbumControllerTest
    {
        private readonly Mock<IAlbumService> _albumServiceMock = new();
        private readonly AlbumController _albumController;

        public AlbumControllerTest() => _albumController = new(_albumServiceMock.Object);

        [Fact] public async Task GetAll_Test()
        {
            _albumServiceMock.Setup(mock => mock.GetAllAsync(_albumPayload)).ReturnsAsync(_albumDtosPagination);
            OkObjectResult? result = await _albumController.GetAllAsync(_albumPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_albumDtosPagination);
        }

        [Fact] public async Task GetById_Test()
        {
            _albumServiceMock.Setup(mock => mock.GetByIdAsync(ValidAlbumId)).ReturnsAsync(_albumDtos[0]);
            OkObjectResult? result = await _albumController.GetByIdAsync(ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_albumDtos[0]);
        }

        [Fact] public async Task Add_Test()
        {
            _albumServiceMock.Setup(mock => mock.AddAsync(It.IsAny<AlbumDto>())).ReturnsAsync(_newAlbumDto);
            CreatedResult? result = await _albumController.AddAsync(_newAlbumDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newAlbumDto);
        }

        [Fact] public async Task UpdateById_Test()
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

        [Fact] public async Task DeleteById_Test()
        {
            AlbumDto deletedAlbumDto = _albumDtos[0];
            deletedAlbumDto.DeletedAt = DateTime.UtcNow;
            _albumServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidAlbumId)).ReturnsAsync(deletedAlbumDto);
            OkObjectResult? result = await _albumController.DeleteByIdAsync(ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedAlbumDto);
        }
    }
}