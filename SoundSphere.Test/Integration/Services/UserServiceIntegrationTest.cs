﻿using AutoMapper;
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
        private readonly User _user1 = GetUser1();
        private readonly User _user2 = GetUser2();
        private readonly User _newUser = GetNewUser();
        private readonly List<User> _users = GetUsers();
        private readonly UserDto _userDto1 = GetUserDto1();
        private readonly UserDto _userDto2 = GetUserDto2();
        private readonly UserDto _newUserDto = GetNewUserDto();
        private readonly List<UserDto> _userDtos = GetUserDtos();

        public UserServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private void Execute(Action<UserService, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var userService = new UserService(new UserRepository(context), _mapper);
            using var transaction = context.Database.BeginTransaction();
            context.Users.AddRange(_users);
            context.SaveChanges();
            action(userService, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((userService, context) => userService.GetAll().Should().BeEquivalentTo(_userDtos));

        [Fact] public void GetById_ValidId_Test() => Execute((userService, context) => userService.GetById(ValidUserId).Should().BeEquivalentTo(_userDto1));

        [Fact] public void GetById_InvalidId_Test() => Execute((userService, context) => userService
            .Invoking(service => service.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((userService, context) =>
        {
            UserDto result = userService.Add(_newUserDto);
            context.Users.Find(result.Id).Should().BeEquivalentTo(_newUser, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt).Excluding(user => user.Password));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((userService, context) =>
        {
            User updatedUser = _user1;
            updatedUser.Name = _newUser.Name;
            updatedUser.Email = _newUser.Email;
            updatedUser.Phone = _newUser.Phone;
            updatedUser.Address = _user2.Address;
            updatedUser.Birthday = _user2.Birthday;
            updatedUser.ImageUrl = _user2.ImageUrl;
            updatedUser.Role = _user2.Role;
            UserDto updatedUserDto = updatedUser.ToDto(_mapper);
            UserDto result = userService.UpdateById(updatedUserDto, ValidUserId);
            result.Should().BeEquivalentTo(updatedUserDto, options => options.Excluding(user => user.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((userService, context) => userService
            .Invoking(service => service.UpdateById(_userDto2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((userService, context) =>
        {
            UserDto result = userService.DeleteById(ValidUserId);
            result.Should().BeEquivalentTo(_userDto1, options => options.Excluding(user => user.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((userService, context) => userService
            .Invoking(service => service.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));
    }
}