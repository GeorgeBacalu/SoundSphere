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
using static SoundSphere.Test.Mocks.AlbumMock;

namespace SoundSphere.Test.Integration.Services
{
    public class AlbumServiceIntegrationTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _dbFixture;
        private readonly IMapper _mapper;
        private readonly Album _album1 = GetAlbum1();
        private readonly Album _album2 = GetAlbum2();
        private readonly Album _newAlbum = GetNewAlbum();
        private readonly List<Album> _albums = GetAlbums();
        private readonly AlbumDto _albumDto1 = GetAlbumDto1();
        private readonly AlbumDto _albumDto2 = GetAlbumDto2();
        private readonly AlbumDto _newAlbumDto = GetNewAlbumDto();
        private readonly List<AlbumDto> _albumDtos = GetAlbumDtos();

        public AlbumServiceIntegrationTest(DbFixture dbFixture) => (_dbFixture, _mapper) = (dbFixture, new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>()).CreateMapper());

        private void Execute(Action<AlbumService, AppDbContext> action)
        {
            using var context = _dbFixture.CreateContext();
            var albumService = new AlbumService(new AlbumRepository(context), _mapper);
            using var transaction = context.Database.BeginTransaction();
            context.Albums.AddRange(_albums);
            context.SaveChanges();
            action(albumService, context);
            transaction.Rollback();
        }

        [Fact] public void GetAll_Test() => Execute((albumService, context) => albumService.GetAll().Should().BeEquivalentTo(_albumDtos));

        [Fact] public void GetById_ValidId_Test() => Execute((albumService, context) => albumService.GetById(ValidAlbumId).Should().BeEquivalentTo(_albumDto1));

        [Fact] public void GetById_InvalidId_Test() => Execute((albumService, context) => albumService
            .Invoking(service => service.GetById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public void Add_Test() => Execute((albumService, context) =>
        {
            AlbumDto result = albumService.Add(_newAlbumDto);
            context.Albums.Find(result.Id).Should().BeEquivalentTo(_newAlbum, options => options.Excluding(album => album.Id).Excluding(album => album.CreatedAt).Excluding(album => album.UpdatedAt));
            result.Id.Should().NotBe(Guid.Empty);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_ValidId_Test() => Execute((albumService, context) =>
        {
            Album updatedAlbum = _album1;
            updatedAlbum.Title = _album2.Title;
            updatedAlbum.ImageUrl = _album2.ImageUrl;
            updatedAlbum.ReleaseDate = _album2.ReleaseDate;
            updatedAlbum.SimilarAlbums = _album2.SimilarAlbums;
            AlbumDto updatedAlbumDto = updatedAlbum.ToDto(_mapper);
            AlbumDto result = albumService.UpdateById(_albumDto2, ValidAlbumId);
            result.Should().BeEquivalentTo(updatedAlbumDto, options => options.Excluding(album => album.UpdatedAt));
            result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void UpdateById_InvalidId_Test() => Execute((albumService, context) => albumService
            .Invoking(service => service.UpdateById(_albumDto2, InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));

        [Fact] public void DeleteById_ValidId_Test() => Execute((albumService, context) =>
        {
            AlbumDto result = albumService.DeleteById(ValidAlbumId);
            result.Should().BeEquivalentTo(_albumDto1, options => options.Excluding(album => album.DeletedAt));
            result.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        });

        [Fact] public void DeleteById_InvalidId_Test() => Execute((albumService, context) => albumService
            .Invoking(service => service.DeleteById(InvalidId))
            .Should().Throw<ResourceNotFoundException>()
            .WithMessage(string.Format(AlbumNotFound, InvalidId)));
    }
}