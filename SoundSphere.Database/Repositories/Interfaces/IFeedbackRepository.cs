using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllAsync(FeedbackPaginationRequest payload);

        Task<Feedback> GetByIdAsync(Guid id);

        Task<Feedback> AddAsync(Feedback feedback);

        Task<Feedback> UpdateByIdAsync(Feedback feedback, Guid id);

        Task<Feedback> DeleteByIdAsync(Guid id);

        void LinkFeedbackToUser(Feedback feedback);
    }
}