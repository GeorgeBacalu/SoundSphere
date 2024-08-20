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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService) => _feedbackService = feedbackService;

        /// <summary>Get all feedbacks with pagination rules</summary>
        /// <param name="payload">Request body with feedbacks pagination rules</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("get")] public async Task<IActionResult> GetAllAsync(FeedbackPaginationRequest payload) => Ok(await _feedbackService.GetAllAsync(payload));

        /// <summary>Get feedback by ID</summary>
        /// <param name="id">Feedback fetching ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(await _feedbackService.GetByIdAsync(id));

        /// <summary>Add feedback</summary>
        /// <param name="feedbackDto">FeedbackDTO to add</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost] public async Task<IActionResult> AddAsync(FeedbackDto feedbackDto)
        {
            FeedbackDto addedFeedbackDto = await _feedbackService.AddAsync(feedbackDto);
            return Created($"{ApiFeedback}/{addedFeedbackDto.Id}", addedFeedbackDto);
        }

        /// <summary>Update feedback by ID</summary>
        /// <param name="feedbackDto">FeedbackDTO to update</param>
        /// <param name="id">Feedback updating ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")] public async Task<IActionResult> UpdateByIdAsync(FeedbackDto feedbackDto, Guid id) => Ok(await _feedbackService.UpdateByIdAsync(feedbackDto, id));

        /// <summary>Soft delete feedback by ID</summary>
        /// <param name="id">Feedback deleting ID</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")] public async Task<IActionResult> DeleteByIdAsync(Guid id) => Ok(await _feedbackService.DeleteByIdAsync(id));
    }
}