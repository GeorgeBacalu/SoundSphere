using AutoMapper;
using FluentAssertions;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services;
using SoundSphere.Database.Context;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories;
using SoundSphere.Infrastructure.Exceptions;
using static SoundSphere.Database.Constants;
using static SoundSphere.Test.Mocks.ArtistMock;

namespace SoundSphere.Test.Integration.Services
{
    public class ArtistServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;
        private readonly Artist _artist1 = GetArtist1();
        private readonly Artist _artist2 = GetArtist2();
        private readonly Artist _newArtist = GetNewArtist();
        private readonly List<Artist> _artists = GetArtists();
        private readonly ArtistDto _artistDto1 = GetArtistDto1();
        private readonly ArtistDto _artistDto2 = GetArtistDto2();
        private readonly ArtistDto _newArtistDto = GetNewArtistDto();
        private readonly List<ArtistDto> _artistDtos = GetArtistDtos();

        public ArtistServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private void Execute(Action<ArtistService, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var artistService = new ArtistService(new ArtistRepository(context), _mapper);
            using var transaction = context.Database.BeginTransaction();
            context.Artists.AddRange(_artists);
            context.SaveChanges();
            action(artistService, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((artistService, context) => artistService.GetAll().Should().BeEquivalentTo(_artistDtos));

        [Fact] public void GetById_ValidId_Test() => Execute((artistService, context) => artistService.GetById(ValidArtistId).Should().BeEquivalentTo(_artistDto1));

        [Fact] public void GetById_InvalidId_Test() => Execute((artistService, context) => artistService
            .Invoking(service => service.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((artistService, context) =>
        {
            ArtistDto result = artistService.Add(_newArtistDto);
            context.Artists.Find(result.Id).Should().BeEquivalentTo(_newArtist, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((artistService, context) =>
        {
            Artist updatedArtist = _artist1;
            updatedArtist.Name = _artist2.Name;
            updatedArtist.ImageUrl = _artist2.ImageUrl;
            updatedArtist.Bio = _artist2.Bio;
            updatedArtist.SimilarArtists = _artist2.SimilarArtists;
            ArtistDto updatedArtistDto = updatedArtist.ToDto(_mapper);
            ArtistDto result = artistService.UpdateById(_artistDto2, ValidArtistId);
            result.Should().BeEquivalentTo(updatedArtistDto, options => options.Excluding(artist => artist.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((artistService, context) => artistService
            .Invoking(service => service.UpdateById(_artistDto2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((artistService, context) =>
        {
            ArtistDto result = artistService.DeleteById(ValidArtistId);
            result.Should().BeEquivalentTo(_artistDto1, options => options.Excluding(artist => artist.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((artistService, context) => artistService
            .Invoking(service => service.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(ArtistNotFound, InvalidId)));
    }
}