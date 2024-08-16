using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
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

        public UserControllerTest() => _userController = new(_userServiceMock.Object);

        [Fact] public void GetAll_Test()
        {
            _userServiceMock.Setup(mock => mock.GetAll(_userPayload)).Returns(_userDtosPagination);
            OkObjectResult? result = _userController.GetAll(_userPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_userDtosPagination);
        }

        [Fact] public void GetById_Test()
        {
            _userServiceMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_userDtos[0]);
            OkObjectResult? result = _userController.GetById(ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_userDtos[0]);
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
            UserDto updatedUserDto = _userDtos[0];
            updatedUserDto.Name = _userDtos[1].Name;
            updatedUserDto.Email = _userDtos[1].Email;
            updatedUserDto.Phone = _userDtos[1].Phone;
            updatedUserDto.Address = _userDtos[1].Address;
            updatedUserDto.Birthday = _userDtos[1].Birthday;
            updatedUserDto.ImageUrl = _userDtos[1].ImageUrl;
            updatedUserDto.Role = _userDtos[1].Role;
            _userServiceMock.Setup(mock => mock.UpdateById(It.IsAny<UserDto>(), ValidUserId)).Returns(updatedUserDto);
            OkObjectResult? result = _userController.UpdateById(_userDtos[1], ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedUserDto);
        }

        [Fact] public void DeleteById_Test()
        {
            UserDto deletedUserDto = _userDtos[0];
            deletedUserDto.DeletedAt = DateTime.UtcNow;
            _userServiceMock.Setup(mock => mock.DeleteById(ValidUserId)).Returns(deletedUserDto);
            OkObjectResult? result = _userController.DeleteById(ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedUserDto);
        }
    }
}