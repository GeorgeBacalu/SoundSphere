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

        [Fact] public void GetAll_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.GetAll(_albumPayload)).Returns(_albumsPagination);
            _albumService.GetAll(_albumPayload).Should().BeEquivalentTo(_albumDtosPagination);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.GetById(ValidAlbumId)).Returns(_albums[0]);
            _albumService.GetById(ValidAlbumId).Should().BeEquivalentTo(_albumDtos[0]);
        }

        [Fact] public void GetById_InvalidId_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.GetById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            _albumService.Invoking(service => service.GetById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
            _albumRepositoryMock.Verify(mock => mock.GetById(InvalidId));
        }

        [Fact] public void Add_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.Add(It.IsAny<Album>())).Returns(_newAlbum);
            _albumService.Add(_newAlbumDto).Should().BeEquivalentTo(_newAlbumDto, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            _albumRepositoryMock.Verify(mock => mock.Add(It.IsAny<Album>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Album updatedAlbum = _albums[0];
            updatedAlbum.Title = _albums[1].Title;
            updatedAlbum.ImageUrl = _albums[1].ImageUrl;
            updatedAlbum.ReleaseDate = _albums[1].ReleaseDate;
            updatedAlbum.SimilarAlbums = _albums[1].SimilarAlbums;
            AlbumDto updatedAlbumDto = updatedAlbum.ToDto(_mapper);
            _albumRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Album>(), ValidAlbumId)).Returns(updatedAlbum);
            _albumService.UpdateById(_albumDtos[1], ValidAlbumId).Should().BeEquivalentTo(updatedAlbumDto);
            _albumRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Album>(), ValidAlbumId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Album>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            _albumService.Invoking(service => service.UpdateById(_albumDtos[1], InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
            _albumRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Album>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Album deletedAlbum = _albums[0];
            deletedAlbum.DeletedAt = DateTime.UtcNow;
            AlbumDto deletedAlbumDto = deletedAlbum.ToDto(_mapper);
            _albumRepositoryMock.Setup(mock => mock.DeleteById(ValidAlbumId)).Returns(deletedAlbum);
            _albumService.DeleteById(ValidAlbumId).Should().BeEquivalentTo(deletedAlbumDto);
            _albumRepositoryMock.Verify(mock => mock.DeleteById(ValidAlbumId));
        }

        [Fact] public void DeleteById_InvalidId_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.DeleteById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            _albumService.Invoking(service => service.DeleteById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
            _albumRepositoryMock.Verify(mock => mock.DeleteById(InvalidId));
        }
    }
}