using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Mappings;
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
        private readonly IMapper _mapper;
        private readonly AlbumDto _albumDto1 = GetAlbumDto1();
        private readonly AlbumDto _albumDto2 = GetAlbumDto2();
        private readonly AlbumDto _newAlbumDto = GetNewAlbumDto();
        private readonly List<AlbumDto> _albumDtos = GetAlbumDtos();

        public AlbumControllerTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _albumController = new(_albumServiceMock.Object);
        }

        [Fact] public void GetAll_Test()
        {
            _albumServiceMock.Setup(mock => mock.GetAll()).Returns(_albumDtos);
            OkObjectResult? result = _albumController.GetAll() as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_albumDtos);
        }

        [Fact] public void GetById_Test()
        {
            _albumServiceMock.Setup(mock => mock.GetById(ValidAlbumId)).Returns(_albumDto1);
            OkObjectResult? result = _albumController.GetById(ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_albumDto1);
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
            AlbumDto updatedAlbumDto = _albumDto1;
            updatedAlbumDto.Title = _albumDto2.Title;
            updatedAlbumDto.ImageUrl = _albumDto2.ImageUrl;
            updatedAlbumDto.ReleaseDate = _albumDto2.ReleaseDate;
            updatedAlbumDto.SimilarAlbumsIds = _albumDto2.SimilarAlbumsIds;
            _albumServiceMock.Setup(mock => mock.UpdateById(It.IsAny<AlbumDto>(), ValidAlbumId)).Returns(updatedAlbumDto);
            OkObjectResult? result = _albumController.UpdateById(_albumDto2, ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedAlbumDto);
        }

        [Fact] public void DeleteById_Test()
        {
            AlbumDto deletedAlbumDto = _albumDto1;
            deletedAlbumDto.DeletedAt = DateTime.UtcNow;
            _albumServiceMock.Setup(mock => mock.DeleteById(ValidAlbumId)).Returns(deletedAlbumDto);
            OkObjectResult? result = _albumController.DeleteById(ValidAlbumId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedAlbumDto);
        }
    }
}