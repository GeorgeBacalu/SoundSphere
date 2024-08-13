using Microsoft.AspNetCore.Mvc;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using System.Net.Mime;

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
        [HttpPost("get")] public IActionResult GetAll(SongPaginationRequest payload) => Ok(_songService.GetAll(payload));

        /// <summary>Get song by ID</summary>
        /// <param name="id">Song fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public IActionResult GetById(Guid id) => Ok(_songService.GetById(id));

        /// <summary>Add song</summary>
        /// <param name="songDto">SongDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost] public IActionResult Add(SongDto songDto)
        {
            SongDto addedSongDto = _songService.Add(songDto);
            return CreatedAtAction(nameof(GetById), new { addedSongDto.Id }, addedSongDto);
        }
        /// <summary>Update song by ID</summary>
        /// <param name="songDto">SongDTO to update</param>
        /// <param name="id">Song updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")] public IActionResult UpdateById(SongDto songDto, Guid id) => Ok(_songService.UpdateById(songDto, id));

        /// <summary>Soft delete song by ID</summary>
        /// <param name="id">Song deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")] public IActionResult DeleteById(Guid id) => Ok(_songService.DeleteById(id));
    }
}