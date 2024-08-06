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
using static SoundSphere.Test.Mocks.PlaylistMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class PlaylistControllerTest
    {
        private readonly Mock<IPlaylistService> _playlistServiceMock = new();
        private readonly PlaylistController _playlistController;
        private readonly IMapper _mapper;
        private readonly PlaylistDto _playlistDto1 = GetPlaylistDto1();
        private readonly PlaylistDto _playlistDto2 = GetPlaylistDto1();
        private readonly PlaylistDto _newPlaylistDto = GetNewPlaylistDto();
        private readonly List<PlaylistDto> _playlistDtos = GetPlaylistDtos();

        public PlaylistControllerTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _playlistController = new(_playlistServiceMock.Object);
        }

        [Fact] public void GetAll_Test()
        {
            _playlistServiceMock.Setup(mock => mock.GetAll()).Returns(_playlistDtos);
            OkObjectResult? result = _playlistController.GetAll() as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_playlistDtos);
        }

        [Fact] public void GetById_Test()
        {
            _playlistServiceMock.Setup(mock => mock.GetById(ValidPlaylistId)).Returns(_playlistDto1);
            OkObjectResult? result = _playlistController.GetById(ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_playlistDto1);
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
            PlaylistDto updatedPlaylistDto = _playlistDto1;
            updatedPlaylistDto.Title = _playlistDto2.Title;
            _playlistServiceMock.Setup(mock => mock.UpdateById(It.IsAny<PlaylistDto>(), ValidPlaylistId)).Returns(updatedPlaylistDto);
            OkObjectResult? result = _playlistController.UpdateById(_playlistDto2, ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedPlaylistDto);
        }

        [Fact] public void DeleteById_Test()
        {
            PlaylistDto deletedPlaylistDto = _playlistDto1;
            deletedPlaylistDto.DeletedAt = DateTime.UtcNow;
            _playlistServiceMock.Setup(mock => mock.DeleteById(ValidPlaylistId)).Returns(deletedPlaylistDto);
            OkObjectResult? result = _playlistController.DeleteById(ValidPlaylistId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedPlaylistDto);
        }
    }
}