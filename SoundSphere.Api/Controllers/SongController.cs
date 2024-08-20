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
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService) => _songService = songService;

        /// <summary>Get all songs with pagination rules</summary>
        /// <param name="payload">Request body with songs pagination rules</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("get")] public async Task<IActionResult> GetAllAsync(SongPaginationRequest payload) => Ok(await _songService.GetAllAsync(payload));

        /// <summary>Get song by ID</summary>
        /// <param name="id">Song fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(await _songService.GetByIdAsync(id));

        /// <summary>Add song</summary>
        /// <param name="songDto">SongDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost] public async Task<IActionResult> AddAsync(SongDto songDto)
        {
            SongDto addedSongDto = await _songService.AddAsync(songDto);
            return Created($"{ApiSong}/{addedSongDto.Id}", addedSongDto);
        }
        /// <summary>Update song by ID</summary>
        /// <param name="songDto">SongDTO to update</param>
        /// <param name="id">Song updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")] public async Task<IActionResult> UpdateByIdAsync(SongDto songDto, Guid id) => Ok(await _songService.UpdateByIdAsync(songDto, id));

        /// <summary>Soft delete song by ID</summary>
        /// <param name="id">Song deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteByIdAsync(Guid id) => Ok(await _songService.DeleteByIdAsync(id));
    }
}