using AutoMapper;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;

namespace SoundSphere.Core.Mappings
{
    public static class FeedbackMappingExtensions
    {
        public static List<FeedbackDto> ToDtos(this List<Feedback> feedbacks, IMapper mapper) => feedbacks.Select(feedback => feedback.ToDto(mapper)).ToList();

        public static async Task<List<Feedback>> ToEntitiesAsync(this List<FeedbackDto> feedbackDtos, IUserRepository userRepository, IMapper mapper) =>
            (await Task.WhenAll(feedbackDtos.Select(feedbackDto => feedbackDto.ToEntityAsync(userRepository, mapper)))).ToList();

        public static FeedbackDto ToDto(this Feedback feedback, IMapper mapper) => mapper.Map<FeedbackDto>(feedback);

        public static async Task<Feedback> ToEntityAsync(this FeedbackDto feedbackDto, IUserRepository userRepository, IMapper mapper)
        {
            Feedback feedback = mapper.Map<Feedback>(feedbackDto);
            feedback.User = await userRepository.GetByIdAsync(feedbackDto.UserId);
            return feedback;
        }
    }
}