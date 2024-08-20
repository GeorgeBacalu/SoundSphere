using AutoMapper;
using FluentAssertions;
using Moq;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
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

        [Fact] public async Task GetAll_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetAllAsync(_userPayload)).ReturnsAsync(_usersPagination);
            (await _userService.GetAllAsync(_userPayload)).Should().BeEquivalentTo(_userDtosPagination);
        }

        [Fact] public async Task GetById_ValidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidUserId)).ReturnsAsync(_users[0]);
            (await _userService.GetByIdAsync(ValidUserId)).Should().BeEquivalentTo(_userDtos[0]);
        }

        [Fact] public async Task GetById_InvalidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userService.Invoking(service => service.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.GetByIdAsync(InvalidId));
        }

        [Fact] public async Task Add_Test()
        {
            _userRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<User>())).ReturnsAsync(_newUser);
            (await _userService.AddAsync(_newUserDto)).Should().BeEquivalentTo(_newUserDto, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt));
            _userRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<User>()));
        }

        [Fact] public async Task UpdateById_ValidId_Test()
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

        [Fact] public async Task UpdateById_InvalidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<User>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userService.Invoking(service => service.UpdateByIdAsync(_userDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<User>(), InvalidId));
        }

        [Fact] public async Task DeleteById_ValidId_Test()
        {
            User deletedUser = _users[0];
            deletedUser.DeletedAt = DateTime.UtcNow;
            UserDto deletedUserDto = deletedUser.ToDto(_mapper);
            _userRepositoryMock.Setup(mock => mock.DeleteByIdAsync(ValidUserId)).ReturnsAsync(deletedUser);
            (await _userService.DeleteByIdAsync(ValidUserId)).Should().BeEquivalentTo(deletedUserDto);
            _userRepositoryMock.Verify(mock => mock.DeleteByIdAsync(ValidUserId));
        }

        [Fact] public async Task DeleteById_InvalidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            await _userService.Invoking(service => service.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.DeleteByIdAsync(InvalidId));
        }
    }
}