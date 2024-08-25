using AutoMapper;
using FluentAssertions;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Services
{
    public class UserServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public UserServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private async Task ExecuteAsync(Func<UserService, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var userService = new UserService(new UserRepository(context), _mapper);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await _dbFixture.TrackAndAddAsync(context, _users);
            await action(userService, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedUsers() => await ExecuteAsync(async (userService, context) => (await userService.GetAllAsync(_userPayload)).Should().BeEquivalentTo(_userDtosPagination));

        [Fact] public async Task GetByIdAsync_ShouldReturnUser_WhenUserIdIsValid() => await ExecuteAsync(async (userService, context) => (await userService.GetByIdAsync(ValidUserId)).Should().BeEquivalentTo(_userDtos[0]));

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenUserIdIsInvalid() => await ExecuteAsync(async (userService, context) => await userService
            .Invoking(service => service.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateUser_WhenUserIdIsValid() => await ExecuteAsync(async (userService, context) =>
        {
            User updatedUser = _users[0];
            updatedUser.Name = _newUser.Name;
            updatedUser.Email = _newUser.Email;
            updatedUser.Phone = _newUser.Phone;
            updatedUser.Address = _users[1].Address;
            updatedUser.Birthday = _users[1].Birthday;
            updatedUser.ImageUrl = _users[1].ImageUrl;
            updatedUser.Role = _users[1].Role;
            UserDto updatedUserDto = updatedUser.ToDto(_mapper);
            UserDto result = await userService.UpdateByIdAsync(updatedUserDto, ValidUserId);
            result.Should().BeEquivalentTo(updatedUserDto, options => options.Excluding(user => user.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenUserIdIsInvalid() => await ExecuteAsync(async (userService, context) => await userService
            .Invoking(service => service.UpdateByIdAsync(_userDtos[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserIdIsValid() => await ExecuteAsync(async (userService, context) =>
        {
            UserDto result = await userService.DeleteByIdAsync(ValidUserId);
            result.Should().BeEquivalentTo(_userDtos[0], options => options.Excluding(user => user.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenUserIdIsInvalid() => await ExecuteAsync(async (userService, context) => await userService
            .Invoking(service => service.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public async Task RegisterAsync_ShouldCreateNewUser_WhenUserDoesNotExist() => await ExecuteAsync(async (userService, context) => (await userService.RegisterAsync(_registerRequestNewUser)).Should().BeEquivalentTo(_newUser, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt).Excluding(user => user.PasswordSalt).Excluding(user => user.PasswordHash).Excluding(user => user.UserArtists).Excluding(user => user.UserSongs)));

        [Fact] public async Task RegisterAsync_ShouldThrowException_WhenUserAlreadyExists() => await ExecuteAsync(async (userService, context) => await userService
            .Invoking(service => service.RegisterAsync(_registerRequestExistingUser))
            .Should().ThrowAsync<InvalidRequestException>()
            .WithMessage(UserAlreadyExists));

        [Fact] public async Task LoginAsync_ShouldReturnToken_WhenExistingUserLogsIn() => await ExecuteAsync(async (userService, context) => (await userService.LoginAsync(_loginRequestExistingUser)).Should().NotBeNull()); // TODO: Find out how to mock AppConfig.JwtSettings

        [Fact] public async Task LoginAsync_ShouldThrowException_WhenNewUserTriesToLogin() => await ExecuteAsync(async (userService, context) => await userService
            .Invoking(service => service.LoginAsync(_loginRequestNewUser))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserEmailNotFound, _loginRequestNewUser.Email)));

        [Fact] public async Task LoginAsync_ShouldThrowException_WhenPasswordIsInvalid() => await ExecuteAsync(async (userService, context) => await userService
            .Invoking(service => service.LoginAsync(_loginRequestInvalidPassword))
            .Should().ThrowAsync<InvalidRequestException>()
            .WithMessage(InvalidPassword));
    }
}