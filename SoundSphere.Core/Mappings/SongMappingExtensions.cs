﻿using AutoMapper;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Mappings
{
    public static class SongMappingExtensions
    {
        public static List<SongDto> ToDtos(this List<Song> songs, IMapper mapper) => songs.Select(song => song.ToDto(mapper)).ToList();

        public static async Task<List<Song>> ToEntitiesAsync(this List<SongDto> songDtos, IAlbumRepository albumRepository, IArtistRepository artistRepository, IMapper mapper) =>
            (await Task.WhenAll(songDtos.Select(songDto => songDto.ToEntityAsync(albumRepository, artistRepository, mapper)))).ToList();

        public static SongDto ToDto(this Song song, IMapper mapper)
        {
            SongDto songDto = mapper.Map<SongDto>(song);
            songDto.ArtistsIds = song.Artists.Select(artist => artist.Id).ToList();
            songDto.SimilarSongsIds = song.SimilarSongs.Select(songPair => songPair.SimilarSongId).ToList();
            return songDto;
        }

        public static async Task<Song> ToEntityAsync(this SongDto songDto, IAlbumRepository albumRepository, IArtistRepository artistRepository, IMapper mapper)
        {
            Song song = mapper.Map<Song>(songDto);
            song.Album = await albumRepository.GetByIdAsync(songDto.AlbumId);
            song.Artists = (await Task.WhenAll(songDto.ArtistsIds.Select(artistRepository.GetByIdAsync))).ToList();
            song.SimilarSongs = songDto.SimilarSongsIds.Select(id => new SongPair { SongId = song.Id, SimilarSongId = id }).ToList();
            return song;
        }
    }
}