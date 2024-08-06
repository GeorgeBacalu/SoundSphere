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
        private readonly Album _album1 = GetAlbum1();
        private readonly Album _album2 = GetAlbum2();
        private readonly Album _newAlbum = GetNewAlbum();
        private readonly List<Album> _albums = GetAlbums();
        private readonly AlbumDto _albumDto1 = GetAlbumDto1();
        private readonly AlbumDto _albumDto2 = GetAlbumDto2();
        private readonly AlbumDto _newAlbumDto = GetNewAlbumDto();
        private readonly List<AlbumDto> _albumDtos = GetAlbumDtos();

        public AlbumServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _albumService = new AlbumService(_albumRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.GetAll()).Returns(_albums);
            _albumService.GetAll().Should().BeEquivalentTo(_albumDtos);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.GetById(ValidAlbumId)).Returns(_album1);
            _albumService.GetById(ValidAlbumId).Should().BeEquivalentTo(_albumDto1);
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
            Album updatedAlbum = _album1;
            updatedAlbum.Title = _album2.Title;
            updatedAlbum.ImageUrl = _album2.ImageUrl;
            updatedAlbum.ReleaseDate = _album2.ReleaseDate;
            updatedAlbum.SimilarAlbums = _album2.SimilarAlbums;
            AlbumDto updatedAlbumDto = updatedAlbum.ToDto(_mapper);
            _albumRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Album>(), ValidAlbumId)).Returns(updatedAlbum);
            _albumService.UpdateById(_albumDto2, ValidAlbumId).Should().BeEquivalentTo(updatedAlbumDto);
            _albumRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Album>(), ValidAlbumId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _albumRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Album>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(AlbumNotFound, InvalidId)));
            _albumService.Invoking(service => service.UpdateById(_albumDto2, InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(AlbumNotFound, InvalidId));
            _albumRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Album>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Album deletedAlbum = _album1;
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