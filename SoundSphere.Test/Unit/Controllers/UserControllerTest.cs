using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoundSphere.Api.Controllers;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Response;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Unit.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock = new();
        private readonly UserController _userController;

        public UserControllerTest() => _userController = new(_userServiceMock.Object);

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedUsers()
        {
            _userServiceMock.Setup(mock => mock.GetAllAsync(_userPayload)).ReturnsAsync(_userDtosPagination);
            OkObjectResult? result = await _userController.GetAllAsync(_userPayload) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_userDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnUser_WhenUserIdIsValid()
        {
            _userServiceMock.Setup(mock => mock.GetByIdAsync(ValidUserId)).ReturnsAsync(_userDtos[0]);
            OkObjectResult? result = await _userController.GetByIdAsync(ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(_userDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenUserIdIsInvalid()
        {
            _userServiceMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userController.Invoking(controller => controller.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateUser_WhenUserIdIsValid()
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

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenUserIdIsInvalid()
        {
            _userServiceMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<UserDto>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userController.Invoking(controller => controller.UpdateByIdAsync(_userDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserIdIsValid()
        {
            UserDto deletedUserDto = _userDtos[0];
            deletedUserDto.DeletedAt = DateTime.UtcNow;
            _userServiceMock.Setup(mock => mock.DeleteByIdAsync(ValidUserId)).ReturnsAsync(deletedUserDto);
            OkObjectResult? result = await _userController.DeleteByIdAsync(ValidUserId) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(deletedUserDto);
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenUserIdIsInvalid()
        {
            _userServiceMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userController.Invoking(controller => controller.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
        }

        [Fact] public async Task RegisterAsync_ShouldCreateNewUser_WhenUserDoesNotExist()
        {
            _userServiceMock.Setup(mock => mock.RegisterAsync(_registerRequestNewUser)).ReturnsAsync(_newUserDto);
            CreatedResult? result = await _userController.RegisterAsync(_registerRequestNewUser) as CreatedResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            result?.Value.Should().BeEquivalentTo(_newUserDto);
        }

        [Fact] public async Task RegisterAsync_ShouldThrowException_WhenUserAlreadyExists()
        {
            _userServiceMock.Setup(mock => mock.RegisterAsync(_registerRequestExistingUser)).ThrowsAsync(new InvalidRequestException(UserAlreadyExists));
            await _userController.Invoking(controller => controller.RegisterAsync(_registerRequestExistingUser))
                .Should().ThrowAsync<InvalidRequestException>()
                .WithMessage(UserAlreadyExists);
        }

        [Fact] public async Task LoginAsync_ShouldReturnToken_WhenExistingUserLogsIn()
        {
            _userServiceMock.Setup(mock => mock.LoginAsync(_loginRequestExistingUser)).ReturnsAsync(_token);
            OkObjectResult? result = await _userController.LoginAsync(_loginRequestExistingUser) as OkObjectResult;
            result?.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status200OK);
            result?.Value.Should().BeEquivalentTo(new LoginResponse(Token: _token));
        }

        [Fact] public async Task LoginAsync_ShouldThrowException_WhenNewUserTriesToLogin()
        {
            _userServiceMock.Setup(mock => mock.LoginAsync(_loginRequestNewUser)).ThrowsAsync(new InvalidRequestException(string.Format(UserEmailNotFound, _loginRequestNewUser.Email)));
            await _userController.Invoking(controller => controller.LoginAsync(_loginRequestNewUser))
                .Should().ThrowAsync<InvalidRequestException>()
                .WithMessage(string.Format(UserEmailNotFound, _loginRequestNewUser.Email));
        }

        [Fact] public async Task LoginAsync_ShouldThrowException_WhenPasswordIsInvalid()
        {
            _userServiceMock.Setup(mock => mock.LoginAsync(_loginRequestInvalidPassword)).ThrowsAsync(new InvalidRequestException(InvalidPassword));
            await _userController.Invoking(controller => controller.LoginAsync(_loginRequestInvalidPassword))
                .Should().ThrowAsync<InvalidRequestException>()
                .WithMessage(string.Format(InvalidPassword));
        }
    }
}