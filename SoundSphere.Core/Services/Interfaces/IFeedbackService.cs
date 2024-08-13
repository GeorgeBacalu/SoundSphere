﻿using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Pagination;

namespace SoundSphere.Core.Services.Interfaces
{
    public interface IFeedbackService
    {
        List<FeedbackDto> GetAll(FeedbackPaginationRequest payload);

        FeedbackDto GetById(Guid id);

        FeedbackDto Add(FeedbackDto feedbackDto);

        FeedbackDto UpdateById(FeedbackDto feedbackDto, Guid id);

        FeedbackDto DeleteById(Guid id);
    }
}