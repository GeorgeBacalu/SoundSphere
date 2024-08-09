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
        private readonly User _user1 = GetUser1();
        private readonly User _user2 = GetUser2();
        private readonly User _newUser = GetNewUser();
        private readonly List<User> _users = GetUsers();
        private readonly UserDto _userDto1 = GetUserDto1();
        private readonly UserDto _userDto2 = GetUserDto2();
        private readonly UserDto _newUserDto = GetNewUserDto();
        private readonly List<UserDto> _userDtos = GetUserDtos();

        public UserServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _userService = new UserService(_userRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetAll()).Returns(_users);
            _userService.GetAll().Should().BeEquivalentTo(_userDtos);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.GetById(ValidUserId)).Returns(_user1);
            _userService.GetById(ValidUserId).Should().BeEquivalentTo(_userDto1);
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
            User updatedUser = _user1;
            updatedUser.Name = _user2.Name;
            updatedUser.Email = _user2.Email;
            updatedUser.Phone = _user2.Phone;
            updatedUser.Address = _user2.Address;
            updatedUser.Birthday = _user2.Birthday;
            updatedUser.ImageUrl = _user2.ImageUrl;
            updatedUser.Role = _user2.Role;
            UserDto updatedUserDto = updatedUser.ToDto(_mapper);
            _userRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<User>(), ValidUserId)).Returns(updatedUser);
            _userService.UpdateById(_userDto2, ValidUserId).Should().BeEquivalentTo(updatedUserDto);
            _userRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<User>(), ValidUserId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _userRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<User>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(UserNotFound, InvalidId)));
            _userService.Invoking(service => service.UpdateById(_userDto2, InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(UserNotFound, InvalidId));
            _userRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<User>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            User deletedUser = _user1;
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