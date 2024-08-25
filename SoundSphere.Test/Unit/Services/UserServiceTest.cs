using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Moq;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Config;
using SoundSphere.Infrastructure.Config.Models;
using SoundSphere.Infrastructure.Exceptions;
using System.Reflection;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Unit.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _userService = new UserService(_userRepositoryMock.Object, _mapper);
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedUsers()
        {
            _userRepositoryMock.Setup(mock => mock.GetAllAsync(_userPayload)).ReturnsAsync(_usersPagination);
            (await _userService.GetAllAsync(_userPayload)).Should().BeEquivalentTo(_userDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnUser_WhenUserIdIsValid()
        {
            _userRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidUserId)).ReturnsAsync(_users[0]);
            (await _userService.GetByIdAsync(ValidUserId)).Should().BeEquivalentTo(_userDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenUserIdIsInvalid()
        {
            _userRepositoryMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userService.Invoking(service => service.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.GetByIdAsync(InvalidId));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateUser_WhenUserIdIsValid()
        {
            User updatedUser = _users[0];
            updatedUser.Name = _users[1].Name;
            updatedUser.Email = _users[1].Email;
            updatedUser.Phone = _users[1].Phone;
            updatedUser.Address = _users[1].Address;
            updatedUser.Birthday = _users[1].Birthday;
            updatedUser.ImageUrl = _users[1].ImageUrl;
            updatedUser.Role = _users[1].Role;
            UserDto updatedUserDto = updatedUser.ToDto(_mapper);
            _userRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<User>(), ValidUserId)).ReturnsAsync(updatedUser);
            (await _userService.UpdateByIdAsync(_userDtos[1], ValidUserId)).Should().BeEquivalentTo(updatedUserDto);
            _userRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<User>(), ValidUserId));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenUserIdIsInvalid()
        {
            _userRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<User>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userService.Invoking(service => service.UpdateByIdAsync(_userDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<User>(), InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserIdIsValid()
        {
            User deletedUser = _users[0];
            deletedUser.DeletedAt = DateTime.UtcNow;
            UserDto deletedUserDto = deletedUser.ToDto(_mapper);
            _userRepositoryMock.Setup(mock => mock.DeleteByIdAsync(ValidUserId)).ReturnsAsync(deletedUser);
            (await _userService.DeleteByIdAsync(ValidUserId)).Should().BeEquivalentTo(deletedUserDto);
            _userRepositoryMock.Verify(mock => mock.DeleteByIdAsync(ValidUserId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenUserIdIsInvalid()
        {
            _userRepositoryMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userService.Invoking(service => service.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.DeleteByIdAsync(InvalidId));
        }

        [Fact] public async Task RegisterAsync_ShouldCreateNewUser_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<User>())).ReturnsAsync(_newUser);
            (await _userService.RegisterAsync(_registerRequestNewUser)).Should().BeEquivalentTo(_newUser, options => options.Excluding(user => user.PasswordSalt).Excluding(user => user.PasswordHash).Excluding(user => user.UserSongs).Excluding(user => user.UserArtists));
            _userRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<User>()));
        }

        [Fact] public async Task RegisterAsync_ShouldThrowException_WhenUserAlreadyExists()
        {
            _userRepositoryMock.Setup(mock => mock.GetByInfoAsync(_registerRequestExistingUser.Name, _registerRequestExistingUser.Email, _registerRequestExistingUser.Phone)).ReturnsAsync(_users[0]);
            await _userService.Invoking(service => service.RegisterAsync(_registerRequestExistingUser))
                .Should().ThrowAsync<InvalidRequestException>()
                .WithMessage(UserAlreadyExists);
            _userRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact] public async Task LoginAsync_ShouldReturnToken_WhenExistingUserLogsIn() // TODO: Find out how to mock AppConfig.JwtSettings
        {
            _userRepositoryMock.Setup(mock => mock.GetByEmailAsync(_loginRequestExistingUser.Email)).ReturnsAsync(_users[0]);
            (await _userService.LoginAsync(_loginRequestExistingUser)).Should().NotBeNull();
            _userRepositoryMock.Verify(mock => mock.GetByEmailAsync(_loginRequestExistingUser.Email));
        }

        [Fact] public async Task LoginAsync_ShouldThrowException_WhenNewUserTriesToLogin()
        {
            _userRepositoryMock.Setup(mock => mock.GetByEmailAsync(_loginRequestNewUser.Email)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserEmailNotFound, _loginRequestNewUser.Email)));
            await _userService.Invoking(service => service.LoginAsync(_loginRequestNewUser))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserEmailNotFound, _loginRequestNewUser.Email));
            _userRepositoryMock.Verify(mock => mock.GetByEmailAsync(_loginRequestNewUser.Email));
        }

        [Fact] public async Task LoginAsync_ShouldThrowException_WhenPasswordIsInvalid()
        {
            _userRepositoryMock.Setup(mock => mock.GetByEmailAsync(_loginRequestInvalidPassword.Email)).ReturnsAsync(_users[0]);
            await _userService.Invoking(service => service.LoginAsync(_loginRequestInvalidPassword))
                .Should().ThrowAsync<InvalidRequestException>()
                .WithMessage(InvalidPassword);
            _userRepositoryMock.Verify(mock => mock.GetByEmailAsync(_loginRequestInvalidPassword.Email));
        }
    }
}