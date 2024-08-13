﻿using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IPlaylistService
    {
        List<PlaylistDto> GetAll(PlaylistPaginationRequest payload);

        PlaylistDto GetById(Guid id);

        PlaylistDto Add(PlaylistDto playlistDto);

        PlaylistDto UpdateById(PlaylistDto playlistDto, Guid id);

        PlaylistDto DeleteById(Guid id);
    }
}