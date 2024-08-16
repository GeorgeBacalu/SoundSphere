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

        [Fact] public void GetAll_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetAll(_userPayload)).Returns(_usersPagination);
            _userService.GetAll(_userPayload).Should().BeEquivalentTo(_userDtosPagination);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_users[0]);
            _userService.GetById(ValidUserId).Should().BeEquivalentTo(_userDtos[0]);
        }

        [Fact] public void GetById_InvalidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            _userService.Invoking(service => service.GetById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.GetById(InvalidId));
        }

        [Fact] public void Add_Test()
        {
            _userRepositoryMock.Setup(mock => mock.Add(It.IsAny<User>())).Returns(_newUser);
            _userService.Add(_newUserDto).Should().BeEquivalentTo(_newUserDto, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt));
            _userRepositoryMock.Verify(mock => mock.Add(It.IsAny<User>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
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
            _userRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<User>(), ValidUserId)).Returns(updatedUser);
            _userService.UpdateById(_userDtos[1], ValidUserId).Should().BeEquivalentTo(updatedUserDto);
            _userRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<User>(), ValidUserId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<User>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            _userService.Invoking(service => service.UpdateById(_userDtos[1], InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<User>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            User deletedUser = _users[0];
            deletedUser.DeletedAt = DateTime.UtcNow;
            UserDto deletedUserDto = deletedUser.ToDto(_mapper);
            _userRepositoryMock.Setup(mock => mock.DeleteById(ValidUserId)).Returns(deletedUser);
            _userService.DeleteById(ValidUserId).Should().BeEquivalentTo(deletedUserDto);
            _userRepositoryMock.Verify(mock => mock.DeleteById(ValidUserId));
        }

        [Fact] public void DeleteById_InvalidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.DeleteById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            _userService.Invoking(service => service.DeleteById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.DeleteById(InvalidId));
        }
    }
}