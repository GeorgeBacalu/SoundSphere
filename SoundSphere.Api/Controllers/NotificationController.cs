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
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService) => _notificationService = notificationService;

        /// <summary>Get all notifications with pagination rules</summary>
        /// <param name="payload">Request body with notifications pagination rules</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("get")] public async Task<IActionResult> GetAllAsync(NotificationPaginationRequest payload) => Ok(await _notificationService.GetAllAsync(payload));

        /// <summary>Get notification by ID</summary>
        /// <param name="id">Notification fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(await _notificationService.GetByIdAsync(id));

        /// <summary>Add notification</summary>
        /// <param name="notificationDto">NotificationDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost] public async Task<IActionResult> AddAsync(NotificationDto notificationDto)
        {
            NotificationDto addedNotificationDto = await _notificationService.AddAsync(notificationDto);
            return Created($"{ApiNotification}/{addedNotificationDto.Id}", addedNotificationDto);
        }

        /// <summary>Update notification by ID</summary>
        /// <param name="notificationDto">NotificationDTO to update</param>
        /// <param name="id">Notification updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut("{id}")] public async Task<IActionResult> UpdateByIdAsync(NotificationDto notificationDto, Guid id) => Ok(await _notificationService.UpdateByIdAsync(notificationDto, id));

        /// <summary>Soft delete notification by ID</summary>
        /// <param name="id">Notification deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteByIdAsync(Guid id) => Ok(await _notificationService.DeleteByIdAsync(id));
    }
}