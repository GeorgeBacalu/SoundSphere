using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Unit.Repositories
{
    public class UserRepositoryTest
    {
        private readonly Mock<DbSet<User>> _dbSetMock = new();
        private readonly Mock<AppDbContext> _dbContextMock = new();
        private readonly IUserRepository _userRepository;

        public UserRepositoryTest()
        {
            IQueryable<User> queryableUsers = _users.AsQueryable();
            _dbSetMock.As<IQueryable<User>>().Setup(mock => mock.Provider).Returns(queryableUsers.Provider);
            _dbSetMock.As<IQueryable<User>>().Setup(mock => mock.Expression).Returns(queryableUsers.Expression);
            _dbSetMock.As<IQueryable<User>>().Setup(mock => mock.ElementType).Returns(queryableUsers.ElementType);
            _dbSetMock.As<IQueryable<User>>().Setup(mock => mock.GetEnumerator()).Returns(queryableUsers.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Users).Returns(_dbSetMock.Object);
            _userRepository = new UserRepository(_dbContextMock.Object);
        }

        [Fact] public void GetAll_Test() => _userRepository.GetAll(_userPayload).Should().BeEquivalentTo(_usersPagination);

        [Fact] public void GetById_ValidId_Test() => _userRepository.GetById(ValidUserId).Should().BeEquivalentTo(_users[0]);

        [Fact] public void GetById_InvalidId_Test() => _userRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId));

        [Fact] public void Add_Test()
        {
            User result = _userRepository.Add(_newUser);
            result.Should().BeEquivalentTo(_newUser, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Mock<CustomEntityEntry<User>> entryMock = new();
            entryMock.SetupProperty(mock => mock.State, EntityState.Modified);
            _dbContextMock.Setup(mock => mock.Entry(It.IsAny<User>())).Returns(entryMock.Object);
            User updatedUser = _users[0];
            updatedUser.Name = _users[1].Name;
            updatedUser.Email = _users[1].Email;
            updatedUser.Phone = _users[1].Phone;
            updatedUser.Address = _users[1].Address;
            updatedUser.Birthday = _users[1].Birthday;
            updatedUser.ImageUrl = _users[1].ImageUrl;
            updatedUser.Role = _users[1].Role;
            User result = _userRepository.UpdateById(_users[1], ValidUserId);
            result.Should().BeEquivalentTo(updatedUser, options => options.Excluding(user => user.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void UpdateById_InvalidId_Test() => _userRepository
            .Invoking(repository => repository.UpdateById(_users[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId));

        [Fact] public void DeleteById_ValidId_Test()
        {
            User result = _userRepository.DeleteById(ValidUserId);
            result.Should().BeEquivalentTo(_users[0], options => options.Excluding(user => user.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChanges());
        }

        [Fact] public void DeleteById_InvalidId_Test() => _userRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId));
    }
}