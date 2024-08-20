using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class ArtistControllerTest
    {
        private readonly Mock<IArtistService> _artistServiceMock = new();
        private readonly ArtistController _artistController;

        public ArtistControllerTest() => _artistController = new(_artistServiceMock.Object);

        [Fact] public async Task GetAll_Test()
        {
            _artistServiceMock.Setup(mock => mock.GetAllAsync(_artistPayload)).ReturnsAsync(_artistDtosPagination);
            OkObjectResult? result = await _artistController.GetAllAsync(_artistPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_artistDtosPagination);
        }

        [Fact] public async Task GetById_Test()
        {
            _artistServiceMock.Setup(mock => mock.GetByIdAsync(ValidArtistId)).ReturnsAsync(_artistDtos[0]);
            OkObjectResult? result = await _artistController.GetByIdAsync(ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_artistDtos[0]);
        }

        [Fact] public async Task Add_Test()
        {
            _artistServiceMock.Setup(mock => mock.AddAsync(It.IsAny<ArtistDto>())).ReturnsAsync(_newArtistDto);
            CreatedResult? result = await _artistController.AddAsync(_newArtistDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newArtistDto);
        }

        [Fact] public async Task UpdateById_Test()
        {
            ArtistDto updatedArtistDto = _artistDtos[0];
            updatedArtistDto.Name = _artistDtos[1].Name;
            updatedArtistDto.ImageUrl = _artistDtos[1].ImageUrl;
            updatedArtistDto.Bio = _artistDtos[1].Bio;
            updatedArtistDto.SimilarArtistsIds = _artistDtos[1].SimilarArtistsIds;
            _artistServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<ArtistDto>(), ValidArtistId)).ReturnsAsync(updatedArtistDto);
            OkObjectResult? result = await _artistController.UpdateByIdAsync(_artistDtos[1], ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedArtistDto);
        }

        [Fact] public async Task DeleteById_Test()
        {
            ArtistDto deletedArtistDto = _artistDtos[0];
            deletedArtistDto.DeletedAt = DateTime.UtcNow;
            _artistServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidArtistId)).ReturnsAsync(deletedArtistDto);
            OkObjectResult? result = await _artistController.DeleteByIdAsync(ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedArtistDto);
        }
    }
}