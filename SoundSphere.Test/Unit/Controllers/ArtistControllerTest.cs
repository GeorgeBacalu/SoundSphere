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
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class ArtistControllerTest
    {
        private readonly Mock<IArtistService> _artistServiceMock = new();
        private readonly ArtistController _artistController;
        private readonly IMapper _mapper;
        private readonly ArtistDto _artistDto1 = GetArtistDto1();
        private readonly ArtistDto _artistDto2 = GetArtistDto1();
        private readonly ArtistDto _newArtistDto = GetNewArtistDto();
        private readonly List<ArtistDto> _artistDtos = GetArtistDtos();

        public ArtistControllerTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _artistController = new(_artistServiceMock.Object);
        }

        [Fact] public void GetAll_Test()
        {
            _artistServiceMock.Setup(mock => mock.GetAll()).Returns(_artistDtos);
            OkObjectResult? result = _artistController.GetAll() as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_artistDtos);
        }

        [Fact] public void GetById_Test()
        {
            _artistServiceMock.Setup(mock => mock.GetById(ValidArtistId)).Returns(_artistDto1);
            OkObjectResult? result = _artistController.GetById(ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_artistDto1);
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
            ArtistDto updatedArtistDto = _artistDto1;
            updatedArtistDto.Name = _artistDto2.Name;
            updatedArtistDto.ImageUrl = _artistDto2.ImageUrl;
            updatedArtistDto.Bio = _artistDto2.Bio;
            updatedArtistDto.SimilarArtistsIds = _artistDto2.SimilarArtistsIds;
            _artistServiceMock.Setup(mock => mock.UpdateById(It.IsAny<ArtistDto>(), ValidArtistId)).Returns(updatedArtistDto);
            OkObjectResult? result = _artistController.UpdateById(_artistDto2, ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedArtistDto);
        }

        [Fact] public void DeleteById_Test()
        {
            ArtistDto deletedArtistDto = _artistDto1;
            deletedArtistDto.DeletedAt = DateTime.UtcNow;
            _artistServiceMock.Setup(mock => mock.DeleteById(ValidArtistId)).Returns(deletedArtistDto);
            OkObjectResult? result = _artistController.DeleteById(ValidArtistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedArtistDto);
        }
    }
}