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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService) => _artistService = artistService;

        /// <summary>Get all artists with pagination rules</summary>
        /// <param name="payload">Request body with artists pagination rules</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("get")] public async Task<IActionResult> GetAllAsync(ArtistPaginationRequest payload) => Ok(await _artistService.GetAllAsync(payload));

        /// <summary>Get artist by ID</summary>
        /// <param name="id">Artist fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(await _artistService.GetByIdAsync(id));

        /// <summary>Add artist</summary>
        /// <param name="artistDto">ArtistDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost] public async Task<IActionResult> AddAsync(ArtistDto artistDto)
        {
            ArtistDto addedArtistDto = await _artistService.AddAsync(artistDto);
            return Created($"{ApiArtist}/{addedArtistDto.Id}", addedArtistDto);
        }

        /// <summary>Update artist by ID</summary>
        /// <param name="artistDto">ArtistDTO to update</param>
        /// <param name="id">Artist updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")] public async Task<IActionResult> UpdateByIdAsync(ArtistDto artistDto, Guid id) => Ok(await _artistService.UpdateByIdAsync(artistDto, id));

        /// <summary>Soft delete artist by ID</summary>
        /// <param name="id">Artist deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteByIdAsync(Guid id) => Ok(await _artistService.DeleteByIdAsync(id));
    }
}