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

        [Fact] public void GetAll_Test()
        {
            _albumServiceMock.Setup(mock => mock.GetAll(_albumPayload)).Returns(_albumDtosPagination);
            OkObjectResult? result = _albumController.GetAll(_albumPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_albumDtosPagination);
        }

        [Fact] public void GetById_Test()
        {
            _albumServiceMock.Setup(mock => mock.GetById(ValidAlbumId)).Returns(_albumDtos[0]);
            OkObjectResult? result = _albumController.GetById(ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_albumDtos[0]);
        }

        [Fact] public void Add_Test()
        {
            _albumServiceMock.Setup(mock => mock.Add(It.IsAny<AlbumDto>())).Returns(_newAlbumDto);
            CreatedResult? result = _albumController.Add(_newAlbumDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newAlbumDto);
        }

        [Fact] public void UpdateById_Test()
        {
            AlbumDto updatedAlbumDto = _albumDtos[0];
            updatedAlbumDto.Title = _albumDtos[1].Title;
            updatedAlbumDto.ImageUrl = _albumDtos[1].ImageUrl;
            updatedAlbumDto.ReleaseDate = _albumDtos[1].ReleaseDate;
            updatedAlbumDto.SimilarAlbumsIds = _albumDtos[1].SimilarAlbumsIds;
            _albumServiceMock.Setup(mock => mock.UpdateById(It.IsAny<AlbumDto>(), ValidAlbumId)).Returns(updatedAlbumDto);
            OkObjectResult? result = _albumController.UpdateById(_albumDtos[1], ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedAlbumDto);
        }

        [Fact] public void DeleteById_Test()
        {
            AlbumDto deletedAlbumDto = _albumDtos[0];
            deletedAlbumDto.DeletedAt = DateTime.UtcNow;
            _albumServiceMock.Setup(mock => mock.DeleteById(ValidAlbumId)).Returns(deletedAlbumDto);
            OkObjectResult? result = _albumController.DeleteById(ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedAlbumDto);
        }
    }
}