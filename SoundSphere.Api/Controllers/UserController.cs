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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        /// <summary>Get all users with pagination rules</summary>
        /// <param name="payload">Request body with users pagination rules</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("get")] public async Task<IActionResult> GetAllAsync(UserPaginationRequest payload) => Ok(await _userService.GetAllAsync(payload));

        /// <summary>Get user by ID</summary>
        /// <param name="id">User fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(await _userService.GetByIdAsync(id));

        /// <summary>Add user</summary>
        /// <param name="userDto">UserDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost] public async Task<IActionResult> AddAsync(UserDto userDto)
        {
            UserDto addedUserDto = await _userService.AddAsync(userDto);
            return Created($"{ApiUser}/{addedUserDto.Id}", addedUserDto);
        }

        /// <summary>Update user by ID</summary>
        /// <param name="userDto">UserDTO to update</param>
        /// <param name="id">User updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")] public async Task<IActionResult> UpdateByIdAsync(UserDto userDto, Guid id) => Ok(await _userService.UpdateByIdAsync(userDto, id));

        /// <summary>Soft delete user by ID</summary>
        /// <param name="id">User deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteByIdAsync(Guid id) => Ok(await _userService.DeleteByIdAsync(id));
    }
}