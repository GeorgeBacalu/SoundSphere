using AutoMapper;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUserRepository userRepository, IMapper mapper) =>
            (_feedbackRepository, _userRepository, _mapper) = (feedbackRepository, userRepository, mapper);

        public async Task<List<FeedbackDto>> GetAllAsync(FeedbackPaginationRequest payload) => (await _feedbackRepository.GetAllAsync(payload)).ToDtos(_mapper);

        public async Task<FeedbackDto> GetByIdAsync(Guid id) => (await _feedbackRepository.GetByIdAsync(id)).ToDto(_mapper);

        public async Task<FeedbackDto> AddAsync(FeedbackDto feedbackDto)
        {
            Feedback feedback = await feedbackDto.ToEntityAsync(_userRepository, _mapper);
            _feedbackRepository.LinkFeedbackToUser(feedback);
            return (await _feedbackRepository.AddAsync(feedback)).ToDto(_mapper);
        }

        public async Task<FeedbackDto> UpdateByIdAsync(FeedbackDto feedbackDto, Guid id) => (await _feedbackRepository.UpdateByIdAsync(await feedbackDto.ToEntityAsync(_userRepository, _mapper), id)).ToDto(_mapper);

        public async Task<FeedbackDto> DeleteByIdAsync(Guid id) => (await _feedbackRepository.DeleteByIdAsync(id)).ToDto(_mapper);
    }
}