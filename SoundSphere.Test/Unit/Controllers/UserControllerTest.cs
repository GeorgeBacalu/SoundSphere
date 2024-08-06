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
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock = new();
        private readonly UserController _userController;
        private readonly IMapper _mapper;
        private readonly UserDto _userDto1 = GetUserDto1();
        private readonly UserDto _userDto2 = GetUserDto1();
        private readonly UserDto _newUserDto = GetNewUserDto();
        private readonly List<UserDto> _userDtos = GetUserDtos();

        public UserControllerTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _userController = new(_userServiceMock.Object);
        }

        [Fact] public void GetAll_Test()
        {
            _userServiceMock.Setup(mock => mock.GetAll()).Returns(_userDtos);
            OkObjectResult? result = _userController.GetAll() as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_userDtos);
        }

        [Fact] public void GetById_Test()
        {
            _userServiceMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_userDto1);
            OkObjectResult? result = _userController.GetById(ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_userDto1);
        }

        [Fact] public void Add_Test()
        {
            _userServiceMock.Setup(mock => mock.Add(It.IsAny<UserDto>())).Returns(_newUserDto);
            CreatedResult? result = _userController.Add(_newUserDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newUserDto);
        }

        [Fact] public void UpdateById_Test()
        {
            UserDto updatedUserDto = _userDto1;
            updatedUserDto.Name = _userDto2.Name;
            updatedUserDto.Email = _userDto2.Email;
            updatedUserDto.Phone = _userDto2.Phone;
            updatedUserDto.Address = _userDto2.Address;
            updatedUserDto.Birthday = _userDto2.Birthday;
            updatedUserDto.ImageUrl = _userDto2.ImageUrl;
            updatedUserDto.Role = _userDto2.Role;
            _userServiceMock.Setup(mock => mock.UpdateById(It.IsAny<UserDto>(), ValidUserId)).Returns(updatedUserDto);
            OkObjectResult? result = _userController.UpdateById(_userDto2, ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedUserDto);
        }

        [Fact] public void DeleteById_Test()
        {
            UserDto deletedUserDto = _userDto1;
            deletedUserDto.DeletedAt = DateTime.UtcNow;
            _userServiceMock.Setup(mock => mock.DeleteById(ValidUserId)).Returns(deletedUserDto);
            OkObjectResult? result = _userController.DeleteById(ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedUserDto);
        }
    }
}