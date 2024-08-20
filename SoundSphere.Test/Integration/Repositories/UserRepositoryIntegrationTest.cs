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

        private async Task ExecuteAsync(Func<UserRepository, AppDbContext, Task> action)
        {
            using var context = _dbFixture.CreateContext();
            var userRepository = new UserRepository(context);
            await using var transaction = await context.Database.BeginTransactionAsync();
            await context.Users.AddRangeAsync(_users);
            await context.SaveChangesAsync();
            await action(userRepository, context);
            await transaction.RollbackAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async (userRepository, context) => (await userRepository.GetAllAsync(_userPayload)).Should().BeEquivalentTo(_usersPagination));

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async (userRepository, context) => (await userRepository.GetByIdAsync(ValidUserId)).Should().BeEquivalentTo(_users[0], options => options));

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async (userRepository, context) => await userRepository
            .Invoking(repository => repository.GetByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public async Task Add_Test() => await ExecuteAsync(async (userRepository, context) =>
        {
            User result = await userRepository.AddAsync(_newUser);
            context.Users.Find(result.Id).Should().BeEquivalentTo(_newUser, options => options.Excluding(user => user.Id).Excluding(user => user.CreatedAt).Excluding(user => user.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async (userRepository, context) =>
        {
            User updatedUser = _users[0];
            updatedUser.Name = _newUser.Name;
            updatedUser.Email = _newUser.Email;
            updatedUser.Phone = _newUser.Phone;
            updatedUser.Address = _users[1].Address;
            updatedUser.Birthday = _users[1].Birthday;
            updatedUser.ImageUrl = _users[1].ImageUrl;
            updatedUser.Role = _users[1].Role;
            User result = await userRepository.UpdateByIdAsync(updatedUser, ValidUserId);
            result.Should().BeEquivalentTo(updatedUser, options => options.Excluding(user => user.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async (userRepository, context) => await userRepository
            .Invoking(repository => repository.UpdateByIdAsync(_users[1], InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async (userRepository, context) =>
        {
            User result = await userRepository.DeleteByIdAsync(ValidUserId);
            result.Should().BeEquivalentTo(_users[0], options => options.Excluding(user => user.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async (userRepository, context) => await userRepository
            .Invoking(repository => repository.DeleteByIdAsync(InvalidId))
            .Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage(string.Format(UserNotFound, InvalidId)));
    }
}