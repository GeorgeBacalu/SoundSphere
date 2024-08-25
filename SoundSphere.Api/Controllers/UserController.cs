using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Auth;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Dtos.Response;
using static SoundSphere.Database.Constants;

namespace SoundSphere.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseController
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

        /// <summary>Update user by ID</summary>
        /// <param name="userDto">UserDTO to update</param>
        /// <param name="id">User updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut("{id}")] public async Task<IActionResult> UpdateByIdAsync(UserDto userDto, Guid id) => Ok(await _userService.UpdateByIdAsync(userDto, id));

        /// <summary>Soft delete user by ID</summary>
        /// <param name="id">User deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteByIdAsync(Guid id) => Ok(await _userService.DeleteByIdAsync(id));

        /// <summary>Register new user</summary>
        /// <param name="payload">User registration details</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("register")] public async Task<IActionResult> RegisterAsync(RegisterRequest payload)
        {
            UserDto registeredUserDto = await _userService.RegisterAsync(payload);
            return Created($"{ApiUser}/{registeredUserDto.Id}", registeredUserDto);
        }

        /// <summary>Login user</summary>
        /// <param name="payload">User login details</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpPost("login")] public async Task<IActionResult> LoginAsync(LoginRequest payload) => Ok(new LoginResponse(Token: await _userService.LoginAsync(payload)));
    }
}