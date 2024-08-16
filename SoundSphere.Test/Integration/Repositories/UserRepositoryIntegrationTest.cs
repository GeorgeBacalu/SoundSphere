using FluentAssertions;
using SoundSphere.Database.Context;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.UserMock;

namespace SoundSphere.Test.Integration.Repositories
{
    public class UserRepositoryIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;

        public UserRepositoryIntegrationTest(DbFixture dbFixture) => _dbFixture = dbFixture;

        private void Execute(Action<UserRepository, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var userRepository = new UserRepository(context);
            using var transaction = context.Database.BeginTransaction();
            context.Users.AddRange(_users);
            context.SaveChanges();
            action(userRepository, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((userRepository, context) => userRepository.GetAll(_userPayload).Should().BeEquivalentTo(_usersPagination));

        [Fact] public void GetById_ValidId_Test() => Execute((userRepository, context) => userRepository.GetById(ValidUserId).Should().BeEquivalentTo(_users[0], options => options));

        [Fact] public void GetById_InvalidId_Test() => Execute((userRepository, context) => userRepository
            .Invoking(repository => repository.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((userRepository, context) =>
        {
            User result = userRepository.Add(_newUser);
            context.Users.Find(result.Id).Should().BeEquivalentTo(_newUser, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((userRepository, context) =>
        {
            User updatedUser = _users[0];
            updatedUser.Name = _newUser.Name;
            updatedUser.Email = _newUser.Email;
            updatedUser.Phone = _newUser.Phone;
            updatedUser.Address = _users[1].Address;
            updatedUser.Birthday = _users[1].Birthday;
            updatedUser.ImageUrl = _users[1].ImageUrl;
            updatedUser.Role = _users[1].Role;
            User result = userRepository.UpdateById(updatedUser, ValidUserId);
            result.Should().BeEquivalentTo(updatedUser, options => options.Excluding(user => user.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((userRepository, context) => userRepository
            .Invoking(repository => repository.UpdateById(_users[1], InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((userRepository, context) =>
        {
            User result = userRepository.DeleteById(ValidUserId);
            result.Should().BeEquivalentTo(_users[0], options => options.Excluding(user => user.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((userRepository, context) => userRepository
            .Invoking(repository => repository.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));
    }
}