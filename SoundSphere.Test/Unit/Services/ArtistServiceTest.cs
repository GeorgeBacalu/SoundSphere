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
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Unit.Services
{
    public class ArtistServiceTest
    {
        private readonly Mock<IArtistRepository> _artistRepositoryMock = new();
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _artistService = new ArtistService(_artistRepositoryMock.Object, _mapper);
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedArtists()
        {
            _artistRepositoryMock.Setup(mock => mock.GetAllAsync(_artistPayload)).ReturnsAsync(_artistsPagination);
            (await _artistService.GetAllAsync(_artistPayload)).Should().BeEquivalentTo(_artistDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnArtist_WhenArtistIdIsValid()
        {
            _artistRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidArtistId)).ReturnsAsync(_artists[0]);
            (await _artistService.GetByIdAsync(ValidArtistId)).Should().BeEquivalentTo(_artistDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid()
        {
            _artistRepositoryMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(ArtistNotFound, InvalidId)));
            await _artistService.Invoking(service => service.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(ArtistNotFound, InvalidId));
            _artistRepositoryMock.Verify(mock => mock.GetByIdAsync(InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewArtist_WhenArtistDtoIsValid()
        {
            _artistRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Artist>())).ReturnsAsync(_newArtist);
            (await _artistService.AddAsync(_newArtistDto)).Should().BeEquivalentTo(_newArtistDto, options => options.Excluding(artist => artist.Id).Excluding(artist => artist.CreatedAt).Excluding(artist => artist.UpdatedAt));
            _artistRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Artist>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateArtist_WhenArtistIdIsValid()
        {
            Artist updatedArtist = _artists[0];
            updatedArtist.Name = _artists[1].Name;
            updatedArtist.ImageUrl = _artists[1].ImageUrl;
            updatedArtist.Bio = _artists[1].Bio;
            updatedArtist.SimilarArtists = _artists[1].SimilarArtists;
            ArtistDto updatedArtistDto = updatedArtist.ToDto(_mapper);
            _artistRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Artist>(), ValidArtistId)).ReturnsAsync(updatedArtist);
            (await _artistService.UpdateByIdAsync(_artistDtos[1], ValidArtistId)).Should().BeEquivalentTo(updatedArtistDto);
            _artistRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Artist>(), ValidArtistId));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid()
        {
            _artistRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Artist>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(ArtistNotFound, InvalidId)));
            await _artistService.Invoking(service => service.UpdateByIdAsync(_artistDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(ArtistNotFound, InvalidId));
            _artistRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Artist>(), InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteArtist_WhenArtistIdIsValid()
        {
            Artist deletedArtist = _artists[0];
            deletedArtist.DeletedAt = DateTime.UtcNow;
            ArtistDto deletedArtistDto = deletedArtist.ToDto(_mapper);
            _artistRepositoryMock.Setup(mock => mock.DeleteByIdAsync(ValidArtistId)).ReturnsAsync(deletedArtist);
            (await _artistService.DeleteByIdAsync(ValidArtistId)).Should().BeEquivalentTo(deletedArtistDto);
            _artistRepositoryMock.Verify(mock => mock.DeleteByIdAsync(ValidArtistId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenArtistIdIsInvalid()
        {
            _artistRepositoryMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(ArtistNotFound, InvalidId)));
            await _artistService.Invoking(service => service.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(ArtistNotFound, InvalidId));
            _artistRepositoryMock.Verify(mock => mock.DeleteByIdAsync(InvalidId));
        }
    }
}