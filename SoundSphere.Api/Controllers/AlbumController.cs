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
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService) => _albumService = albumService;

        /// <summary>Get all albums with pagination rules</summary>
        /// <param name="payload">Request body with albums pagination rules</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("get")] public IActionResult GetAll(AlbumPaginationRequest payload) => Ok(_albumService.GetAll(payload));

        /// <summary>Get album by ID</summary>
        /// <param name="id">Album fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public IActionResult GetById(Guid id) => Ok(_albumService.GetById(id));

        /// <summary>Add album</summary>
        /// <param name="albumDto">AlbumDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost] public IActionResult Add(AlbumDto albumDto)
        {
            AlbumDto addedAlbumDto = _albumService.Add(albumDto);
            return CreatedAtAction(nameof(GetById), new { addedAlbumDto.Id }, addedAlbumDto);
        }

        /// <summary>Update album by ID</summary>
        /// <param name="albumDto">AlbumDTO to update</param>
        /// <param name="id">Album updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")] public IActionResult UpdateById(AlbumDto albumDto, Guid id) => Ok(_albumService.UpdateById(albumDto, id));

        /// <summary>Soft delete album by ID</summary>
        /// <param name="id">Album deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")] public IActionResult DeleteById(Guid id) => Ok(_albumService.DeleteById(id));
    }
}