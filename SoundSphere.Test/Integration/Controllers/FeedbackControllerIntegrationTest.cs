using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
using System.Net.Mime;
using System.Text;
using static Newtonsoft.Json.JsonConvert;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.FeedbackMock;
using static SoundSphere.Test.Mocks.UserMock;
using static System.Net.HttpStatusCode;

namespace SoundSphere.Test.Integration.Controllers
{
    public class FeedbackControllerIntegrationTest : IDisposable
    {
        private readonly DbFixture _dbFixture;
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;

        public FeedbackControllerIntegrationTest() { _dbFixture = new DbFixture(); _factory = new(_dbFixture); _httpClient = _factory.CreateClient(); }

        public void Dispose() { _factory.Dispose(); _httpClient.Dispose(); }

        private async Task ExecuteAsync(Func<Task> action)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Users.RemoveRange(context.Users);
            context.Feedbacks.RemoveRange(context.Feedbacks);
            await _dbFixture.TrackAndAddAsync(context, _users);
            await _dbFixture.TrackAndAddAsync(context, _feedbacks);
            await context.SaveChangesAsync();
            await action();
            context.Users.RemoveRange(context.Users);
            context.Feedbacks.RemoveRange(context.Feedbacks);
            await context.SaveChangesAsync();
        }

        [Fact] public async Task GetAll_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PostAsync($"{ApiFeedback}/get", new StringContent(SerializeObject(_feedbackPayload), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<List<FeedbackDto>>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_feedbackDtosPagination);
        });

        [Fact] public async Task GetById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiFeedback}/{ValidFeedbackId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(OK);
            var responseBody = DeserializeObject<FeedbackDto>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(_feedbackDtos[0]);
        });

        [Fact] public async Task GetById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync($"{ApiFeedback}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(FeedbackNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task Add_Test() => await ExecuteAsync(async () =>
        {
            var addResponse = await _httpClient.PostAsync(ApiFeedback, new StringContent(SerializeObject(_newFeedbackDto), Encoding.UTF8, MediaTypeNames.Application.Json));
            addResponse.Should().NotBeNull();
            addResponse.StatusCode.Should().Be(Created);
            var addResponseBody = DeserializeObject<FeedbackDto>(await addResponse.Content.ReadAsStringAsync());
            addResponseBody.Should().BeEquivalentTo(_newFeedbackDto, options => options.Excluding(feedback => feedback.Id).Excluding(feedback => feedback.CreatedAt).Excluding(feedback => feedback.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiFeedback}/{addResponseBody?.Id}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<FeedbackDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(addResponseBody);
        });

        [Fact] public async Task UpdateById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            FeedbackDto updatedFeedbackDto = _feedbackDtos[0];
            updatedFeedbackDto.Type = _feedbackDtos[1].Type;
            updatedFeedbackDto.Message = _feedbackDtos[1].Message;
            var updateResponse = await _httpClient.PutAsync($"{ApiFeedback}/{ValidFeedbackId}", new StringContent(SerializeObject(_feedbackDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            updateResponse.Should().NotBeNull();
            updateResponse.StatusCode.Should().Be(OK);
            var updateResponseBody = DeserializeObject<FeedbackDto>(await updateResponse.Content.ReadAsStringAsync());
            updateResponseBody.Should().BeEquivalentTo(updatedFeedbackDto, options => options.Excluding(feedback => feedback.UpdatedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiFeedback}/{ValidFeedbackId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(OK);
            var getResponseBody = DeserializeObject<FeedbackDto>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(updatedFeedbackDto, options => options.Excluding(feedback => feedback.UpdatedAt));
        });

        [Fact] public async Task UpdateById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.PutAsync($"{ApiFeedback}/{InvalidId}", new StringContent(SerializeObject(_feedbackDtos[1]), Encoding.UTF8, MediaTypeNames.Application.Json));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(FeedbackNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_ValidId_Test() => await ExecuteAsync(async () =>
        {
            FeedbackDto deletedFeedbackDto = _feedbackDtos[0];
            deletedFeedbackDto.DeletedAt = DateTime.UtcNow;
            var deleteResponse = await _httpClient.DeleteAsync($"{ApiFeedback}/{ValidFeedbackId}");
            deleteResponse.Should().NotBeNull();
            deleteResponse.StatusCode.Should().Be(OK);
            var deleteResponseBody = DeserializeObject<FeedbackDto>(await deleteResponse.Content.ReadAsStringAsync());
            deleteResponseBody.Should().BeEquivalentTo(deletedFeedbackDto, options => options.Excluding(feedback => feedback.DeletedAt));

            var getResponse = await _httpClient.GetAsync($"{ApiFeedback}/{ValidFeedbackId}");
            getResponse.Should().NotBeNull();
            getResponse.StatusCode.Should().Be(NotFound);
            var getResponseBody = DeserializeObject<ProblemDetails>(await getResponse.Content.ReadAsStringAsync());
            getResponseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(FeedbackNotFound, ValidFeedbackId), Status = StatusCodes.Status404NotFound });
        });

        [Fact] public async Task DeleteById_InvalidId_Test() => await ExecuteAsync(async () =>
        {
            var response = await _httpClient.DeleteAsync($"{ApiFeedback}/{InvalidId}");
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(NotFound);
            var responseBody = DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());
            responseBody.Should().BeEquivalentTo(new ProblemDetails { Title = "Resource not found", Detail = string.Format(FeedbackNotFound, InvalidId), Status = StatusCodes.Status404NotFound });
        });
    }
}