using AutoMapper;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Mappings
{
    public static class PlaylistMappingExtensions
    {
        public static List<PlaylistDto> ToDtos(this List<Playlist> playlists, IMapper mapper) => playlists.Select(playlist => playlist.ToDto(mapper)).ToList();

        public static async Task<List<Playlist>> ToEntitiesAsync(this List<PlaylistDto> playlistDtos, IUserRepository userRepository, ISongRepository songRepository, IMapper mapper) =>
            (await Task.WhenAll(playlistDtos.Select(playlistDto => playlistDto.ToEntityAsync(userRepository, songRepository, mapper)))).ToList();

        public static PlaylistDto ToDto(this Playlist playlist, IMapper mapper)
        {
            PlaylistDto playlistDto = mapper.Map<PlaylistDto>(playlist);
            playlistDto.SongsIds = playlist.Songs.Select(song => song.Id).ToList();
            return playlistDto;
        }

        public static async Task<Playlist> ToEntityAsync(this PlaylistDto playlistDto, IUserRepository userRepository, ISongRepository songRepository, IMapper mapper)
        {
            Playlist playlist = mapper.Map<Playlist>(playlistDto);
            playlist.User = await userRepository.GetByIdAsync(playlistDto.UserId);
            playlist.Songs = (await Task.WhenAll(playlistDto.SongsIds.Select(songRepository.GetByIdAsync))).ToList();
            return playlist;
        }
    }
}