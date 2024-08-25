using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using static SoundSphere.Database.Constants;

namespace SoundSphere.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AlbumController : BaseController
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService) => _albumService = albumService;

        /// <summary>Get all albums with pagination rules</summary>
        /// <param name="payload">Request body with albums pagination rules</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("get")] public async Task<IActionResult> GetAllAsync(AlbumPaginationRequest payload) => Ok(await _albumService.GetAllAsync(payload));

        /// <summary>Get album by ID</summary>
        /// <param name="id">Album fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(await _albumService.GetByIdAsync(id));

        /// <summary>Add album</summary>
        /// <param name="albumDto">AlbumDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost] public async Task<IActionResult> AddAsync(AlbumDto albumDto)
        {
            AlbumDto addedAlbumDto = await _albumService.AddAsync(albumDto);
            return Created($"{ApiAlbum}/{addedAlbumDto.Id}", addedAlbumDto);
        }

        /// <summary>Update album by ID</summary>
        /// <param name="albumDto">AlbumDTO to update</param>
        /// <param name="id">Album updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut("{id}")] public async Task<IActionResult> UpdateByIdAsync(AlbumDto albumDto, Guid id) => Ok(await _albumService.UpdateByIdAsync(albumDto, id));

        /// <summary>Soft delete album by ID</summary>
        /// <param name="id">Album deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteByIdAsync(Guid id) => Ok(await _albumService.DeleteByIdAsync(id));
    }
}