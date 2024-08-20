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
            var asyncQueryableUsers = (IQueryable<User>)new AsyncQueryable<User>(_users);
            _dbSetMock.As<IQueryable<User>>().Setup(mock => mock.Provider).Returns(asyncQueryableUsers.Provider);
            _dbSetMock.As<IQueryable<User>>().Setup(mock => mock.Expression).Returns(asyncQueryableUsers.Expression);
            _dbSetMock.As<IQueryable<User>>().Setup(mock => mock.ElementType).Returns(asyncQueryableUsers.ElementType);
            _dbSetMock.As<IQueryable<User>>().Setup(mock => mock.GetEnumerator()).Returns(asyncQueryableUsers.GetEnumerator());
            _dbContextMock.Setup(mock => mock.Users).Returns(_dbSetMock.Object);
            _userRepository = new UserRepository(_dbContextMock.Object);
        }

        [Fact] public async Task GetAll_Test() => (await _userRepository.GetAllAsync(_userPayload)).Should().BeEquivalentTo(_usersPagination);

        [Fact] public async Task GetById_ValidId_Test() => (await _userRepository.GetByIdAsync(ValidUserId)).Should().BeEquivalentTo(_users[0]);

        [Fact] public async Task GetById_InvalidId_Test() => await _userRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId));

        [Fact] public async Task Add_Test()
        {
            User result = await _userRepository.AddAsync(_newUser);
            result.Should().BeEquivalentTo(_newUser, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_ValidId_Test()
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
            User result = await _userRepository.UpdateByIdAsync(_users[1], ValidUserId);
            result.Should().BeEquivalentTo(updatedUser, options => options.Excluding(user => user.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task UpdateById_InvalidId_Test() => await _userRepository
            .Invoking(repository => repository.UpdateByIdAsync(_users[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId));

        [Fact] public async Task DeleteById_ValidId_Test()
        {
            User result = await _userRepository.DeleteByIdAsync(ValidUserId);
            result.Should().BeEquivalentTo(_users[0], options => options.Excluding(user => user.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _dbContextMock.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact] public async Task DeleteById_InvalidId_Test() => await _userRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId));
    }
}