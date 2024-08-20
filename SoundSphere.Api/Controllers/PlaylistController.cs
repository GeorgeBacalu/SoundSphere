using Microsoft.AspNetCore.Mvc;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using System.Net.Mime;
using static SoundSphere.Database.Constants;

namespace SoundSphere.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService) => _playlistService = playlistService;

        /// <summary>Get all playlists with pagination rules</summary>
        /// <param name="payload">Request body with playlists pagination rules</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("get")] public async Task<IActionResult> GetAllAsync(PlaylistPaginationRequest payload) => Ok(await _playlistService.GetAllAsync(payload));

        /// <summary>Get playlist by ID</summary>
        /// <param name="id">Playlist fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(await _playlistService.GetByIdAsync(id));

        /// <summary>Add playlist</summary>
        /// <param name="playlistDto">PlaylistDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost] public async Task<IActionResult> AddAsync(PlaylistDto playlistDto)
        {
            PlaylistDto addedPlaylistDto = await _playlistService.AddAsync(playlistDto);
            return Created($"{ApiPlaylist}/{addedPlaylistDto.Id}", addedPlaylistDto);
        }

        /// <summary>Update playlist by ID</summary>
        /// <param name="playlistDto">PlaylistDTO to update</param>
        /// <param name="id">Playlist updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")] public async Task<IActionResult> UpdateByIdAsync(PlaylistDto playlistDto, Guid id) => Ok(await _playlistService.UpdateByIdAsync(playlistDto, id));

        /// <summary>Soft delete playlist by ID</summary>
        /// <param name="id">Playlist deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteByIdAsync(Guid id) => Ok(await _playlistService.DeleteByIdAsync(id));
    }
}