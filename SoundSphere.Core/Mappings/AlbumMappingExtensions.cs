using AutoMapper;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;

namespace SoundSphere.Core.Mappings
{
    public static class AlbumMappingExtensions
    {
        public static List<AlbumDto> ToDtos(this List<Album> albums, IMapper mapper) => albums.Select(album => album.ToDto(mapper)).ToList();

        public static List<Album> ToEntities(this List<AlbumDto> albumDtos, IMapper mapper) => albumDtos.Select(albumDto => albumDto.ToEntity(mapper)).ToList();

        public static AlbumDto ToDto(this Album album, IMapper mapper)
        {
            AlbumDto albumDto = mapper.Map<AlbumDto>(album);
            albumDto.SimilarAlbumsIds = album.SimilarAlbums.Select(albumPair => albumPair.SimilarAlbumId).ToList();
            return albumDto;
        }

        public static Album ToEntity(this AlbumDto albumDto, IMapper mapper)
        {
            Album album = mapper.Map<Album>(albumDto);
            album.SimilarAlbums = albumDto.SimilarAlbumsIds.Select(id => new AlbumPair { AlbumId = album.Id, SimilarAlbumId = id }).ToList();
            return album;
        }
    }
}