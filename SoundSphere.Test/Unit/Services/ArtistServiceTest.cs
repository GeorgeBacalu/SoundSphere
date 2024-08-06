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
        private readonly Artist _artist1 = GetArtist1();
        private readonly Artist _artist2 = GetArtist2();
        private readonly Artist _newArtist = GetNewArtist();
        private readonly List<Artist> _artists = GetArtists();
        private readonly ArtistDto _artistDto1 = GetArtistDto1();
        private readonly ArtistDto _artistDto2 = GetArtistDto2();
        private readonly ArtistDto _newArtistDto = GetNewArtistDto();
        private readonly List<ArtistDto> _artistDtos = GetArtistDtos();

        public ArtistServiceTest()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper();
            _artistService = new ArtistService(_artistRepositoryMock.Object, _mapper);
        }

        [Fact] public void GetAll_Test()
        {
            _artistRepositoryMock.Setup(mock => mock.GetAll()).Returns(_artists);
            _artistService.GetAll().Should().BeEquivalentTo(_artistDtos);
        }

        [Fact] public void GetById_ValidId_Test()
        {
            _artistRepositoryMock.Setup(mock => mock.GetById(ValidArtistId)).Returns(_artist1);
            _artistService.GetById(ValidArtistId).Should().BeEquivalentTo(_artistDto1);
        }

        [Fact] public void GetById_InvalidId_Test()
        {
            _artistRepositoryMock.Setup(mock => mock.GetById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(ArtistNotFound, InvalidId)));
            _artistService.Invoking(service => service.GetById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(ArtistNotFound, InvalidId));
            _artistRepositoryMock.Verify(mock => mock.GetById(InvalidId));
        }

        [Fact] public void Add_Test()
        {
            _artistRepositoryMock.Setup(mock => mock.Add(It.IsAny<Artist>())).Returns(_newArtist);
            _artistService.Add(_newArtistDto).Should().BeEquivalentTo(_newArtistDto, options => options.Excluding(artist => artist.Id).Excluding(artist => artist.CreatedAt).Excluding(artist => artist.UpdatedAt));
            _artistRepositoryMock.Verify(mock => mock.Add(It.IsAny<Artist>()));
        }

        [Fact] public void UpdateById_ValidId_Test()
        {
            Artist updatedArtist = _artist1;
            updatedArtist.Name = _artist2.Name;
            updatedArtist.ImageUrl = _artist2.ImageUrl;
            updatedArtist.Bio = _artist2.Bio;
            updatedArtist.SimilarArtists = _artist2.SimilarArtists;
            ArtistDto updatedArtistDto = updatedArtist.ToDto(_mapper);
            _artistRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Artist>(), ValidArtistId)).Returns(updatedArtist);
            _artistService.UpdateById(_artistDto2, ValidArtistId).Should().BeEquivalentTo(updatedArtistDto);
            _artistRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Artist>(), ValidArtistId));
        }

        [Fact] public void UpdateById_InvalidId_Test()
        {
            _artistRepositoryMock.Setup(mock => mock.UpdateById(It.IsAny<Artist>(), InvalidId)).Throws(new ResourceNotFoundException(string.Format(ArtistNotFound, InvalidId)));
            _artistService.Invoking(service => service.UpdateById(_artistDto2, InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(ArtistNotFound, InvalidId));
            _artistRepositoryMock.Verify(mock => mock.UpdateById(It.IsAny<Artist>(), InvalidId));
        }

        [Fact] public void DeleteById_ValidId_Test()
        {
            Artist deletedArtist = _artist1;
            deletedArtist.DeletedAt = DateTime.UtcNow;
            ArtistDto deletedArtistDto = deletedArtist.ToDto(_mapper);
            _artistRepositoryMock.Setup(mock => mock.DeleteById(ValidArtistId)).Returns(deletedArtist);
            _artistService.DeleteById(ValidArtistId).Should().BeEquivalentTo(deletedArtistDto);
            _artistRepositoryMock.Verify(mock => mock.DeleteById(ValidArtistId));
        }

        [Fact] public void DeleteById_InvalidId_Test()
        {
            _artistRepositoryMock.Setup(mock => mock.DeleteById(InvalidId)).Throws(new ResourceNotFoundException(string.Format(ArtistNotFound, InvalidId)));
            _artistService.Invoking(service => service.DeleteById(InvalidId))
                .Should().Throw<ResourceNotFoundException>()
                .WithMessage(string.Format(ArtistNotFound, InvalidId));
            _artistRepositoryMock.Verify(mock => mock.DeleteById(InvalidId));
        }
    }
}