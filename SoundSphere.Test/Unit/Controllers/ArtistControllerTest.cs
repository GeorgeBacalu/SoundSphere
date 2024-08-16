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

        [Fact] public void GetAll_Test()
        {
            _artistServiceMock.Setup(mock => mock.GetAll(_artistPayload)).Returns(_artistDtosPagination);
            OkObjectResult? result = _artistController.GetAll(_artistPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_artistDtosPagination);
        }

        [Fact] public void GetById_Test()
        {
            _artistServiceMock.Setup(mock => mock.GetById(ValidArtistId)).Returns(_artistDtos[0]);
            OkObjectResult? result = _artistController.GetById(ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_artistDtos[0]);
        }

        [Fact] public void Add_Test()
        {
            _artistServiceMock.Setup(mock => mock.Add(It.IsAny<ArtistDto>())).Returns(_newArtistDto);
            CreatedResult? result = _artistController.Add(_newArtistDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newArtistDto);
        }

        [Fact] public void UpdateById_Test()
        {
            ArtistDto updatedArtistDto = _artistDtos[0];
            updatedArtistDto.Name = _artistDtos[1].Name;
            updatedArtistDto.ImageUrl = _artistDtos[1].ImageUrl;
            updatedArtistDto.Bio = _artistDtos[1].Bio;
            updatedArtistDto.SimilarArtistsIds = _artistDtos[1].SimilarArtistsIds;
            _artistServiceMock.Setup(mock => mock.UpdateById(It.IsAny<ArtistDto>(), ValidArtistId)).Returns(updatedArtistDto);
            OkObjectResult? result = _artistController.UpdateById(_artistDtos[1], ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedArtistDto);
        }

        [Fact] public void DeleteById_Test()
        {
            ArtistDto deletedArtistDto = _artistDtos[0];
            deletedArtistDto.DeletedAt = DateTime.UtcNow;
            _artistServiceMock.Setup(mock => mock.DeleteById(ValidArtistId)).Returns(deletedArtistDto);
            OkObjectResult? result = _artistController.DeleteById(ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedArtistDto);
        }
    }
}