﻿using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        List<Feedback> GetAll(FeedbackPaginationRequest payload);

        Feedback GetById(Guid id);

        Feedback Add(Feedback feedback);

        Feedback UpdateById(Feedback feedback, Guid id);

        Feedback DeleteById(Guid id);

        void LinkFeedbackToUser(Feedback feedback);
    }
}