using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<FeedbackDto>> GetAllAsync(FeedbackPaginationRequest payload);

        Task<FeedbackDto> GetByIdAsync(Guid id);

        Task<FeedbackDto> AddAsync(FeedbackDto feedbackDto);

        Task<FeedbackDto> UpdateByIdAsync(FeedbackDto feedbackDto, Guid id);

        Task<FeedbackDto> DeleteByIdAsync(Guid id);
    }
}