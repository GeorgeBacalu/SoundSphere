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
using static SoundSphere.Test.Mocks.AlbumMock;

namespace SoundSphere.Test.Unit.Services
{
    public class AlbumServiceTest
    {
        private readonly Mock<IAlbumRepository> _albumRepositoryMock = new();
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        public AlbumServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _albumService = new AlbumService(_albumRepositoryMock.Object, _mapper);
        }

        [Fact] public async Task GetAllAsync_ShouldReturnPaginatedAlbums()
        {
            _albumRepositoryMock.Setup(mock => mock.GetAllAsync(_albumPayload)).ReturnsAsync(_albumsPagination);
            (await _albumService.GetAllAsync(_albumPayload)).Should().BeEquivalentTo(_albumDtosPagination);
        }

        [Fact] public async Task GetByIdAsync_ShouldReturnAlbum_WhenAlbumIdIsValid()
        {
            _albumRepositoryMock.Setup(mock => mock.GetByIdAsync(ValidAlbumId)).ReturnsAsync(_albums[0]);
            (await _albumService.GetByIdAsync(ValidAlbumId)).Should().BeEquivalentTo(_albumDtos[0]);
        }

        [Fact] public async Task GetByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid()
        {
            _albumRepositoryMock.Setup(mock => mock.GetByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            await _albumService.Invoking(service => service.GetByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
            _albumRepositoryMock.Verify(mock => mock.GetByIdAsync(InvalidId));
        }

        [Fact] public async Task AddAsync_ShouldAddNewAlbum_WhenAlbumDtoIsValid()
        {
            _albumRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Album>())).ReturnsAsync(_newAlbum);
            (await _albumService.AddAsync(_newAlbumDto)).Should().BeEquivalentTo(_newAlbumDto, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            _albumRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Album>()));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldUpdateAlbum_WhenAlbumIdIsValid()
        {
            Album updatedAlbum = _albums[0];
            updatedAlbum.Title = _albums[1].Title;
            updatedAlbum.ImageUrl = _albums[1].ImageUrl;
            updatedAlbum.ReleaseDate = _albums[1].ReleaseDate;
            updatedAlbum.SimilarAlbums = _albums[1].SimilarAlbums;
            AlbumDto updatedAlbumDto = updatedAlbum.ToDto(_mapper);
            _albumRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Album>(), ValidAlbumId)).ReturnsAsync(updatedAlbum);
            (await _albumService.UpdateByIdAsync(_albumDtos[1], ValidAlbumId)).Should().BeEquivalentTo(updatedAlbumDto);
            _albumRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Album>(), ValidAlbumId));
        }

        [Fact] public async Task UpdateByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid()
        {
            _albumRepositoryMock.Setup(mock => mock.UpdateByIdAsync(It.IsAny<Album>(), InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            await _albumService.Invoking(service => service.UpdateByIdAsync(_albumDtos[1], InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
            _albumRepositoryMock.Verify(mock => mock.UpdateByIdAsync(It.IsAny<Album>(), InvalidId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldDeleteAlbum_WhenAlbumIdIsValid()
        {
            Album deletedAlbum = _albums[0];
            deletedAlbum.DeletedAt = DateTime.UtcNow;
            AlbumDto deletedAlbumDto = deletedAlbum.ToDto(_mapper);
            _albumRepositoryMock.Setup(mock => mock.DeleteByIdAsync(ValidAlbumId)).ReturnsAsync(deletedAlbum);
            (await _albumService.DeleteByIdAsync(ValidAlbumId)).Should().BeEquivalentTo(deletedAlbumDto);
            _albumRepositoryMock.Verify(mock => mock.DeleteByIdAsync(ValidAlbumId));
        }

        [Fact] public async Task DeleteByIdAsync_ShouldThrowException_WhenAlbumIdIsInvalid()
        {
            _albumRepositoryMock.Setup(mock => mock.DeleteByIdAsync(InvalidId)).ThrowsAsync(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            await _albumService.Invoking(service => service.DeleteByIdAsync(InvalidId))
                .Should().ThrowAsync<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
            _albumRepositoryMock.Verify(mock => mock.DeleteByIdAsync(InvalidId));
        }
    }
}