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

        [Fact] public async Task GetAll_Test()
        {
            _userServiceMock.Setup(mock => mock.GetAllAsync(_userPayload)).ReturnsAsync(_userDtosPagination);
            OkObjectResult? result = await _userController.GetAllAsync(_userPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_userDtosPagination);
        }

        [Fact] public async Task GetById_Test()
        {
            _userServiceMock.Setup(mock => mock.GetByIdAsync(ValidUserId)).ReturnsAsync(_userDtos[0]);
            OkObjectResult? result = await _userController.GetByIdAsync(ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_userDtos[0]);
        }

        [Fact] public async Task Add_Test()
        {
            _userServiceMock.Setup(mock => mock.AddAsync(It.IsAny<UserDto>())).ReturnsAsync(_newUserDto);
            CreatedResult? result = await _userController.AddAsync(_newUserDto) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newUserDto);
        }

        [Fact] public async Task UpdateById_Test()
        {
            UserDto updatedUserDto = _userDtos[0];
            updatedUserDto.Name = _userDtos[1].Name;
            updatedUserDto.Email = _userDtos[1].Email;
            updatedUserDto.Phone = _userDtos[1].Phone;
            updatedUserDto.Address = _userDtos[1].Address;
            updatedUserDto.Birthday = _userDtos[1].Birthday;
            updatedUserDto.ImageUrl = _userDtos[1].ImageUrl;
            updatedUserDto.Role = _userDtos[1].Role;
            _userServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<UserDto>(), ValidUserId)).ReturnsAsync(updatedUserDto);
            OkObjectResult? result = await _userController.UpdateByIdAsync(_userDtos[1], ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(updatedUserDto);
        }

        [Fact] public async Task DeleteById_Test()
        {
            UserDto deletedUserDto = _userDtos[0];
            deletedUserDto.DeletedAt = DateTime.UtcNow;
            _userServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidUserId)).ReturnsAsync(deletedUserDto);
            OkObjectResult? result = await _userController.DeleteByIdAsync(ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedUserDto);
        }
    }
}