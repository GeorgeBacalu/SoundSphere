﻿namespace SoundSphere.Database.Entities
{
    public class Feedback : BaseEntity
    {
        public User User { get; set; } = null!;
        public FeedbackType Type { get; set; }
        public string Message { get; set; } = null!;
    }

    public enum FeedbackType { InvalidFeedbackType, Issue, Optimization, Improvement }
}